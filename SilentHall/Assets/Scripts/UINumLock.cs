using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINumLock : MonoBehaviour
{
    public string password;
    public string currentRotation;
    public List<Button> gearButton = new List<Button>();

    private void OnEnable()
    {
        foreach (Button button in gearButton)
        {
            button.onClick.AddListener(RotateGear);
        }
    }

    public void RotateGear()
    {
        Debug.Log("HELO");
    }
    
    public bool CheckCombination()
    {
        return password == currentRotation;
    }
}
