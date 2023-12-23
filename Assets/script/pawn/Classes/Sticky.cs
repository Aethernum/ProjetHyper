using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticky : Character
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
            rb.velocity = Vector3.zero;
            Debug.Log("Collision Enfant : Ennemy");
        }
       
    }
}
