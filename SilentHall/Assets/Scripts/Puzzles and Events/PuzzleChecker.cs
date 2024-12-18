using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleChecker : MonoBehaviour
{
    public List<GameObject> puzzles = new List<GameObject>();
    public int currentOrgan = 0;
    public int requiredOrgans = 3;
    public void CheckOrganPuzzle()
    {
        if (currentOrgan == requiredOrgans)
        {
            //GameManager.instance.playerRef.pickable.Add();
        }
    }
}
