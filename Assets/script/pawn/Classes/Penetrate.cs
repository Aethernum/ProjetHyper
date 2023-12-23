using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penetrate : Character
{
    protected List<GameObject> enemiesInContact = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.excludeLayers = LayerMask.GetMask("Ally" ,"Ennemy"); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnCollisionEnter(Collision col)
    {
        base.OnCollisionEnter(col);    
    }

    private void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "Ennemy")
        {
            if (!enemiesInContact.Contains(col.gameObject))
            {
                enemiesInContact.Add(col.gameObject);
                Debug.Log("Contact avec : " + col.gameObject.name);
                Stats opponentScript = col.gameObject.GetComponentInParent<Stats>();
                opponentScript.TakeDamage(attack);
                Debug.Log("Collision Enfant : Ennemy");
            }
        }
    }

    private void OnTriggerExit(Collider col) {
        if (enemiesInContact.Contains(col.gameObject))
        {
            enemiesInContact.Remove(col.gameObject);
        }
    }
}
