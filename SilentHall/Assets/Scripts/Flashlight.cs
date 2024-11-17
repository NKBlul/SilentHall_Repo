using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour, IUseable
{
    [SerializeField] Light spotLight;
    bool isOn;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Use()
    {
        if (isOn) 
        {
            spotLight.enabled = false;
            isOn = false;
        }
        else
        {
            spotLight.enabled = true;
            isOn = true;
        }
    }
}
