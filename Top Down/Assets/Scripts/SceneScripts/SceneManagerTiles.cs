using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerTiles : MonoBehaviour
{
    public string sceneName;
    public int sceneID;
    public GameObject player;

    public float x_position;

    public float y_position;

    public bool usingPosition;
    public bool usingID;

    void OnTriggerEnter2D(Collider2D col){

        if(col.CompareTag("Player")){
            if(usingID){
                SceneManager.LoadScene(sceneID);
                //Aqui setamos tudo que precisamos pegar do player;
                //Precisamos do HP do jogador, das armas empunhadas, sua municao;
                GameObject weaponholder = player.transform.GetChild(0).gameObject;
                
                GeneralWeaponScript belt = weaponholder.GetComponent<GeneralWeaponScript>();

                //municao fora da arma
                PlayerPrefs.SetInt("ammo_weapon_1", belt.gunTypes[belt.Belt[0]].GetComponent<Gun>().ammo);
                PlayerPrefs.SetInt("curammo_weapon_1",belt.gunTypes[belt.Belt[0]].GetComponent<Gun>().curammo);

                //municao na arma
                PlayerPrefs.SetInt("ammo_weapon_2", belt.gunTypes[belt.Belt[1]].GetComponent<Gun>().ammo);
                PlayerPrefs.SetInt("curammo_weapon_2",belt.gunTypes[belt.Belt[1]].GetComponent<Gun>().curammo);
                
                //quais sao as armas
                PlayerPrefs.SetInt("weaponindex_1", belt.Belt[0]);
                PlayerPrefs.SetInt("weaponindex_2", belt.Belt[1]);

                //qual esta sendo segurada
                PlayerPrefs.SetInt("held", belt.held);
                //E a oposta
                PlayerPrefs.SetInt("_other", belt._other);


                //Quanta vida o nosso jogador ainda tem:
                PlayerPrefs.SetInt("hp", player.GetComponent<PlayerGotHit>().health);
                PlayerPrefs.SetInt("use_default", 0);

                //Quardamos uso de posicao;
                if(usingPosition){
                    PlayerPrefs.SetInt("use_position", 1);
                }
                else{
                    PlayerPrefs.SetInt("use_position", 0);
                }

                PlayerPrefs.SetFloat("x_position", x_position);
                PlayerPrefs.SetFloat("y_position", y_position);
                
            }
            else{
                SceneManager.LoadScene(sceneName);

                //Aqui setamos tudo que precisamos pegar do player;
                //Precisamos do HP do jogador, das armas empunhadas, sua municao;
                GameObject weaponholder = player.transform.GetChild(0).gameObject;
                
                GeneralWeaponScript belt = weaponholder.GetComponent<GeneralWeaponScript>();

                //municao fora da arma
                PlayerPrefs.SetInt("ammo_weapon_1", belt.gunTypes[belt.Belt[0]].GetComponent<Gun>().ammo);
                PlayerPrefs.SetInt("curammo_weapon_1",belt.gunTypes[belt.Belt[0]].GetComponent<Gun>().curammo);

                //municao na arma
                PlayerPrefs.SetInt("ammo_weapon_2", belt.gunTypes[belt.Belt[1]].GetComponent<Gun>().ammo);
                PlayerPrefs.SetInt("curammo_weapon_2",belt.gunTypes[belt.Belt[1]].GetComponent<Gun>().curammo);
                
                //quais sao as armas
                PlayerPrefs.SetInt("weaponindex_1", belt.Belt[0]);
                PlayerPrefs.SetInt("weaponindex_2", belt.Belt[1]);

                //qual esta sendo segurada
                PlayerPrefs.SetInt("held", belt.held);
                //E a oposta
                PlayerPrefs.SetInt("_other", belt._other);


                //Quanta vida o nosso jogador ainda tem:
                PlayerPrefs.SetInt("hp", player.GetComponent<PlayerGotHit>().health);
                PlayerPrefs.SetInt("use_default", 0);
            }
        }
    }

}
