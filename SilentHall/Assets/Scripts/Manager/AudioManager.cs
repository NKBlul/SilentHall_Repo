using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0, 1)]  public float volume = 1f;
    [Range(0, 1)] public float spatialBlend = 0f;
    public bool loop = false;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource musicSource, sfxSource;
    public Sound[] musicSounds, sfxSounds;

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

    private void Start()
    {
        PlayMusic("bgm");
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);
        if (s != null)
        {
            musicSource.clip = s.clip;
            musicSource.volume = s.volume;
            musicSource.loop = s.loop;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning($"Music with name {name} not found!");
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);
        if (s != null)
        {
            AudioSource.PlayClipAtPoint(s.clip, Camera.main.transform.position, s.volume);
        }
        else
        {
            Debug.LogWarning($"SFX with name {name} not found!");
        }
    }

    public void PlaySFXAtPosition(string name, Vector3 position, float range)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);
        if (s != null)
        {
            // Use the prefab manager to instantiate a preconfigured audio source
            GameObject audioObject = Instantiate(PrefabManager.instance.tempAudioPrefab, position, Quaternion.identity);
            AudioSource audioSource = audioObject.GetComponent<AudioSource>();

            if (audioSource != null)
            {
                audioSource.clip = s.clip;
                audioSource.volume = s.volume;
                audioSource.minDistance = range;
                audioSource.maxDistance = range * 2f;
                audioSource.Play();

                // Destroy after the clip finishes playing
                Destroy(audioObject, s.clip.length);
            }
            else
            {
                Debug.LogError("Audio source prefab is missing an AudioSource component!");
            }
        }
        else
        {
            Debug.LogWarning($"SFX with name {name} not found!");
        }
    }

    public void StopMusic(AudioSource source)
    {
        source.Stop();
    }

    public AudioClip GetClipByName(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name) ?? Array.Find(musicSounds, x => x.name == name);
        return s?.clip;
    }
}
