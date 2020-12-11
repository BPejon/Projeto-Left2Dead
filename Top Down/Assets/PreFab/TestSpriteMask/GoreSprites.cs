using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoreSprites : MonoBehaviour
{
   
    public static GoreSprites Instance {get; private set;}

    private void Awake(){
        Instance = this;
    }

    public Sprite[] pools;

    public Sprite[] splatters;

}
