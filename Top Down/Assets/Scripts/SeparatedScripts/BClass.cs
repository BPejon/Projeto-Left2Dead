using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BClass : MonoBehaviour
{
    //Classe Base para todos os tipos de balas, ela guarda um KBReport para quando ocorrer o impacto
    //Esse KBReport deverá ser passado para o inimigo. 
    public KBReport kb;

    public float kbspeed;

    public float kbdur;
    public int damage;

    public GameObject bullet;

    // Start is called before the first frame update
    void Instance(){
        bullet = this.gameObject;
        kb = new KBReport();

        kb.kbdur = kbdur;
        kb.kbspeed = kbspeed;
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
