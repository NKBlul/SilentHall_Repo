using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingItem : MonoBehaviour
{
    public bool needToBeLookedAt = false;
    [SerializeField] Vector3 offset;
    float duration = 2f;
    [SerializeField] LayerMask collideLayer;

    public void Move()
    {
        StartCoroutine(MoveToPosition(transform.position + offset, duration));
    }

    IEnumerator MoveToPosition(Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = transform.position; // Starting position of the object
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        transform.position = targetPosition; // Ensure the object reaches the target position
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            AudioManager.instance.PlaySFX("Broken Glass");
            Debug.Log("Broken");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Move();
        }
    }
}
