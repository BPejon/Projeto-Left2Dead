using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class TextLocalizerUI : MonoBehaviour
{
    TextMeshProUGUI textField;

    public string key;  //palavra
    // Quando Startar, colocarar o valor da string no Texto
    void Start()
    {
        textField = GetComponent<TextMeshProUGUI>();
        string value = LocalizationSystem.GetLocalizedValue(key);
        textField.text = value;
        
    }
}
