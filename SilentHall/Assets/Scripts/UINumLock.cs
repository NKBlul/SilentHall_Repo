using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINumLock : MonoBehaviour
{
    public GameObject numLock;
    DDoor door;
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
            UIManager.instance.CloseNumlock();

            door = numLock.GetComponent<NumLock>().door;
            if (door == null) 
            {
                Debug.Log("Door not found");
            }
            if (door != null)
            {
                door.UnlockAndOpen();
            }
            Destroy(numLock);
        }
    }

    public bool CheckCombination()
    {
        return password == currentRotation;
    }
}
