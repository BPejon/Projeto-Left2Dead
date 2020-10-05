using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioButton : MonoBehaviour
{
    public AudioSource meuFx;
    public AudioClip hoverFx;
    public AudioClip clickFx;

    public void HoverSound()
    {
        meuFx.PlayOneShot(hoverFx);
    }

    public void ClickSound()
    {
        meuFx.PlayOneShot(clickFx);
    }
}
