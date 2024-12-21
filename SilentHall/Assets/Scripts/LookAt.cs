using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LookAt : MonoBehaviour
{
    public Transform player; // The player transform
    public Camera playerCamera; // Camera used to determine the player's view direction
    public float lookDistance = 7f; // Maximum distance to check if the player can see the object
    public float playerVisionAngle = 60f; // The angle of the player's vision
    public float rotationSpeed = 5f; // Speed at which the object rotates towards the player
    public bool canLook = false;
    public LayerMask playerLayer;

    private void Update()
    {
        // Check if the object should look at the player
        if (ShouldLookAtPlayer() && canLook)
        {
            RotateTowardsPlayer();
        }
    }

    private bool ShouldLookAtPlayer()
    {
        // Calculate distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > lookDistance)
        {
            return false; // Skip the checks if the player is out of range
        }

        // Check if the object is within the player's vision range
        if (distanceToPlayer <= lookDistance)
        {
            // Check if the player is looking at the object
            if (!PlayerIsLookingAtObject())
            {
                return true; // The object should look at the player
            }
        }

        return false; // The object should not look at the player
    }

    private bool PlayerIsLookingAtObject()
    {
        // Direction from the player to the object
        Vector3 directionToObject = (transform.position - player.position).normalized;

        // Calculate the angle between the player's view direction and the direction to the object
        float angleBetweenPlayerAndObject = Vector3.Angle(playerCamera.transform.forward, directionToObject);

        // Check if the object is within the player's field of view
        if (angleBetweenPlayerAndObject < playerVisionAngle / 2)
        {
            // Check if the object is visible (i.e., no obstructions in the way)
            if (!Physics.Raycast(playerCamera.transform.position, directionToObject, Vector3.Distance(player.position, transform.position), playerLayer))
            {
                return true; // Player is looking at the object
            }
        }

        return false; // Player is not looking at the object
    }

    private void RotateTowardsPlayer()
    {
        // Direction to the player
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        // Calculate the target rotation
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));

        // Smoothly rotate towards the player
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canLook = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canLook = false;
        }
    }
}
