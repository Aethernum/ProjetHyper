using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PawnType
    {
        Bounce,
        Penetrate,
        Stick
        // Ajoutez d'autres types selon vos besoins
    }
public class Pawn : MonoBehaviour
{

    [SerializeField] private string heroesName;
    [SerializeField] private float heroesSpeed;
    
    [SerializeField] private PawnType heroesPawnType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
