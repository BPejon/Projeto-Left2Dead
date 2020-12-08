using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeSetter : MonoBehaviour
{
    // Start is called before the first frame update
    // Script Minusculo, usado na primeira cena do jogo.
    //Isto é uma versao beeeeeeem folgada, nao se preocupem em manter

    //Como implementar "save" no jogo --> Fazer se der tempo.
    void Awake(){
        PlayerPrefs.SetInt("use_default", 1);
        PlayerPrefs.SetInt("use_position", 0);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
