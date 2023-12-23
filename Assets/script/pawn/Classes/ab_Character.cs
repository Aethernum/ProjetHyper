using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PawnType
{
    Bounce,
    Penetrate,
    Stick
    // Ajoutez d'autres types selon vos besoins
}

abstract public class Character : MonoBehaviour
{
    [SerializeField] protected string heroesName;
    [SerializeField] protected int maxHp = 50;
    [SerializeField] protected int currentHp = 50;
    [SerializeField] protected float speed = 5;
    [SerializeField] protected int attack = 10;
    [SerializeField] protected int defense = 5;
    [SerializeField] protected PawnType type;
    protected Rigidbody rb;
    protected Vector3 lastVelocity;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lastVelocity = rb.velocity;
    }

    public void takeDamage(int dmg)
    {
        currentHp = currentHp - (dmg - defense);
    }

    protected virtual void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Wall")
        {
            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, col.contacts[0].normal);

            rb.velocity = direction * Mathf.Max(speed, 0f);
        }
       
    }
}
