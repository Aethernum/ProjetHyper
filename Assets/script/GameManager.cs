using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        // Assurez-vous qu'il n'y a qu'une seule instance de GameManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            // S'il y a d�j� une instance, d�truisez celle-ci
            Destroy(gameObject);
        }
    }

    public UiManager uiManager;
    public BattleSystem battleSystem;

    private void Start()
    {
        BattleSystem.OnBattleStateChange += HandleBattleStateChange;
    }

    // M�thode qui sera appel�e lorsque l'�tat de la bataille change
    public void HandleBattleStateChange(BattleState newState)
    {
    }

    private void OnDestroy()
    {
        // Assurez-vous de vous d�sabonner lorsque le GameManager est d�truit
        BattleSystem.OnBattleStateChange -= HandleBattleStateChange;
    }
}