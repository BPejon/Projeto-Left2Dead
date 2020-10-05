using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationSystem : MonoBehaviour
{
    public enum Language{Portugues,English}

    //Linguagem Padrão
    public static Language language = Language.Portugues;

    //Dicionários para cada língua
    private static Dictionary<string, string> localizedPT;
    private static Dictionary<string, string> localizedEN;

    //Inicalizar todos os valores
    public static bool isInit;

    public static void Init()
    {
        CSVLoader csvLoader = new CSVLoader();
        csvLoader.LoadCSV();

        localizedPT = csvLoader.GetDictionaryValues("pt-br");
        localizedEN = csvLoader.GetDictionaryValues("en-us");

        isInit = true;
    }

    //Retornar os valores baseados nas chaves pedidas
    public static string GetLocalizedValue(string key)
    {
        if (!isInit){
            Init();
        }

        string value = key;

        switch (language)
        {
            case Language.Portugues:
                localizedPT.TryGetValue(key, out value);
                break;
            case Language.English:
                localizedEN.TryGetValue(key, out value);
                break;
        }

        return value;
    }
}
