using System.Collections.Generic;
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
    private void Update()
    {
    }

    protected override void OnCollisionEnter(Collision col)
    {
        base.OnCollisionEnter(col);
    }

    public override void ActiveLayer()
    {
        gameObject.layer = LayerMask.NameToLayer("ActiveTeam");
        transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("ActiveTeam");
        gameObject.GetComponent<Rigidbody>().excludeLayers = LayerMask.GetMask("ActiveTeam", "InactiveTeam");
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Ennemy")
        {
            if (!enemiesInContact.Contains(col.gameObject))
            {
                enemiesInContact.Add(col.gameObject);
                Character opponentCharacter = col.gameObject.GetComponent<Character>();
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