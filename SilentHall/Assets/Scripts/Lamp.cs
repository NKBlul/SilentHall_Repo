using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    [SerializeField] Light light;
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
        light.enabled = true;
        yield return new WaitForSeconds(Random.Range(minFlickerTime, maxFlickerTime));
        light.enabled = false;
        yield return new WaitForSeconds(Random.Range(minFlickerTime, maxFlickerTime));
        StartCoroutine(FlickerLight());
    }
}
