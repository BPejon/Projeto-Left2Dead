using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public static class SoundManager{

    //Enum que se refere aos sons
    public enum Sound {
        PistolShot,
        RealoadPistol,
        RifleSound,
        RealoadRifle,
    }
    public static void PlaySound(Sound sound)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(sound));
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        //Procura pelo som correspondente ao escolhido para tocar
        foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.Instance.soundAudioClipArray)
        {
            if(soundAudioClip.sound == sound){
                return soundAudioClip.audioClip;
            }
        }

        Debug.LogError("Sound" + sound + "not found");
        return null;
    }
}
