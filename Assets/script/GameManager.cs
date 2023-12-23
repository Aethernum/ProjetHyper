using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Spawner> spawnersTeam1;
    public List<Spawner> spawnersTeam2;
    public List<Character> characterListTeam1;
    public List<Character> characterListTeam2;

    private void Start()
    {
        SpawnTeam(spawnersTeam1, characterListTeam1, "Team1");
        SpawnTeam(spawnersTeam2, characterListTeam2, "Team2");
    }

    private void SpawnTeam(List<Spawner> spawners, List<Character> characterList, string teamName)
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
            spawners[i].Spawn(characterList[i], teamName);
        }
    }
}