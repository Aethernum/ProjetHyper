using UnityEngine;

public class Spawner : MonoBehaviour
{
    public void Spawn(Character pawnToSpawn, string memo)
    {
        Character newPawn = Instantiate(pawnToSpawn, transform.position, transform.rotation, transform); ;
        newPawn.gameObject.layer = LayerMask.NameToLayer(memo);
        newPawn.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer(memo);
        newPawn.gameObject.GetComponent<Rigidbody>().excludeLayers = LayerMask.GetMask(memo);
    }
}