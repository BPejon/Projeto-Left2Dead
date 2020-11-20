using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    //Classe para instanciar game objects

    private static GameAssets _Instance;

    public static GameAssets Instance{
        get{
            if (_Instance == null){
                _Instance = Instantiate(Resources.Load<GameAssets>("GameAssets"));
                Debug.Log("instanciou");
            }
            return _Instance;
        }
    }

    //Instancia Sons
    //vetor que armazena todos os sons
    public SoundAudioClip[] soundAudioClipArray;

    [System.Serializable]
    public class SoundAudioClip{
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }
}
