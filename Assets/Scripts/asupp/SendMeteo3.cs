using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Collections.Generic;
//using UnityEngine.UI;
//using System.Globalization;
//using System.Text.RegularExpressions;
using System.IO;
using System;
using Newtonsoft.Json;



public class SendMeteo3 : MonoBehaviour
{
    [SerializeField] TMP_InputField LatText;                            //Variable de la zone de saisi lattitude.
    [SerializeField] TMP_InputField LongText;                           //Variable de la zone de saisi longitude.
    [SerializeField] TextMeshProUGUI Resultat;                          //Variable text affichant le résultat.
    string _URL = "http://api.openweathermap.org/geo/1.0/direct";    //Url simplifié de l'api
    string _APIKey = "9e87513ef7f34a0b9dbcf2c387617b30";                //Clef API
    public List<string> data;
    string aaa;

    public void MyFunction() //Méthode appellé par le bouton Send.
    {

        if (LatText.text == string.Empty || LongText.text == string.Empty) // Si les zone de saisi ne sont pas remplis, afficher : ERROR : Write value in the input fields.
        {
            Resultat.text = "ERROR : Write value in the input fields";
            return;                                                        // empeche de jouer la coroutine si les zone de saisi sont vide.
        }

        StartCoroutine(GetText()); // start Coroutine.
    }

    public IEnumerator GetText()
    {
        // GENERATE URL FULL ----
        string stringUrl = _URL;
        //stringUrl += $"?q={_cityName.text}";  
       // stringUrl += $",{_countryCode}"; 
        //stringUrl += $"&limit={_limit}";         
        stringUrl += $"&appid={_APIKey}";     // API KEY
        //-----------------------
        //http://api.openweathermap.org/geo/1.0/direct?q={city name},{state code},{country code}&limit={limit}&appid={API key}
        Debug.Log(stringUrl);

        UnityWebRequest uwr = UnityWebRequest.Get(stringUrl); // Request URL.
        yield return uwr.SendWebRequest();                    // Return Request.


        String bbb = uwr.downloadHandler.text;
        Resultat.text = bbb;

        Parent response = JsonConvert.DeserializeObject<Parent>(bbb);

        float name = response.Main.Temp;
        float name2 = response.Wind.Gust;
        Debug.Log(name2);

   
    }

    [System.Serializable]
    public class Parent
    {
        [JsonProperty("coord")] private Coord _coord;
        [JsonProperty("weather")] private List<Weather> _weatherList;
        [JsonProperty("base")] private string _base;
        [JsonProperty("main")] private Main _main;
        [JsonProperty("visibility")] private int _visibility;
        [JsonProperty("wind")] private Wind _wind;
        [JsonProperty("clouds")] private Clouds _clouds;
        [JsonProperty("dt")] private int _dt;
        [JsonProperty("sys")] private Sys _sys;
        [JsonProperty("timezone")] private int _timezone;
        [JsonProperty("id")] private int _id;
        [JsonProperty("name")] private string _name;
        [JsonProperty("cod")] private int _cod;

        public Coord Coord
        {
            get { return _coord; }
        }

        public List<Weather> WeatherList
        {
            get { return _weatherList; }
        }

        public string Base
        {
            get { return _base; }
        }

        public Main Main
        {
            get { return _main; }
        }

        public int Visibility
        {
            get { return _visibility; }
        }

        public Wind Wind
        {
            get { return _wind; }
        }

        public Clouds Clouds
        {
            get { return _clouds; }
        }

        public int Dt
        {
            get { return _dt; }
        }

        public Sys Sys
        {
            get { return _sys; }
        }

        public int Timezone
        {
            get { return _timezone; }
        }

        public int Id
        {
            get { return _id; }
        }

        public string Name
        {
            get { return _name; }
        }

        public int Cod
        {
            get { return _cod; }
        }
    }

    [System.Serializable]
    public class Coord
    {
        [JsonProperty("lon")] private float _lon;
        [JsonProperty("lat")] private float _lat;

        public float Lon
        {
            get { return _lon; }
        }

        public float Lat
        {
            get { return _lat; }
        }
    }

    [System.Serializable]
    public class Weather
    {
        [JsonProperty("id")] private int _id;
        [JsonProperty("main")] private string _main;
        [JsonProperty("description")] private string _description;
        [JsonProperty("icon")] private string _icon;

        public int Id
        {
            get { return _id; }
        }

        public string Main
        {
            get { return _main; }
        }

        public string Description
        {
            get { return _description; }
        }

        public string Icon
        {
            get { return _icon; }
        }
    }

    [System.Serializable]
    public class Main
    {
        [JsonProperty("temp")] private float _temp;
        [JsonProperty("feels_like")] private float _feelsLike;
        [JsonProperty("temp_min")] private float _tempMin;
        [JsonProperty("temp_max")] private float _tempMax;
        [JsonProperty("pressure")] private int _pressure;
        [JsonProperty("humidity")] private int _humidity;
        [JsonProperty("sea_level")] private int _seaLevel;
        [JsonProperty("grnd_level")] private int _grndLevel;

        public float Temp
        {
            get { return _temp; }
        }

        public float FeelsLike
        {
            get { return _feelsLike; }
        }

        public float TempMin
        {
            get { return _tempMin; }
        }

        public float TempMax
        {
            get { return _tempMax; }
        }

        public int Pressure
        {
            get { return _pressure; }
        }

        public int Humidity
        {
            get { return _humidity; }
        }

        public int SeaLevel
        {
            get { return _seaLevel; }
        }

        public int GrndLevel
        {
            get { return _grndLevel; }
        }
    }

    [System.Serializable]
    public class Wind
    {
        [JsonProperty("speed")] private float _speed;
        [JsonProperty("deg")] private int _deg;
        [JsonProperty("gust")] private float _gust;

        public float Speed
        {
            get { return _speed; }
        }

        public int Deg
        {
            get { return _deg; }
        }

        public float Gust
        {
            get { return _gust; }
        }
    }

    [System.Serializable]
    public class Clouds
    {
        [JsonProperty("all")] private int _all;

        public int All
        {
            get { return _all; }
        }
    }

    [System.Serializable]
    public class Sys
    {
        [JsonProperty("type")] private int _type;
        [JsonProperty("id")] private int _id;
        [JsonProperty("country")] private string _country;
        [JsonProperty("sunrise")] private int _sunrise;
        [JsonProperty("sunset")] private int _sunset;

        public int Type
        {
            get { return _type; }
        }

        public int Id
        {
            get { return _id; }
        }

        public string Country
        {
            get { return _country; }
        }

        public int Sunrise
        {
            get { return _sunrise; }
        }

        public int Sunset
        {
            get { return _sunset; }
        }
    }

    //----------------------------------------------------------------------------------------------------------
    //Note perso------------------------------------------------------------------------------------------------
    //%E2%80%8B
    //[SerializeField] float Lat = 44.34f;
    //[SerializeField] float Long = 10.99f;
    // stringUrl += $"?lat={LatText.text.ToString(CultureInfo.InvariantCulture)}";
    // stringUrl += $"&lon={LongText.text.ToString(CultureInfo.InvariantCulture)}";
    //https://api.openweathermap.org/data/2.5/weather?lat=44.34&lon=10.99&appid=9e87513ef7f34a0b9dbcf2c387617b30 
}