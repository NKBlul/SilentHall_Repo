using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PrefabManager : MonoBehaviour
{
    public static PrefabManager instance;

    public GameObject ecorchePrefab;
    public GameObject skeletonPrefab;
    public GameObject stmuertePrefab;
    public GameObject tempAudioPrefab;
    public GameObject brainPrefab;
    public GameObject heartPrefab;
    public GameObject leftLungPrefab;
    public GameObject rightLungPrefab;
    public GameObject liverPrefab;
    public GameObject eyePrefab;
    public GameObject stomachPrefab;
    public GameObject intestinePrefab;
    public GameObject tempEncounterZonePrefab;
    public GameObject kuntilAnakPrefab;

    private Dictionary<string, GameObject> prefabs;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            prefabs = new Dictionary<string, GameObject>
            {
                { "Ecorche", ecorchePrefab },
                { "Skeleton", skeletonPrefab},
                { "StMuerte", stmuertePrefab},
                { "Brain", brainPrefab },
                { "Heart", heartPrefab },
                { "Left Lung", leftLungPrefab },
                { "Right Lung", rightLungPrefab },
                { "Liver", liverPrefab },
                { "Eyes", eyePrefab},
                { "Stomach", stomachPrefab},
                { "Intestine", intestinePrefab},
                { "TempEnc", tempEncounterZonePrefab},
                { "TempAudio", tempAudioPrefab},
                { "Kunti", kuntilAnakPrefab},

            };
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject GetPrefab(string organName)
    {
        if (prefabs.TryGetValue(organName, out GameObject prefab))
        {
            return prefab;
        }
        Debug.LogWarning($"Prefab for organ '{organName}' not found!");
        return null;
    }

    public GameObject InstantiatePrefab(string prefabName, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        // Attempt to retrieve the prefab using the organ name
        GameObject prefab = GetPrefab(prefabName);

        if (prefab != null)
        {
            // Instantiate the prefab at the specified position and rotation
            return Instantiate(prefab, position, rotation, parent);
        }

        Debug.LogWarning($"Failed to instantiate prefab for '{prefabName}' because it was not found.");
        return null;
    }

    public GameObject InstantiatePrefab(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null)
    {

        if (prefab != null)
        {
            // Instantiate the prefab at the specified position and rotation
            return Instantiate(prefab, position, rotation, parent);
        }

        Debug.LogWarning($"Failed to instantiate prefab for '{prefab}' because it was not found.");
        return null;
    }

    public GameObject InstantiatePrefab(string prefabName, Transform parent)
    {
        GameObject prefab = GetPrefab(prefabName);

        if (prefab != null)
        {
            // Instantiate the prefab at the specified position and rotation
            return Instantiate(prefab, parent);
        }

        Debug.LogWarning($"Failed to instantiate prefab for organ '{prefabName}' because it was not found.");
        return null;
    }

    public GameObject InstantiatePrefab(GameObject prefab, Transform parent)
    {
        if (prefab != null)
        {
            // Instantiate the prefab at the specified position and rotation
            return Instantiate(prefab, parent);
        }

        Debug.LogWarning($"Failed to instantiate prefab for '{prefab}' because it was not found.");
        return null;
    }
}
