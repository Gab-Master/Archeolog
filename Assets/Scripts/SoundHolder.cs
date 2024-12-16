using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class SoundHolder : MonoBehaviour
{
    [SerializeField] private List<AudioClipsHolder> soundsHolderList;
    [SerializeField] private List<AudioClipsListsHolder> soundsListsHolderList;
    private Dictionary<string, AudioClip> soundsHolder;
    private Dictionary<string, List<AudioClip>> listsSoundHolder;

    private void Awake()
    {
        soundsHolder = new();
        listsSoundHolder = new();
        foreach (AudioClipsHolder holder in soundsHolderList)
        {
            soundsHolder[holder.SoundName] = holder.AudioSound;
        }
        foreach (AudioClipsListsHolder holder in soundsListsHolderList)
        {
            listsSoundHolder[holder.SoundsListName] = holder.AudioSoundList;
        }
    }

    public AudioClip GetAudioClip(string soundName)
    {
        return soundsHolder[soundName];
    }
    public List<AudioClip> GetAudioList(string listname)
    {
        return listsSoundHolder[listname];
    }
}

[Serializable] //normally you can't see dictionary in inspector :(
public class AudioClipsHolder
{
    [SerializeField] private string soundName;
    [SerializeField] private AudioClip audioSound;

    public string SoundName => soundName;
    public AudioClip AudioSound => audioSound;
}

[Serializable]
public class AudioClipsListsHolder
{
    [SerializeField] private string soundsListName;
    [SerializeField] private List<AudioClip> audioSoundList;

    public string SoundsListName => soundsListName;
    public List<AudioClip> AudioSoundList => audioSoundList;
}
