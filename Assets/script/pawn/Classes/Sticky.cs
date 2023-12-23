using UnityEngine;

public class Sticky : Character
{
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    protected override void OnCollisionEnter(Collision col)
    {
        base.OnCollisionEnter(col);
        if (col.gameObject.tag == "Ennemy")
        {
            Vector3 velocity = rb.velocity;
            rb.velocity = Vector3.zero;

            Collider opponentCollider = col.collider;
            Collider thisCollider = GetComponent<Collider>();
            if (opponentCollider != null && thisCollider != null)
            {
                // Trouve le point de contact entre les deux colliders
                ContactPoint contact = col.contacts[0]; // Obtient le premier point de contact de la collision
                Vector3 pointDeContact = contact.point;

                // Place l'objet A à la limite du collider de l'objet B
                transform.position = pointDeContact;
            }
            Character opponentCharacter = col.gameObject.GetComponentInParent<Character>();
            opponentCharacter.TakeDamage(attack);
        }
    }
}