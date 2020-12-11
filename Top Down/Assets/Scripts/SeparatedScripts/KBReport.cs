using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KBReport
{
    //Classe/Struct para Report de dano;
    public static KBReport Instance {get; set;}

    public Vector3 dir;
    public float kbspeed;
    public float kbdur;

    public int status;

}
