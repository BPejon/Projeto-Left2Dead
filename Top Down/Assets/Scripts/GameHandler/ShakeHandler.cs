using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MilkShake;

public class ShakeHandler: MonoBehaviour {

    //private static Shaker MyShaker;
    public enum Shake
    {
        pistolShake,
        shotgunShake,
        rifleShake,
        sniperShake,
        onHitShake,
    }

    //public void Start()
    //{
    //    Shaker Myshaker = GameObject.Find("CameraContainer").transform.GetChild(0).GetComponent<Shaker>();
    //}
    public static void PlayShake(Shake shake)
    {
        Debug.Log(shake);
        Shaker.ShakeAll(GetShakePreset(shake));
    }

    private static ShakePreset GetShakePreset( Shake shake)
    {
        foreach (GameAssets.ShakePresets shakePresetsLocal in GameAssets.Instance.ShakePresetArray){
            if(shakePresetsLocal.shake == shake){
                return shakePresetsLocal.shakePreset;
            }
        }
        Debug.LogError("Shake" + shake + "not found!");
        return null;
    }
}
