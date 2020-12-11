using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MilkShake;

public class GameAssets : MonoBehaviour
{

    private static GameAssets _Instance;

    public static GameAssets Instance
    {
        get{
            if (_Instance == null) {
                _Instance = Instantiate(Resources.Load<GameAssets>("GameAssets"));
           
            }
            return _Instance;

        }
    }

    //Clipes de audio
    public SoundAudioClip[] soundAudioClipArray;

    [System.Serializable]
    public class SoundAudioClip{
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }
    


    //Sprites Armas do Menu
    public ImageGunsUI[] imageGunsUIArray;

    [System.Serializable]
    public class ImageGunsUI{
        public UIManager.GunImage gunImage ;
        public Sprite gunSprite;

    }

    //Shakes das Armas
    public ShakePresets[] ShakePresetArray;

    [System.Serializable]
    public class ShakePresets
    {
        public ShakeHandler.Shake shake ;
        public ShakePreset shakePreset;

    }

}