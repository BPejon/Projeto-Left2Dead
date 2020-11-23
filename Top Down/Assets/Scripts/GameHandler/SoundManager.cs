using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager{ 


    //Para usar o script adicione SoundManager.PlaySound(SoundManager.Sound."Som",local do som);
    public enum Sound{
        pistolShot,
        pistolReload,
        shotgunShot,
        shotgunReload,
        playerMove,
    }

   

    //Dicionário para analisar casos especificos de tempo para reproduzir um som
    private static Dictionary<Sound, float> soundTimerDictionary;

    public static void Initialize()
    {
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.playerMove] = 0f;
    }

    public static void PlaySound(Sound sound) {
        if (CanPlaySound(sound) == true){
            GameObject soundGameObject = new GameObject("Sound");
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.PlayOneShot(GetAudioClip(sound));
        }
    }

    //Função que analisa se o som reproduzira de forma alternativa
    private static bool CanPlaySound(Sound sound)
    {
        float playerMoveTimeMax = 0.05f; 
        switch (sound){
            default:
                return true;

            case Sound.playerMove:
                if (soundTimerDictionary.ContainsKey(sound)){
                    float lastTimePlayed = soundTimerDictionary[sound];
                    if (lastTimePlayed + playerMoveTimeMax < Time.time){
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else{
                    return false;
                }         
        }
    }
    //Função que pega o audio que queremos executar
    private static AudioClip GetAudioClip(Sound sound) {
        foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.Instance.soundAudioClipArray){
            if(soundAudioClip.sound == sound){
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound" + sound + "not found!");
        return null;
    }

}
