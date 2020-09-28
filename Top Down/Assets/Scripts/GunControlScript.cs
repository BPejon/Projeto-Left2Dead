using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControlScript : MonoBehaviour
{
    //weaponUse:
    /*
        true - this weapon is in use
        false - this weapon is not in use
    */
    public bool weaponuse = false;
    
    //Vector of the camera/aim
    public Vector3 camvec;

    //Transform for camera Angles
    //private Transform aimTransform;
    


    



    // Start is called before the first frame update
    void Start()
    {
        //if we are not using this weapon: hide its sprite
        gameObject.GetComponent<SpriteRenderer>().enabled = weaponuse;
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = weaponuse;

        AimAngle();
        
    }

    public void AimAngle(){
        camvec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        camvec.z = 0;
        camvec = (camvec - transform.position).normalized;
        float angle = Mathf.Atan2(camvec.y,camvec.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0,0,angle);

    }

}
