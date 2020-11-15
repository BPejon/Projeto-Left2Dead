using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    Resolution[] resolutions;
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown qualityDropdown;
    //Recuperar os dados do pc q estará executando o jogo
    private void Start()
    {
        resolutions = Screen.resolutions; //pega um vetor de resoluções
        //Queremos adicionar todas as resoluções disponíveis no dropdown do menu 
        //Primeiramente, limpar o dropdown.
        //Em seguida, pegar o vetor de resoluções e transformá-lo em string 
        //Finalmente colocar as strings no dropdown e integrar com o jogo
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for(int i=0; i<resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            //comparar com a resolução atual do monitor para colocar como padrão
            if(resolutions[i].width == Screen.width &&
               resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        //Mostrar a resolução padrão nas opções
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        //Mostra a qualidade correta nas configs
        int qualityIndex = QualitySettings.GetQualityLevel();
        qualityDropdown.value = qualityIndex;
    }

    //ALTERAR RESOLUÇÃO
    public void setResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    //ALTERAR VOLUME
    //Referencia ao mixer
    public AudioMixer audioMixer;
    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume", volume); //Alterar a variavel exposta "volume" para o float volume
        Debug.Log(volume);
    }


    //ALTERAR QUALIDADE DE IMAGEM
    public void setQuality(int qualityIndex)
    {
        Debug.Log(qualityIndex);
        QualitySettings.SetQualityLevel(qualityIndex); //Altera a qualidade visual baseado no quality predeterminado nas configurações
    }


    //ALTERAR PARA FULLSCREEN
    public void SetFullscren (bool isFullscreen)
    {
        Debug.Log(isFullscreen);
        Screen.fullScreen = isFullscreen;
    }
}
