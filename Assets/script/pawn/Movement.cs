using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
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
        if (rb.velocity.magnitude > 0)
        {
            Debug.Log(rb.velocity);
        }
    }

    private IEnumerator SpeedCheckCoroutine()
    {
        while (true)
        {
            if (character.GetActivated() || rb.velocity.magnitude > 0.1f)
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
        character.SetActivated(false);
    }

    private void OnMouseUp()
    {
        Vector3 currentMousePosition = Input.mousePosition;
        currentMousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z;

        // Convertir la position actuelle de la souris en position dans l'espace du monde
        currentMousePosition = Camera.main.ScreenToWorldPoint(currentMousePosition);

        // Calcul de la direction et distance du drag
        Vector3 dragDirection = currentMousePosition - clickPosition;
        float dragDistance = dragDirection.magnitude;
        // Distance limite
        float limitDistance = 7f;

        // Si distance > limite, force = vitesse max
        // Sinon force = % de la vitesse
        float forceMultiplier;
        if (dragDistance > limitDistance)
        {
            forceMultiplier = 1f;
        }
        else
        {
            forceMultiplier = dragDistance / limitDistance;
        }
        Vector3 forceToApply = -dragDirection.normalized * character.GetSpeed() * forceMultiplier;

        // Appliquer une force proportionnelle � la distance pour simuler la projection
        rb.AddForce(forceToApply, ForceMode.Impulse);
    }

    private void OnMouseDown()
    {
        if (gameObject.layer == LayerMask.NameToLayer("InactiveTeam"))
        {
            return;
        }
        character.SetActivated(true);

        clickPosition = Input.mousePosition;
        clickPosition.z = Camera.main.WorldToScreenPoint(transform.position).z;

        // Convertir la position du clic en position dans l'espace du monde
        clickPosition = Camera.main.ScreenToWorldPoint(clickPosition);
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