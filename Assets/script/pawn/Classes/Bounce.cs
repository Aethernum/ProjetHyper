using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : Character
{
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnCollisionEnter(Collision col)
    {
        base.OnCollisionEnter(col);
        if(col.gameObject.tag == "Ennemy")
        {
            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, col.contacts[0].normal);
            rb.velocity = direction * Mathf.Max(speed, 0f);
            Debug.Log("Collision Enfant : Ennemy");
        }
       
    }
}
