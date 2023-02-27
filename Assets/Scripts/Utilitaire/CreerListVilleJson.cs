using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class CreerListVilleJson : MonoBehaviour
{

    string filePath_Light =  @"Assets/Json/JustName.json";          
    string filePath = @"Assets/Json/city.list.json";             // entrer
   // string filePath_Light = @"Assets/Json/citylistLight.json";   //sortie
                                                                
    public List<MyObject> myObjects = new List<MyObject>();

    private void Start()
    {
        if (File.Exists(filePath_Light))
        {
            LectureEtDeserialisation(filePath_Light);
        }
        else
        {
            LectureEtDeserialisation(filePath);
            CreerNewJson();
            Debug.Log("----------------Création filePath_Light----------------");
        }
    }
    void LectureEtDeserialisation(string filePathMethode)
    {
        string classData = File.ReadAllText(filePathMethode);
        myObjects = JsonConvert.DeserializeObject<List<MyObject>>(classData);


    }
    void CreerNewJson()
    {
        OrderByName();
        //myObjects.AddRange(myObjects);
        string updatedJsonData = JsonConvert.SerializeObject(myObjects, Formatting.Indented);
        File.WriteAllText(filePath_Light, updatedJsonData);
    }
    void OrderByName()
    {
        myObjects.Sort(delegate (MyObject a, MyObject b)
        {
            return a.name.CompareTo(b.name);
        });
    }







    //Ma Class @"Assets/Json/city.list.json------------------------
    //comment for restruct
    [System.Serializable]
    public class MyObject
    {
        //public float id;
        public string name;
        /*
        public Coord coord;
        public string country;

        [System.Serializable]
        public class Coord
        {
            public float lon;
            public float lat;
        }*/
    }
    //------------------------------------------------------------
}



