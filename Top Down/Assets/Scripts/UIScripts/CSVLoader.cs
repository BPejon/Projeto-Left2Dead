using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;
//using UnityEditor.Callbacks;

//Script para carregar o arquivo CSV corretamente

public class CSVLoader
{
    //Reference file
    private TextAsset csvFile;
    private char lineSeparator = '\n';
    private char surround = '"';
    private string[] fieldSeparator = { "\", \"" }; //permitir \ e "" dentro do texto

    public void LoadCSV()
    {
        csvFile = Resources.Load<TextAsset>("Localizacao");

    }

    //Criar um dicionario para referir um código a uma palavra na lingua escolhida
    public Dictionary<string,string> GetDictionaryValues(string attributeId){
        Dictionary<string, string> dictionary = new Dictionary<string, string>();

        //quebrar o texto 
        string[] lines = csvFile.text.Split(lineSeparator);

        int attributeIndex = -1;

        //Separar o cabeçalho 
        string[] headers = lines[0].Split(fieldSeparator, System.StringSplitOptions.None);

        for(int i=0; i<headers.Length; ++i)
        {
            if (headers[i].Contains(attributeId))
            {
                attributeIndex = i;
                break;
            }
        }

        //Definir parametros de split das linhas (não sei o que isso faz d verdade)
        //REgex é uma ferramente de identificação de caracteres em texto
        Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
        
        //Recorta as palavras das linhas e tira " " ao redor delas
        for(int i=1; i<lines.Length; ++i)
        {
            string line = lines[i];

            string[] fields = CSVParser.Split(line);

            for(int f=0; f<fields.Length; ++f)
            {
                fields[f] = fields[f].TrimStart(' ', surround); //acho q é aqui q altera se quiser deixar apenas , para separar
                fields[f] = fields[f].TrimEnd(surround);
            }

            //A primeira coluna é a chave
            //Alinhar chave com palavra
            if(fields.Length> attributeIndex)
            {
                var key = fields[0];

                if (dictionary.ContainsKey(key)) { continue; }

                var value = fields[attributeIndex];

                dictionary.Add(key, value);
            }
        }

        return dictionary;


    }
}
