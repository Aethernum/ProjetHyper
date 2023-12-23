using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{

    [SerializeField] private int maxHp = 50;
    [SerializeField] private int currentHp = 50;
    [SerializeField] private float heroesSpeed = 5;
    [SerializeField] private int attack = 10;
    [SerializeField] private int defense = 5;
    private List<GameObject> enemiesInContact = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(int dmg)
    {
        currentHp = currentHp - (dmg - defense);
    }

    private void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "Ennemy")
        {
            if (!enemiesInContact.Contains(col.gameObject))
            {
                enemiesInContact.Add(col.gameObject);
                Debug.Log("Contact avec : " + col.gameObject.name);
                Stats opponentScript = col.gameObject.GetComponentInParent<Stats>();
                opponentScript.takeDamage(attack);
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
