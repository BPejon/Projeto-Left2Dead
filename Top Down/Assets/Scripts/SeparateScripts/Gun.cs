using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bullet;

    public int maxammo;

    public SpriteRenderer gunModel;

    public GameObject barrel;
    
    public float bulletspeed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public int Shoot(Vector3 aim){

        //transform.rotation
        GameObject nbullet = Instantiate(bullet,barrel.transform.position,transform.rotation);
        Rigidbody2D rbb = nbullet.GetComponent<Rigidbody2D>();
        rbb.AddForce(aim * bulletspeed, ForceMode2D.Impulse);
        Destroy(nbullet, 2.0f);

        return 0;
    }
}
