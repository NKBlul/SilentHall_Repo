using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    [SerializeField] Light lights;
    float minFlickerTime = 0.1f;
    float maxFlickerTime = 1.0f;

    private void Start()
    {
        StartCoroutine(FlickerLight());
    }

    void Update()
    {
        
    }

    IEnumerator FlickerLight()
    {
        lights.enabled = true;
        yield return new WaitForSeconds(Random.Range(minFlickerTime, maxFlickerTime));
        lights.enabled = false;
        yield return new WaitForSeconds(Random.Range(minFlickerTime, maxFlickerTime));
        StartCoroutine(FlickerLight());
    }
}
