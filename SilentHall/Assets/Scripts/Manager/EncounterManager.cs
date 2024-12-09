using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    public static EncounterManager instance;

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
            
        }
    }

    IEnumerator DissappearReappearTimer(float timer, GameObject go, bool tf1, bool tf2)
    {
        go.SetActive(tf1);
        yield return new WaitForSeconds(timer);
        go.SetActive(tf2);
    }
}
