using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class Cutscene
{
    public string cutsceneName;
    public PlayableAsset cutscene;
}

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager instance;
    public PlayableDirector playableDirector;
    public Cutscene[] cutsceneList;

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

    public void PlayCutscene(string cutsceneName)
    {
        PlayableAsset cutsceneAsset = GetCutscene(cutsceneName);
        if (cutsceneAsset != null)
        {
            playableDirector.playableAsset = cutsceneAsset;
            playableDirector.Play();
        }
        else
        {
            Debug.LogWarning($"Cutscene '{cutsceneName}' not found!");
        }
    }

    PlayableAsset GetCutscene(string cutsceneName)
    {
        foreach (var cutscene in cutsceneList)
        {
            // Check if the cutscene name matches
            if (cutscene.cutsceneName == cutsceneName)
            {
                return cutscene.cutscene; // Return the PlayableAsset if found
            }
        }

        return null;
    }
}
