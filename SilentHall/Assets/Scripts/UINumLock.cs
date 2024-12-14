using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINumLock : MonoBehaviour
{
    public GameObject numLock;
    public string password;
    public string currentRotation;
    
    public void GetCombination()
    {
        currentRotation = "";
        foreach (Transform child in transform)
        {
            Gear gear = child.GetComponent<Gear>();
            if (gear != null) // Make sure the child has a Gear component.
            {
                currentRotation += gear.gearIndex.ToString(); // Append each gear's index to the combination.
            }
        }
        Debug.Log($"Current Combination: {currentRotation}");
        if (CheckCombination())
        {
            Debug.Log($"Correct Password");
        }
    }

    public bool CheckCombination()
    {
        return password == currentRotation;
    }
}
