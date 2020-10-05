using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    //referencia ao objeto
    public Transform firePoint;

    //referencia ao objeto da bala
    public GameObject bulletPrefab;

    //Controla a força da bala
    public float bulletForce = 20f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot(){
        // O objeto bala vai ser clonado do bulletPrefab e vai aparecer na posição e rotação abaixo
        GameObject bala = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bala.GetComponent<Rigidbody2D>(); //a bala possui rigid body
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
