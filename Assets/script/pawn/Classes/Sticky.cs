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
            rb.velocity = -col.contacts[0].normal * velocity.magnitude * 0.1f;
            Debug.Log("Collision Enfant : Ennemy");
        }
    }
}