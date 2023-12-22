using UnityEngine;

public class Rebound : MonoBehaviour
{
    private Pawn pawn;
    private Rigidbody rb;
    private Vector3 lastVelocity;
    private PawnType pawnType;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pawn = GetComponent<Pawn>();
        pawnType = pawn.getPawnType();
    }

    // Update is called once per frame
    private void Update()
    {
        lastVelocity = rb.velocity;
    }

    private void OnCollisionEnter(Collision col)
    {
        Debug.Log("Collision!");
        foreach (ContactPoint contact in col.contacts)
        {
            Debug.Log("Autre collider : " + contact.otherCollider);
            // Vous pouvez ajouter d'autres informations que vous voulez afficher ici
        }

        if (col.gameObject.tag == "Wall")
        {
            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, col.contacts[0].normal);

            rb.velocity = direction * Mathf.Max(speed, 0f);
        }
        /*else if(col.gameObject.tag == "Ennemy")
        {
            switch(this.pawnType)
            {
                case PawnType.Bounce:
                    var speed = lastVelocity.magnitude;
                    var direction = Vector3.Reflect(lastVelocity.normalized, col.contacts[0].normal);
                    rb.velocity = direction * Mathf.Max(speed, 0f);
                break;
                case PawnType.Penetrate:
                    Debug.Log("Penetrate");
                break;
                case PawnType.Stick:
                    Debug.Log("Stick!");
                     rb.velocity = Vector3.zero;
                break;
                default:
                break;
            }
        }*/
       
    }
}