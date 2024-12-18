using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private Dictionary<string, GameObject> organPrefabs;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            organPrefabs = new Dictionary<string, GameObject>
            {
                { "Brain", brainPrefab },
                { "Heart", heartPrefab },
                { "Left Lung", leftLungPrefab },
                { "Right Lung", rightLungPrefab },
                { "Liver", liverPrefab }
            };
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject GetOrganPrefab(string organName)
    {
        if (organPrefabs.TryGetValue(organName, out GameObject prefab))
        {
            return prefab;
        }
        Debug.LogWarning($"Prefab for organ '{organName}' not found!");
        return null;
    }
}
