using UnityEngine;
using UnityEngine.UIElements;

public class UiManager : MonoBehaviour
{
    [SerializeField] private UIDocument baseUi;
    private VisualElement root;
    private Button startGame;
    private Button endturn1;
    private Button endturn2;

    // Start is called before the first frame update
    private void Start()
    {
        root = baseUi.rootVisualElement;
        endturn1 = root.Q<Button>("EndTurnPlayer");
        endturn1.clicked += () =>
        {
            if (GameManager.Instance.battleSystem.state == BattleState.PlayerTurn)
            {
                GameManager.Instance.battleSystem.UpdateGameState(BattleState.EndingTurn);
            }
        };
    }

    // Update is called once per frame
    private void Update()
    {
    }
}