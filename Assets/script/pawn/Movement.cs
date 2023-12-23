using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private bool isMouseDragging = false;
    private bool isSelected;
    private Vector3 clickPosition;
    private Rigidbody rb;

    private SpeedSensor sensor;
    private IEnumerator speedCheckCoroutine;
    private Character character;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        character = GetComponent<Character>();
        sensor = GetComponent<SpeedSensor>();
        sensor.OnSpeedReached += OnSpeedLimitReached;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryStartDrag();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isMouseDragging)
            {
                OnMouseUp();
            }
        }
    }

    private IEnumerator SpeedCheckCoroutine()
    {
        while (true)
        {
            if (isSelected || rb.velocity.magnitude > 0.1f)
            {
                rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            }
            else
            {
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    private void OnSpeedLimitReached(float speed)
    {
        isSelected = false;
    }

    private void TryStartDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
        {
            // Le clic de la souris a commencé sur le pawn
            OnMouseDown();
        }
    }

    private void OnMouseUp()
    {
        Vector3 currentMousePosition = Input.mousePosition;
        currentMousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z;

        // Convertir la position actuelle de la souris en position dans l'espace du monde
        currentMousePosition = Camera.main.ScreenToWorldPoint(currentMousePosition);

        // Calculer la direction et la distance entre le clic initial et la position actuelle de la souris
        Vector3 dragDirection = currentMousePosition - clickPosition;
        Vector3 forceToApply = -dragDirection * character.GetSpeed();
        // Vérifier si la magnitude de la force à appliquer dépasse la force maximale

        // Appliquer une force proportionnelle à la distance pour simuler la projection
        rb.AddForce(forceToApply, ForceMode.Impulse);

        isMouseDragging = false;
        Debug.Log("unclicked");
    }

    private void OnMouseDown()
    {
        if (gameObject.tag == "Ennemy")
        {
            return;
        }
        isSelected = true;

        clickPosition = Input.mousePosition;
        clickPosition.z = Camera.main.WorldToScreenPoint(transform.position).z;

        // Convertir la position du clic en position dans l'espace du monde
        clickPosition = Camera.main.ScreenToWorldPoint(clickPosition);
        Debug.Log("clicked");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            speedCheckCoroutine = SpeedCheckCoroutine();
            StartCoroutine(speedCheckCoroutine);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            StopCoroutine(speedCheckCoroutine);
        }
    }
}