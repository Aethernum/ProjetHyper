using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rebound : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 lastVelocity;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();   
        rb.AddForce(200,0,300);
    }

    // Update is called once per frame
     void Update()
    {
        lastVelocity = rb.velocity;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name.Contains("Wall") || col.gameObject.name.Contains("Cylinder"))
        {
            Debug.Log("Collision!");
            foreach (ContactPoint contact in col.contacts)
            {
                Debug.Log("Point de contact : " + contact.point);
                Debug.Log("Normale du contact : " + contact.normal);
                Debug.Log("Autre collider : " + contact.otherCollider);
                // Vous pouvez ajouter d'autres informations que vous voulez afficher ici
            }
            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, col.contacts[0].normal);

            rb.velocity = direction * Mathf.Max(speed, 0f);
        }
    }

}
