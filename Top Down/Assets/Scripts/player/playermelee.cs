using System.Collections;
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
    

    float TimeStart; 
    float TimeEnd;
    public bool isAttackingMelee;
    Vector2 inicial_size;


    
    // Start is called before the first frame update
    void Start()
    {
        TimeStart = -10;
        TimeEnd = -10;
        isAttackingMelee = false;
        inicial_size = new Vector2 (m_collider.size.x, m_collider.size.y);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetMouseButton(1) && !isAttackingMelee && Time.time - TimeEnd >= TimeBTWAttackig){
            isAttackingMelee = true;
            TimeStart = Time.time;
            m_collider.size = (new Vector2(inicial_size.x,inicial_size.y)) * newHitSize;
        }
        
    }
    
    void FixedUpdate()
    {
        if(isAttackingMelee && Time.time - TimeStart > TimeAttacking){
            isAttackingMelee = false;
            TimeEnd = Time.time;
            m_collider.size = (new Vector2(inicial_size.x,inicial_size.y));
        }

        animator1.SetBool("isMelee",isAttackingMelee);
    }

    

    // se a bala entrar em contato com algo ela é destruida.
    void OnCollisionEnter2D(Collision2D  other) {
    }

}
