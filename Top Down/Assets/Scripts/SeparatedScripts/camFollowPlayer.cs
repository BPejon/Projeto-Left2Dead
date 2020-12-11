using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camFollowPlayer : MonoBehaviour
{
    public Transform player;
    public float off_set_z;

    // Start is called before the first frame update
    void Start()
    {   
        player = GameObject.Find("player").transform;
        off_set_z = transform.position.z;
        transform.position =  new Vector3(player.position.x, player.position.y, off_set_z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position =  new Vector3(player.position.x, player.position.y, off_set_z);
    }
}
