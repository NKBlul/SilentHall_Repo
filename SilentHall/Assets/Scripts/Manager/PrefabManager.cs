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
    public GameObject eyePrefab;

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
                { "Eye", eyePrefab}
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
}
