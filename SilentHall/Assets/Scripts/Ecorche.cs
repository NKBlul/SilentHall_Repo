using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ecorche : MonoBehaviour, IEvent
{
    public Transform newPos;
    public bool eventTriggered = false;

    private void Start()
    {
        if (!eventTriggered)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void TriggerEvent()
    {
        if (!eventTriggered)
        {
            eventTriggered = true;
            gameObject.SetActive(false);
            //GameObject newEcorche = Instantiate(PrefabManager.instance.ecorchePrefab, newPos);
        }
    }
}
