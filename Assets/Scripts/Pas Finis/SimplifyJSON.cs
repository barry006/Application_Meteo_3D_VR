using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SimplifyJSON : MonoBehaviour
{
    [SerializeField] private string filePath = "Assets/Json/city.list.json";
    [SerializeField] private string outputPath = "Assets/Json/output.json";

    [System.Serializable]
    private class CityData //classe qui représente les données à extraire du fichier JSON.
    {
        public string name;
        public float lon;
        public float lat;
    }
    [System.Serializable]
    public class CityInfo
    {
        public Class1[] Property1 { get; set; }
    }

    [System.Serializable]
    public class Class1
    {
        public float id { get; set; }
        public string name { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public Coord coord { get; set; }
    }

    [System.Serializable]
    public class Coord
    {
        public float lon { get; set; }
        public float lat { get; set; }
    }
    private void Start()
    {
        string jsonText = File.ReadAllText(filePath); //Charge le fichier JSON

        List<CityData> cities = new List<CityData>();

        CityInfo[] cityInfos = JsonUtility.FromJson<CityInfo[]>(jsonText); //Désérialiser le JSON 
       
        
        
        foreach (CityInfo cityInfo in cityInfos) 
        {
            CityData cityData = new CityData();
            cityData.name = cityInfo.Property1[0].name;
            cityData.lon = cityInfo.Property1[0].coord.lon;
            cityData.lat = cityInfo.Property1[0].coord.lat;
            cities.Add(cityData);
        }
        
        string outputJSON = JsonUtility.ToJson(cities.ToArray());

        File.WriteAllText(outputPath, outputJSON);
    }
}