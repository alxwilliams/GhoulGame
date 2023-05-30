using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GhoulFactory : MonoBehaviour   
{
    public GhoulHead firstHead;
    public GhoulHead secondHead;
    
    
    public GhoulHead prefab;

    private int i = 0;
    
    [ContextMenu("Breed both into new head")]

    public void BreedBoth()
    {
        GhoulHead newHead = Instantiate(prefab,Vector3.right*i*6,quaternion.identity);
        newHead.LoadStatsIntoHead(new GhoulBaseStats(firstHead.MakeStatsFromHead(), secondHead.MakeStatsFromHead()));
        i++;
    }

    
    
    
    
}
