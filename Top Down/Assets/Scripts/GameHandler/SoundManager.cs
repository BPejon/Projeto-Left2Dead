using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager{


    //Para usar o script adicione SoundManager.PlaySound(SoundManager.Sound."Som");
    //3D SoundManager.PlaySound(SoundManager.Sound."Som", GetPosition());
    public enum Sound{
        pistolShot,
        pistolReload,
        shotgunShot,
        shotgunReload,
        sniperShot,
        sniperReload,
        sniperClick,
        playerMove,
        dash,
        grabLife,
        meleeHit,
    }

   

    //Dicionário para analisar casos especificos de tempo para reproduzir um som
    private static Dictionary<Sound, float> soundTimerDictionary;
    //Criar apenas um game object pra sons
    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;

    public static void Initialize()
    {
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.playerMove] = 0f;
    }

    //Som em 3D
    public static void PlaySound(Sound sound, Vector3 position)
    {
        if (CanPlaySound(sound) == true){
            GameObject soundGameObject = new GameObject("Sound");
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.clip = GetAudioClip(sound);
            audioSource.maxDistance = 100f;
            audioSource.spatialBlend = 1f;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.dopplerLevel = 0f;
            audioSource.Play();

            Object.Destroy(soundGameObject, audioSource.clip.length);
        }
    }
    
    //Som em 2D
    public static void PlaySound(Sound sound) {
        if (CanPlaySound(sound) == true){
            if(oneShotGameObject == null)
            {
                oneShotGameObject = new GameObject("One Shot Sound");
                oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
                //oneShotAudioSource.outputAudioMixerGroup("MainMixer");
            }
            
            oneShotAudioSource.PlayOneShot(GetAudioClip(sound));
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
