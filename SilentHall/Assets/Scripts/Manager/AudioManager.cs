using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

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
    public AudioMixerGroup musicMixer, sfxMixer;

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

    public AudioSource PlayMusicAtPosition(string name, Vector3 position, float minRange, float maxRange, float spatialBlend)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);
        if (s != null)
        {
            // Use the prefab manager to instantiate a preconfigured audio source
            GameObject audioObject = Instantiate(PrefabManager.instance.tempAudioPrefab, position, Quaternion.identity);
            AudioSource audioSource = audioObject.GetComponent<AudioSource>();

            if (audioSource != null)
            {
                audioSource.clip = s.clip;
                audioSource.outputAudioMixerGroup = musicMixer;
                audioSource.volume = s.volume;
                audioSource.minDistance = minRange;
                audioSource.maxDistance = maxRange;
                audioSource.loop = s.loop;
                audioSource.spatialBlend = spatialBlend;
                audioSource.playOnAwake = true;
                audioSource.Play();

                //// Destroy after the clip finishes playing
                //Destroy(audioObject, s.clip.length);
                return audioSource;
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
        return null;
    }

    public AudioSource PlayMusicAtPosition(string name, Transform parent, float minRange, float maxRange, float spatialBlend)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);
        if (s != null)
        {
            // Use the prefab manager to instantiate a preconfigured audio source
            GameObject audioObject = Instantiate(PrefabManager.instance.tempAudioPrefab, parent);
            AudioSource audioSource = audioObject.GetComponent<AudioSource>();

            if (audioSource != null)
            {
                audioSource.clip = s.clip;
                audioSource.outputAudioMixerGroup = musicMixer;
                audioSource.volume = s.volume;
                audioSource.minDistance = minRange;
                audioSource.maxDistance = maxRange;
                audioSource.loop = s.loop;
                audioSource.spatialBlend = spatialBlend;
                audioSource.playOnAwake = true;
                audioSource.Play();

                //// Destroy after the clip finishes playing
                //Destroy(audioObject, s.clip.length);
                return audioSource;
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
        return null;
    }

    public void PlaySFXAtPosition(string name, Vector3 position, float minRange, float maxRange)
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
                audioSource.outputAudioMixerGroup = sfxMixer;
                audioSource.volume = s.volume;
                audioSource.minDistance = minRange;
                audioSource.maxDistance = maxRange;
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

    public void PlaySFXAtPosition(string name, Transform parent, float minRange, float maxRange)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);
        if (s != null)
        {
            // Use the prefab manager to instantiate a preconfigured audio source
            GameObject audioObject = Instantiate(PrefabManager.instance.tempAudioPrefab, parent);
            AudioSource audioSource = audioObject.GetComponent<AudioSource>();

            if (audioSource != null)
            {
                audioSource.clip = s.clip;
                audioSource.outputAudioMixerGroup = sfxMixer;
                audioSource.volume = s.volume;
                audioSource.minDistance = minRange;
                audioSource.maxDistance = maxRange;
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

    public float GetAudioLength(string audioName)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        return s.clip.length;
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
