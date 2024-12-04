using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    public static EncounterManager instance;

    [SerializeField] GameObject stMuerte1;
    [SerializeField] GameObject ecorche;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TriggerEvent(string eventName)
    {
        switch (eventName) 
        {
            case "event 1":
                stMuerte1.SetActive(false);
                break;
            case "event 2":
                break;
            case "event 3":
                break;
            default:
                break;
        }
    }
}
