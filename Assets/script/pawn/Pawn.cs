using UnityEngine;

public enum PawnType
{
    Bounce,
    Penetrate,
    Stick
    // Ajoutez d'autres types selon vos besoins
}

public class Pawn : MonoBehaviour
{
    private bool isMouseDragging = false;
    private Vector3 clickPosition;
    private Rigidbody rb;
    [SerializeField] private string heroesName;
    [SerializeField] private float heroesSpeed;

    [SerializeField] private PawnType heroesPawnType;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
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

    private void TryStartDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
        {
            // Le clic de la souris a commencé sur le pawn
            OnMouseDown();
            isMouseDragging = true;
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

        // Appliquer une force proportionnelle à la distance pour simuler la projection
        rb.AddForce(-dragDirection * heroesSpeed, ForceMode.Impulse);
        isMouseDragging = false;
    }

    private void OnMouseDown()
    {
        clickPosition = Input.mousePosition;
        clickPosition.z = Camera.main.WorldToScreenPoint(transform.position).z;

        // Convertir la position du clic en position dans l'espace du monde
        clickPosition = Camera.main.ScreenToWorldPoint(clickPosition);
    }
}