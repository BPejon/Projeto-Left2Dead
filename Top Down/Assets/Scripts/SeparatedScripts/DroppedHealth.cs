using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedHealth : MonoBehaviour
{

    public HealthFloor hp;

    public int health;

    public string iname;

    void Awake(){
        this.gameObject.GetComponent<SpriteRenderer>().sprite = hp.hpprite;
        health = hp.health;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = hp.hpprite;
        health = hp.health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D other){
         
        if(other.gameObject.CompareTag("Player")){
            GameObject _player = other.gameObject;
            //Se o jogador tomou dano;
            if(_player.GetComponent<PlayerGotHit>().health < _player.GetComponent<PlayerGotHit>().maxHealth){
                _player.GetComponent<PlayerGotHit>().health += health;
                if(_player.GetComponent<PlayerGotHit>().health > _player.GetComponent<PlayerGotHit>().maxHealth){
                    _player.GetComponent<PlayerGotHit>().health = _player.GetComponent<PlayerGotHit>().maxHealth;
                }

                _player.GetComponent<PlayerGotHit>().healthBar.SetHealth( _player.GetComponent<PlayerGotHit>().health);
                Destroy(this.gameObject);
            }

        }
        
    }

   

    
}
