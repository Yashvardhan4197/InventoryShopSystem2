using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : GenericMonoSingleton<SoundManager>
{
    [SerializeField] AudioSource SoundSFX;
    [SerializeField] SoundTypes[] soundTypes;
    public void PlaySound(Sound sound)
    {
        AudioClip clip = GetAudioClip(sound);
        if(clip!= null)
        {
            SoundSFX.PlayOneShot(clip);
        }
    }

    public AudioClip GetAudioClip(Sound sound)
    {
        SoundTypes item= Array.Find(soundTypes, i => i.sound == sound);
        if (item != null)
        {
            return item.clip;
        }
        return null;
    }
}
[Serializable]
public class SoundTypes
{
    public Sound sound;
    public AudioClip clip;
}

public enum Sound { Deny,Accept,Open,Close}