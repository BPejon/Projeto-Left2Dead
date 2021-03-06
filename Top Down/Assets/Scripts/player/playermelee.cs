﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermelee : MonoBehaviour
{
    

    [Space]
    [Header("melee")]
    public float newHitSize;
    public float ForceBack;
    public float TimeAttacking;
    public float TimeBTWAttackig;
    public CapsuleCollider2D m_collider;

    public GameObject bulletPrefab; 
    public int Damage;
    public float ForceBackEnemy;
    

    [Space]
    [Header("animation")]
    public Animator animator1;
    
    private PlayerGotHit plyGotScript;

    float TimeStart; 
    float TimeEnd;
    public bool isAttackingMelee;
    Vector2 inicial_size;

    GameObject WeaponH;

    
    // Start is called before the first frame update
    void Start()
    {
        WeaponH = gameObject.transform.GetChild(0).gameObject;
        plyGotScript = gameObject.GetComponent<PlayerGotHit>();
        TimeStart = -10;
        TimeEnd = -10;
        isAttackingMelee = false;
        inicial_size = new Vector2 (m_collider.size.x, m_collider.size.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (plyGotScript.isPlayerDead)
            return;

        if(Input.GetMouseButton(1) && !isAttackingMelee && Time.time - TimeEnd >= TimeBTWAttackig){
            SoundManager.PlaySound(SoundManager.Sound.meleeHit);
            isAttackingMelee = true;
            WeaponH.SetActive(false);
            TimeStart = Time.time;
            m_collider.size = (new Vector2(inicial_size.x,inicial_size.y)) * newHitSize;
        }
        
    }
    
    void FixedUpdate()
    {
        if(isAttackingMelee && Time.time - TimeStart > TimeAttacking){
            isAttackingMelee = false;
            WeaponH.SetActive(true);
            TimeEnd = Time.time;
            m_collider.size = (new Vector2(inicial_size.x,inicial_size.y));
        }

        animator1.SetBool("isMelee",isAttackingMelee);
    }

    

    

    

}
