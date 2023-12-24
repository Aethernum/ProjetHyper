using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public enum BattleState
{
    InitializeGame,
    PlayerTurn,
    EnnemyTurn,
    EndingTurn,
    Won,
    Lose
}

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private List<Spawner> team1SpawnPoints;
    [SerializeField] private List<Spawner> team2SpawnPoints;

    [SerializeField] private List<Character> team1UnitsInfo;
    [SerializeField] private List<Character> team2UnitsInfo;

    private List<Character> team1Units;
    private List<Character> team2Units;
    private BattleState lastState = BattleState.InitializeGame;
    public BattleState state;
    public static event Action<BattleState> OnBattleStateChange;

    private void Start()
    {
        team1Units = new List<Character>();
        team2Units = new List<Character>();
        UpdateGameState(BattleState.InitializeGame);
    }

    public void UpdateGameState(BattleState newState)
    {
        lastState = state;
        state = newState;

        switch (state)
        {
            case BattleState.InitializeGame:
                InitializeGame();
                break;

            case BattleState.PlayerTurn:
                HandlePlayerTurn();
                break;

            case BattleState.EnnemyTurn:
                HandleEnnemyTurn();
                break;

            case BattleState.EndingTurn:
                HandleEndingTurn();
                break;

            case BattleState.Won:
                break;

            case BattleState.Lose:
                break;
        }

        OnBattleStateChange?.Invoke(newState);
    }

    private void HandleEndingTurn()
    {
        foreach (var pawn in team1Units)
        {
            pawn.DeactiveLayer();
        }
        foreach (var pawn in team2Units)
        {
            pawn.DeactiveLayer();
        }
        if (lastState == BattleState.EnnemyTurn || lastState == BattleState.InitializeGame)
        {
            UpdateGameState(BattleState.PlayerTurn);
        }
        else
        {
            UpdateGameState(BattleState.EnnemyTurn);
        }
    }

    private async void HandleEnnemyTurn()
    {
        foreach (var pawn in team2Units)
        {
            pawn.ActiveLayer();
        }
        await Task.Delay(2000);
        UpdateGameState(BattleState.EndingTurn);
    }

    private void HandlePlayerTurn()
    {
        foreach (var pawn in team1Units)
        {
            Debug.Log(pawn.gameObject.name);
            pawn.ActiveLayer();
        }
    }

    private void InitializeGame()
    {
        SpawnTeam(team1SpawnPoints, team1UnitsInfo, team1Units);
        SpawnTeam(team2SpawnPoints, team2UnitsInfo, team2Units);
        UpdateGameState(BattleState.EndingTurn);
    }

    /*
    public void StartBattle()
    {
        SpawnTeam(team1SpawnPoints, team1Units);
        SpawnTeam(team1SpawnPoints, team1Units);
        // Commencer le tour de l'équipe 1
        activeTeam = 0;
        SetUnitActiveState(team1Units, true);
        SetUnitActiveState(team2Units, false);
    }

    public void EndTurn()
    {
        // Passer au tour suivant
        activeTeam = (activeTeam + 1) % 2;

        SetUnitActiveState(team1Units, activeTeam == 0);
        SetUnitActiveState(team2Units, activeTeam == 1);
    }
    */

    private void SpawnTeam(List<Spawner> spawners, List<Character> characterListInfo, List<Character> TeamGameList)
    {
        int maxCount = Mathf.Min(spawners.Count, characterListInfo.Count);
        if (maxCount == 0 || characterListInfo.Count > spawners.Count)
        {
            Debug.Log("error in list");
            return;
        }
        for (int i = 0; i < maxCount; i++)
        {
            // Faire spawn sur chaque spawner
            Character newPawn = Instantiate(characterListInfo[i], spawners[i].transform.position, spawners[i].transform.rotation, spawners[i].transform);

            TeamGameList.Add(newPawn);
        }
    }

    /*
    private void SetUnitActiveState(List<Character> units, bool active)
    {
        foreach (Character unit in units)
        {
            unit.IsActivated = active;
        }
    }

    // Gestion de la fin de partie
    private bool CheckGameOver()
    {
        if (team1Units.Count == 0 || team2Units.Count == 0)
        {
            return true;
        }

        return false;
    }*/
}