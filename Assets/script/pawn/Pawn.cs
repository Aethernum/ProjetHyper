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
    private bool isSelected;
    private Vector3 clickPosition;
    private Rigidbody rb;
    [SerializeField] private string heroesName;
    [SerializeField] private float heroesSpeed;
    [SerializeField] private float maxForce = 10f; 
    [SerializeField] private PawnType heroesPawnType;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (heroesPawnType == PawnType.Penetrate){
            gameObject.layer = LayerMask.NameToLayer("Penetrate");
        } 
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

    private void TryStartDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.Log("try");
        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
        {
            isMouseDragging = true;
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
        Vector3 forceToApply = -dragDirection * heroesSpeed;
        // Vérifier si la magnitude de la force à appliquer dépasse la force maximale
        if (forceToApply.magnitude > maxForce)
        {
            // Réduire la magnitude à la force maximale autorisée
            forceToApply = forceToApply.normalized * maxForce;
        }
        // Appliquer une force proportionnelle à la distance pour simuler la projection
        rb.AddForce(forceToApply, ForceMode.Impulse);
        
        isMouseDragging = false;
        isSelected = false;
        
    }

    private void OnMouseDown()
    {
        isSelected = true;
        clickPosition = Input.mousePosition;
        clickPosition.z = Camera.main.WorldToScreenPoint(transform.position).z;

        // Convertir la position du clic en position dans l'espace du monde
        clickPosition = Camera.main.ScreenToWorldPoint(clickPosition);
    }
}