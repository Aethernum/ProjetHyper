using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected string heroesName;
    [SerializeField] protected int maxHp = 50;
    [SerializeField] protected int currentHp = 50;
    [SerializeField] protected float speed = 5;

    [SerializeField] protected int attack = 10;
    [SerializeField] protected int defense = 5;
    protected Rigidbody rb;
    protected Vector3 lastVelocity;
    protected bool IsActivated;

    // Accesseur
    public float GetSpeed()
    {
        return this.speed;
    }

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        lastVelocity = rb.velocity;
    }

    public void TakeDamage(int dmg)
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
            Debug.Log("Collision Parent : Wall");
        }
    }
}