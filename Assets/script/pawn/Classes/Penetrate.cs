using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Penetrate : Character
{
    protected List<GameObject> enemiesInContact = new List<GameObject>();

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void OnCollisionEnter(Collision col)
    {
        base.OnCollisionEnter(col);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Ennemy")
        {
            if (!enemiesInContact.Contains(col.gameObject))
            {
                enemiesInContact.Add(col.gameObject);
                Character opponentCharacter = col.gameObject.GetComponentInParent<Character>();
                opponentCharacter.TakeDamage(attack);
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (enemiesInContact.Contains(col.gameObject))
        {
            enemiesInContact.Remove(col.gameObject);
        }
    }
}