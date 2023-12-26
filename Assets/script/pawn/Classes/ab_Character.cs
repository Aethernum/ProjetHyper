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
    protected bool isActivated = false;
    [SerializeField] protected string team;

    protected GameManager gameManager;    // Accesseur
    protected BattleSystem battleSystem;

    public float GetSpeed()
    {
        return this.speed;
    }

    public bool IsActivated
    {
        get
        {
            return isActivated;
        }

        set
        {
            isActivated = value;
        }
    }
    public string Team
    {
        get
        {
            return team;
        }

        set
        {
            team = value;
        }
    }
    private void Awake()
    {
        gameManager = GameManager.Instance;

        if (gameManager != null)
        {
            BattleSystem.OnBattleStateChange += BattleSystemOnBattleStateChange;
        }
    }

    private void BattleSystemOnBattleStateChange(BattleState obj)
    {
    }

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        lastVelocity = rb.velocity;
    }

    public virtual void ActiveLayer()
    {
        gameObject.layer = LayerMask.NameToLayer("ActiveTeam");
        transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("ActiveTeam");
        gameObject.GetComponent<Rigidbody>().excludeLayers = LayerMask.GetMask("ActiveTeam");
    }

    public virtual void DeactiveLayer()
    {
        gameObject.layer = LayerMask.NameToLayer("InactiveTeam");
        transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("InactiveTeam");
        gameObject.GetComponent<Rigidbody>().excludeLayers = LayerMask.GetMask("Nothing");
    }

    public void TakeDamage(int dmg)
    {
        if(!isActivated)
        {
            currentHp = currentHp - (dmg - defense);
        }
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