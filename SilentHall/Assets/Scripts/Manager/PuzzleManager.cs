using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager instance;

    public List<GameObject> organPuzzles = new List<GameObject>();
    public List<GameObject> puzzles = new List<GameObject>();
    public List<GameObject> jars = new List<GameObject>();
    public int organPlaced = 0;
    public int correctOrgan = 0;
    int failureCount = 0;
    public int requiredOrgans = 3;



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

    public void CheckOrganPuzzle()
    {
        if (organPlaced == requiredOrgans)
        {
            Debug.Log($"organPlaced: {organPlaced}, correctOrgan: {correctOrgan}");
            if (correctOrgan == requiredOrgans)
            {
                GameManager.instance.playerRef.pickable.Add("MLKey");
                UIManager.instance.ChangeText(2f, UIManager.instance.extraText, $"You received the Music lab key");
                GameObject tempEncZone = PrefabManager.instance.InstantiatePrefab("TempEnc", new Vector3(-34.5f, 0.5f, -29), Quaternion.identity);
                TempEncounterZone tempEncounter = tempEncZone.GetComponent<TempEncounterZone>();
                tempEncounter.eventName = "event 3";
            }
            else if (correctOrgan < organPlaced)
            {
                Debug.Log("Fail");
                ResetPuzzle();
            }
        }
    }

    public void ResetPuzzle()
    {
        foreach (GameObject puzzle in organPuzzles)
        {
            OrganPuzzle organPuzzle = puzzle.GetComponent<OrganPuzzle>();
            if (organPuzzle != null && organPuzzle.organPlaced)
            {
                organPuzzle.organPlaced = false;

                // Clear the organ at the organPos
                foreach (Transform child in organPuzzle.organPos)
                {
                    Destroy(child.gameObject);
                }
            }
        }

        foreach (GameObject jar in jars)
        {
            Jar organJar = jar.GetComponent<Jar>();
            if (organJar != null)
            {
                organJar.ResetJar();
            }
        }

        organPlaced = 0;
        correctOrgan = 0;
        OnFailure();
    }

    void OnFailure()
    {
        failureCount++;
        switch (failureCount)
        {
            case 1:
                TriggerGameOver();
                break;

            case 2:
                TriggerGameOver();
                break;

            case 3:
                TriggerGameOver();
                break;

            default:
                break;
        }
    }

    void TriggerGameOver()
    {

    }
}
