using UnityEngine;
using Yarn.Unity;
using System.Collections.Generic;
using CsvHelper.Configuration.Attributes;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    public AudioSource bmgSource;
    public AudioSource sfxSource;

    [System.Serializable]
    public struct AudioData
    {
        public string idName;
        public AudioClip clip;
    }

    [Header("Audio Databases")]
    public List<AudioData> musicTracks;
    public List<AudioData> soundEffects;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [YarnCommand("play_music")]
    public static void PlayMusic(string trackName)
    {
        if (Instance == null) return;

        foreach (var track in Instance.musicTracks)
        {
            if (track.idName == trackName)
            {
                if (Instance.bmgSource.clip == track.clip && Instance.bmgSource.isPlaying) return;

                Instance.bmgSource.clip = track.clip;
                Instance.bmgSource.loop = true;
                Instance.bmgSource.Play();
                return;
            }
        }
        Debug.LogWarning($"Музичний трек '{trackName}' не знайдено!");
    }

    [YarnCommand("stop_music")]
    public static void StopMusic()
    {
        if (Instance != null) Instance.bmgSource.Stop();
    }

    [YarnCommand("play_sound")]
    public static void PlaySound(string soundName)
    {
        if (Instance == null) return;

        foreach (var sfx in Instance.soundEffects)
        {
            if (sfx.idName == soundName)
            {
                Instance.sfxSource.PlayOneShot(sfx.clip);
                return;
            }
        }
        Debug.LogWarning($"Звуковий ефект '{soundName}' не знайдено!");
    }
}
