using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System;
using Newtonsoft.Json;
using System.Net;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
//using System.Globalization;
//using System.Text.RegularExpressions;

public class API_WebRequest : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ville, temperature, descrition, tempMini, tempMax;
    [SerializeField] TMP_InputField lat_InputField, long_InputField, ville_InputField;
    [SerializeField] RotationEarth rotationEarth;
    [SerializeField] ClickOnEarth clickOnEarth;
    [SerializeField] RawImage img_Ico;

    string _UrlComplete, _ico;
    string _MiniURL = "https://api.openweathermap.org/";
    string _APIKey = "9e87513ef7f34a0b9dbcf2c387617b30";

    private void Awake()
    {
        lat_InputField.text = "0";
        long_InputField.text = "0";
        /*
        WebClient webClient = new WebClient();
        string publicIp = webClient.DownloadString("https://api.ipify.org");
        Debug.Log(publicIp);
        */
    }

    private void Update()
    {
        if (rotationEarth.ClickingOnEarth && rotationEarth.BoolSaveRotEarth)
        {
            lat_InputField.text = clickOnEarth.LongLat.x.ToString();
            long_InputField.text = clickOnEarth.LongLat.y.ToString();
        }
    }

    //------------WebRequest-------------------------------------------------------
    public IEnumerator GetUnityWebRequestText(string _url, string nameMethod)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(_url);      // Request URL.
        yield return uwr.SendWebRequest();                    // Return Request.
        String resultRequest = uwr.downloadHandler.text;
        ChangeInfo(nameMethod, resultRequest);
    }

    public IEnumerator GetUnityWebRequestTexture(string _url)
    {
        UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(_url);
        yield return uwr.SendWebRequest();
        Texture2D myTexture = DownloadHandlerTexture.GetContent(uwr);
        img_Ico.texture = myTexture;
    }
    //------------ END - WebRequest - END-----------------------------------------



    //------------Deserialize AND Assign------------------------------------------
    void ChangeInfo(string nameMethod, string resultRequest)
    {
        if (nameMethod == "ApiGeo")
        {
            ClassGeo.Class1[] response = JsonConvert.DeserializeObject<ClassGeo.Class1[]>(resultRequest);
            Create_URL_Coordonate(response[0].lat.ToString(), response[0].lon.ToString());
            StartCoroutine(GetUnityWebRequestText(_UrlComplete, "ApiCoord"));
        }
        else if (nameMethod == "ApiCoord")
        {
            ClassOpenWeatherMap.Parent response = JsonConvert.DeserializeObject<ClassOpenWeatherMap.Parent>(resultRequest);
            AssignChangeInfo(response);

            Create_URL_Ico(_ico);
            Debug.Log(_UrlComplete);
            StartCoroutine(GetUnityWebRequestTexture(_UrlComplete));
        }
    }
    public void AssignChangeInfo(ClassOpenWeatherMap.Parent _class)
    {
        ville.text = _class.name;
        temperature.text = "Temperature : " + _class.main.temp.ToString();
        descrition.text = "Descrition : " + _class.weather[0].description;
        tempMini.text = "TempMini : " + _class.main.temp_min.ToString();
        tempMax.text = "TempMax : " + _class.main.temp_max.ToString();
        _ico = _class.weather[0].icon;
    }
    //------------ END - Deserialize AND Assign - END----------------------------



    //------------GET API WITH BUTTON--------------------------------------------
    public void GetApiCoordonate() 
    {
        Create_URL_Coordonate(lat_InputField.text, long_InputField.text);
        StartCoroutine(GetUnityWebRequestText(_UrlComplete, "ApiCoord"));
    }
    public void GetApiGeo() 
    {
        Create_Url_Geo();
        StartCoroutine(GetUnityWebRequestText(_UrlComplete, "ApiGeo"));
    }
    //------------ END - GET API WITH BUTTON - END-------------------------------



    //------------Create_URL-----------------------------------------------------
    public void Create_URL_Coordonate(string _lat, string _long)
    {
        _UrlComplete = _MiniURL;
        _UrlComplete += $"data/2.5/weather?lat={_lat}";
        _UrlComplete += $"&lon={_long}";
        _UrlComplete += "&units=metric";
        _UrlComplete += $"&appid={_APIKey}";
        Debug.Log(_UrlComplete);
    }
    public void Create_URL_Ico(string _ico)
    {
        _UrlComplete = "http://openweathermap.org/";
        _UrlComplete += $"img/wn/{_ico}@4x.png";
        Debug.Log(_UrlComplete);
    }
    public void Create_Url_Geo()
    {
        _UrlComplete = _MiniURL;
        _UrlComplete += $"/geo/1.0/direct?q={ville_InputField.text}";
        _UrlComplete += $"&appid={_APIKey}";
        Debug.Log(_UrlComplete);
    }
    //------------ END - Create_URL - END----------------------------------------





    /*   if (uwr.isNetworkError || uwr.isHttpError)
    {
    debug.log
    }
    else
    { ..}

        */

    //x = float.Parse(LatText.text);
    // y = float.Parse(LongText.text);
    // planete.transform.localRotation = Quaternion.Euler(-x, y, 0f); 
    /*

         if (!resultRequest.Contains("ERROR"))
        {
            Resultat.text = resultRequest;
        else
        {
            Resultat.text = "ERROR BAD COORDONATE RETRY";
        }
    */



    /*
    float GetObjectRotation()
    {
        if (planete.transform.localEulerAngles.x > 180)//- 90)
        {
            return planete.transform.localEulerAngles.x - 360;//- 180;
        }
        else
        {
            return planete.transform.localEulerAngles.x;
        }
    }

    float GetObjectRotation2()
    {
        if (planete.transform.localEulerAngles.y > 180)
        {
            return planete.transform.localEulerAngles.y - 360;
        }
        else
        {
            return planete.transform.localEulerAngles.y;
        }
    }

    */

    //----------------------------------------------------------------------------------------------------------
    //Note perso------------------------------------------------------------------------------------------------
    //%E2%80%8B
    //[SerializeField] float Lat = 44.34f;
    //[SerializeField] float Long = 10.99f;
    // stringUrl += $"?lat={LatText.text.ToString(CultureInfo.InvariantCulture)}";
    // stringUrl += $"&lon={LongText.text.ToString(CultureInfo.InvariantCulture)}";
    //https://api.openweathermap.org/data/2.5/weather?lat=44.34&lon=10.99&appid=9e87513ef7f34a0b9dbcf2c387617b30 
}