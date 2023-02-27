using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System;
using Newtonsoft.Json;
using UnityEngine.UI;


public class API_WebRequest : MonoBehaviour
{
    public TMP_InputField Ville_InputField;

    [SerializeField] RawImage img_Ico;
    [SerializeField] ClickOnEarth clickOnEarth;
    [SerializeField] RotationEarth rotationEarth;
    [SerializeField] InfoAssignation infoAssignation;

    string _urlComplete;
    string _MiniURL = "https://api.openweathermap.org/";
    string _APIKey = "9e87513ef7f34a0b9dbcf2c387617b30";


    private void Update()
    {
        if (rotationEarth.ClickingOnEarth && rotationEarth.BoolSaveRotEarth)
        {

        }
    }

    //------------WebRequest-------------------------------------------------------
    public IEnumerator GetUnityWebRequestText(string _url, string nameMethod)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(_url);

        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Error: " + uwr.error);
        }
        else
        {
            String resultRequest = uwr.downloadHandler.text;
            ChangeInfo(nameMethod, resultRequest);
        }

    }

    public IEnumerator GetUnityWebRequestTexture(string _url)
    {
        UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(_url);
        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Error: " + uwr.error);
        }
        else
        {
            Texture2D myTexture = DownloadHandlerTexture.GetContent(uwr);
            img_Ico.texture = myTexture;
        }
    }
    //------------ END - WebRequest - END-----------------------------------------



    //------------Deserialize ------------------------------------------
    void ChangeInfo(string nameMethod, string resultRequest)
    {
        if (!resultRequest.Contains("[]"))
        {
            if (nameMethod == "ApiGeo")
            {
                ClassGeo.Class1[] response = JsonConvert.DeserializeObject<ClassGeo.Class1[]>(resultRequest);
                Create_URL_Coordonate(response[0].lat.ToString(), response[0].lon.ToString());
                StartCoroutine(GetUnityWebRequestText(_urlComplete, "ApiCoord"));


            }
            else if (nameMethod == "ApiCoord")
            {
                ClassOpenWeatherMap.Parent response = JsonConvert.DeserializeObject<ClassOpenWeatherMap.Parent>(resultRequest);

                infoAssignation.AssignChangeInfo(response);

                Create_URL_Ico(infoAssignation.Ico);

                //Debug.Log(_urlComplete);
                StartCoroutine(GetUnityWebRequestTexture(_urlComplete));
            }
        }
    }

    //------------ END - Deserialize - END----------------------------



    //------------GET API --------------------------------------------
    public IEnumerator GetApiCoordonate()
    {
        Create_URL_Coordonate(clickOnEarth.LongLat.x.ToString(), clickOnEarth.LongLat.y.ToString());
        yield return StartCoroutine(GetUnityWebRequestText(_urlComplete, "ApiCoord"));
    }
    public IEnumerator GetApiGeo()
    {
        Create_Url_Geo();
        yield return StartCoroutine(GetUnityWebRequestText(_urlComplete, "ApiGeo"));
    }
    //------------ END - GET API -------------------------------



    //------------Create_URL-----------------------------------------------------
    public void Create_URL_Coordonate(string _lat, string _long)
    {
        _urlComplete = _MiniURL;
        _urlComplete += $"data/2.5/weather?lat={_lat}";
        _urlComplete += $"&lon={_long}";
        _urlComplete += "&units=metric";
        _urlComplete += "&lang=fr";
        _urlComplete += $"&appid={_APIKey}";
        //Debug.Log(_urlComplete);
    }
    public void Create_URL_Ico(string _ico)
    {
        _urlComplete = "http://openweathermap.org/";
        _urlComplete += $"img/wn/{_ico}@4x.png";
        //Debug.Log(_urlComplete);
    }
    public void Create_Url_Geo()
    {
        _urlComplete = _MiniURL;
        _urlComplete += $"/geo/1.0/direct?q={Ville_InputField.text}";
        _urlComplete += $"&appid={_APIKey}";
        //Debug.Log(_urlComplete);
    }
    //------------ END - Create_URL - END----------------------------------------




    public void Receive(string receive_lat, string receive_long)
    {
        Create_URL_Coordonate(receive_lat, receive_long);
        StartCoroutine(GetUnityWebRequestText(_urlComplete, "ApiCoord"));
    }
}
