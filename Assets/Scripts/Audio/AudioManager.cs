using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The audio manager
/// </summary>
public static class AudioManager
{

    static AudioSource audioSource;
    static Dictionary<AudioClipName, AudioClip> audioClips =
        new Dictionary<AudioClipName, AudioClip>();

    /// <summary>
    /// Initializes the audio manager
    /// </summary>
    /// <param name="source">Audio source</param>
    public static void Initialize(AudioSource source)
    {
        audioSource = source;
        foreach(KeyValuePair<AudioClipName, string> pair in Game.ResourcesPath.AudioClips)
        {
            AudioClip audioClip = Resources.Load<AudioClip>(pair.Value);
            audioClips.Add(pair.Key, audioClip);
        }
    }

    /// <summary>
    /// Plays the audio clip with the given name
    /// </summary>
    /// <param name="name">Name of the audio clip to play</param>
    public static void Play(AudioClipName name)
    {
        audioSource.PlayOneShot(audioClips[name]);
    }

}
