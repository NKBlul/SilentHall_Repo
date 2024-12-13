using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINumLock : MonoBehaviour
{
    public string password;
    public string currentRotation;

    private void Update()
    {
        
    }
    
    public bool CheckCombination()
    {
        return password == currentRotation;
    }
}
