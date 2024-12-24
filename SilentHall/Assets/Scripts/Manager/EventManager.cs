using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

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

    public void TriggerEvent(string eventName, GameObject triggers = null)
    {
        switch (eventName) 
        {
            case "event 1":
                GameObject stmuerte = Instantiate(PrefabManager.instance.stmuertePrefab, triggers.transform);
                break;
            case "event 2":
                Destroy(triggers);
                break;
            case "event 3":
                AudioManager.instance.PlayMusicAtPosition("creepyPiano", new Vector3(-44, 1, -14f), 1, 5, 1);
                break;
            case "event 4":
                break;
            case "event 5":
                break;
            default: 
                break;
        }
    }

    IEnumerator DissappearReappearTimer(float timer, GameObject go, bool tf1, bool tf2)
    {
        go.SetActive(tf1);
        yield return new WaitForSeconds(timer);
        go.SetActive(tf2);
    }
}
