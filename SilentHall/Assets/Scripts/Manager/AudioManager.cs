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
            AudioSource audioSource = new GameObject("TempAudio").AddComponent<AudioSource>();
            audioSource.clip = s.clip;
            audioSource.volume = s.volume;
            audioSource.spatialBlend = 1f; // Enable 3D sound
            audioSource.minDistance = range; // Adjust based on range
            audioSource.maxDistance = range * 2f; // Example adjustment
            audioSource.transform.position = position;
            audioSource.Play();

            Destroy(audioSource.gameObject, s.clip.length);
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
