using System;
using UnityEngine;

public class SoundService
{
    private AudioSource soundSFX;
    private SoundTypes[] soundTypes;

    public SoundService(AudioSource audioSource, SoundTypes[] soundTypes) 
    {
        soundSFX = audioSource;
        this.soundTypes = soundTypes;
    }
    public void PlaySound(Sound sound)
    {
        AudioClip clip = GetAudioClip(sound);
        if(clip!= null)
        {
            soundSFX.PlayOneShot(clip);
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

