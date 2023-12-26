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
            // S'il y a déjà une instance, détruisez celle-ci
            Destroy(gameObject);
        }
    }

    public UiManager uiManager;
    public BattleSystem battleSystem;

    private void Start()
    {
        BattleSystem.OnBattleStateChange += HandleBattleStateChange;
    }

    // Méthode qui sera appelée lorsque l'état de la bataille change
    public void HandleBattleStateChange(BattleState newState)
    {
    }

    private void OnDestroy()
    {
        // Assurez-vous de vous désabonner lorsque le GameManager est détruit
        BattleSystem.OnBattleStateChange -= HandleBattleStateChange;
    }
}