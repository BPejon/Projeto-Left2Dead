using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MilkShake;
public class Shake : MonoBehaviour
{
    public Shaker MyShaker;
    public ShakePreset ShakePreset;
    //The saved shake instance we will be modifying
    private ShakeInstance myShakeInstance;

    private void Update()
    {
        //Start the shake, with a fade-in time of 1 second.
        if (Input.GetKeyDown(KeyCode.Q))
            MyShaker.Shake(ShakePreset);

    }
}
