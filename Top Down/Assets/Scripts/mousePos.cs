using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mousePos : MonoBehaviour
{
    [Space]
    [Header ("crosshair : ")]
    public Camera cam;              
    
    Vector2 mousePosi;   
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePosi = cam.ScreenToWorldPoint(Input.mousePosition);
    }
    void FixedUpdate()
    {
        gameObject.transform.position = mousePosi;

    }
}
