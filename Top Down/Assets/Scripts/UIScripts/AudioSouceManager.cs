using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSouceManager : MonoBehaviour
{

    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(audioSource.gameObject);
    }

   
}
