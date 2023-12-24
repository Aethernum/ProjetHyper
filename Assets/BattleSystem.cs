using System;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState
{
    InitializeGame,
    PlayerTurn,
    EnnemyTurn,
    Won,
    Lose
}

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private List<Spawner> team1SpawnPoints;
    [SerializeField] private List<Spawner> team2SpawnPoints;

    [SerializeField] private List<Character> team1Units;
    [SerializeField] private List<Character> team2Units;

    public BattleState state;
    public static event Action<BattleState> OnBattleStateChange;

    private void Start()
    {
        UpdateGameState(BattleState.InitializeGame);
    }

    public void UpdateGameState(BattleState newState)
    {
        state = newState;

        switch (state)
        {
            case BattleState.InitializeGame:
                InitializeGame();
                break;

            case BattleState.PlayerTurn:
                break;

            case BattleState.EnnemyTurn:
                break;

            case BattleState.Won:
                break;

            case BattleState.Lose:
                break;
        }

        OnBattleStateChange?.Invoke(newState);
    }

    private void InitializeGame()
    {
        SpawnTeam(team1SpawnPoints, team1Units);
        SpawnTeam(team2SpawnPoints, team2Units);
        UpdateGameState(BattleState.PlayerTurn);
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

    private void SpawnTeam(List<Spawner> spawners, List<Character> characterList)
    {
        int maxCount = Mathf.Min(spawners.Count, characterList.Count);
        if (maxCount == 0 || characterList.Count > spawners.Count)
        {
            Debug.Log("error in list");
            return;
        }
        for (int i = 0; i < maxCount; i++)
        {
            // Faire spawn sur chaque spawner
            spawners[i].Spawn(characterList[i]);
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