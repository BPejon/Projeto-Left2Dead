using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermelee : MonoBehaviour
{
    

  
    public float new_size; 
    public CapsuleCollider2D m_collider;
    Vector2 inicial_size;
    
    // Start is called before the first frame update
    void Start()
    {
        inicial_size = m_collider.size;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("e")){
            m_collider.size = (new Vector2(inicial_size.x,inicial_size.y)) * new_size;
        }
    }
    

}
