using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    public static EncounterManager instance;

    [SerializeField] GameObject stMuerte1;
    [SerializeField] GameObject shadowyFigure1;
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
                StartCoroutine(DissappearReappearTimer(0.75f, stMuerte1, true, false));
                break;
            case "event 2":
                StartCoroutine(DissappearReappearTimer(0.75f, shadowyFigure1, true, false));
                break;
            case "event 3":
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
