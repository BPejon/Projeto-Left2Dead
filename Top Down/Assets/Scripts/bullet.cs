﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
  
    // se a bala entrar em contato com algo ela é destruida.
    void OnCollisionEnter2D(Collision2D  other) {
        Destroy(gameObject);
    }
}
