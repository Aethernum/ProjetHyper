using UnityEngine;

public class Rebound : MonoBehaviour
{
    private Pawn pawn;
    private Rigidbody rb;
    private Vector3 lastVelocity;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pawn = GetComponent<Pawn>();
    }

    // Update is called once per frame
    private void Update()
    {
        lastVelocity = rb.velocity;
    }

    private void OnCollisionEnter(Collision col)
    {
        foreach (ContactPoint contact in col.contacts)
        {
            // Vous pouvez ajouter d'autres informations que vous voulez afficher ici
        }

        if (col.gameObject.tag == "Wall")
        {
            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, col.contacts[0].normal);

            rb.velocity = direction * Mathf.Max(speed, 0f);
        }
    }
}