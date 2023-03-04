using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System;
using Newtonsoft.Json;
using UnityEngine.UI;
using System.Text.RegularExpressions;


public class API_WebRequest : MonoBehaviour
{
    public TMP_InputField Ville_InputField;

    [SerializeField] RawImage img_Ico, img_Ville;
    [SerializeField] ClickOnEarth clickOnEarth;
    [SerializeField] RotationEarth rotationEarth;
    [SerializeField] InfoAssignation infoAssignation;
    [SerializeField] FuseauHoraire fuseauHoraire;
    [SerializeField] Texture2D t2d;

    string _urlComplete;
    string _MiniURL = "https://api.openweathermap.org/";
    string _APIKey = "9e87513ef7f34a0b9dbcf2c387617b30";
    Texture2D _myTexture;
    string wikipediaAPIEndpoint = "https://en.wikipedia.org/w/api.php?action=query&format=json&prop=pageimages&titles=";

    private void Start()
    {
        // ApiCallSearchCityImage("grenoble");
    }
    //------------WebRequest-------------------------------------------------------
    public IEnumerator GetUnityWebRequestText(string _url, string nameMethod)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(_url);

        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Error: " + uwr.error);
            yield break;
        }
        else
        {
            String resultRequest = uwr.downloadHandler.text;
            Deserialize(nameMethod, resultRequest);
        }
    }

    public IEnumerator GetUnityWebRequestTexture(string _url, string selectTexture)
    {
        UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(_url);
        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Error: " + uwr.error);
            yield break;
        }
        else
        {
            _myTexture = DownloadHandlerTexture.GetContent(uwr);

            AttibutTexture(selectTexture);
        }
    }
    
    public IEnumerator ApiCallSearchCityImage(string cityName)

    {
        string _url = wikipediaAPIEndpoint + cityName;
        Debug.Log("wiki _url :" + _url);

        UnityWebRequest uwr = UnityWebRequest.Get(_url);

        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Error: " + uwr.error);
            yield break;
        }

        else
        {
            String resultRequest = uwr.downloadHandler.text;
            if (resultRequest.Contains("source"))
            {
                string imageUrl = Deserialize2(resultRequest);
                imageUrl = imageUrl.Replace("50px", "500px");
                Debug.Log("imageUrl 500px 500px :" + imageUrl);
                if (imageUrl != null)
                {
                    StartCoroutine(GetUnityWebRequestTexture(imageUrl, "tVILLE"));
                }
            }
            else
            {
                img_Ville.texture = t2d;
                //StartCoroutine(ApiCallSearchCityImage("earth")); 
                Debug.Log("No Contains source !!!!!!!!!!!! ");
                yield break;
            }

        }        
    }
    //------------ END - WebRequest - END-----------------------------------------

    void AttibutTexture( string selectTexture)
    {
        if (selectTexture == "tICO")
        {
            img_Ico.texture = _myTexture;
        }
        else if (selectTexture == "tVILLE")
        {
            img_Ville.texture = _myTexture;
        }
    }

    //------------Deserialize ------------------------------------------
    void Deserialize(string nameMethod, string resultRequest)
    {
        if (!resultRequest.Contains("[]"))
        {
            if (nameMethod == "ApiGeo")
            {
                ClassGeo.Class1[] response = JsonConvert.DeserializeObject<ClassGeo.Class1[]>(resultRequest);
                Create_URL_Coordonate(response[0].lat.ToString(), response[0].lon.ToString());
                fuseauHoraire.receive(response[0].lat.ToString(), response[0].lon.ToString());
                StartCoroutine(GetUnityWebRequestText(_urlComplete, "ApiCoord"));


            }
            else if (nameMethod == "ApiCoord")
            {
                ClassOpenWeatherMap.Parent response = JsonConvert.DeserializeObject<ClassOpenWeatherMap.Parent>(resultRequest);

                infoAssignation.AssignChangeInfo(response);

                Create_URL_Ico(infoAssignation.Ico);

                //Debug.Log("ChangeInfo ChangeInfo ChangeInfo : " + nameMethod + " : "+_urlComplete);


                StartCoroutine(GetUnityWebRequestTexture(_urlComplete, "tICO"));
            }
        }
    }

    string Deserialize2(string jsonResponse)
    {
        WikipediaCLASS.RootObject deserializedObject = JsonConvert.DeserializeObject<WikipediaCLASS.RootObject>(jsonResponse);

        var enumerator = deserializedObject.query.pages.GetEnumerator();
        enumerator.MoveNext();
        var page = enumerator.Current.Value;

        // if (result.query.pages[0].revisions[0].slots.main["*"].Contains("source"))
        if (page.thumbnail != null)
        {
            Debug.Log("Source de la miniature : " + page.thumbnail.source);
            return page.thumbnail.source;
        }
        else
        {
            Debug.Log("Aucune miniature trouvée." + page.thumbnail.source);
            return null;
        }
    }    

    //------------ END - Deserialize - END----------------------------


    public bool ContainsRepeatedWord(string input, string word, int count)
    {
        // Utiliser une expression régulière pour trouver toutes les occurrences du mot dans la chaîne
        MatchCollection matches = Regex.Matches(input, "\\b" + word + "\\b", RegexOptions.IgnoreCase);

        // Si le nombre d'occurrences du mot dans la chaîne est supérieur à count, renvoyer vrai
        return matches.Count > count;
    }

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
        Debug.Log("Create_URL_Coordonate Create_URL_Coordonate Create_URL_Coordonate : " + _urlComplete);
    }
    public void Create_URL_Ico(string _ico)
    {
        _urlComplete = "http://openweathermap.org/";
        _urlComplete += $"img/wn/{_ico}@4x.png";
        //Debug.Log("Create_URL_Ico Create_URL_Ico Create_URL_Ico : "+_urlComplete);
    }
    public void Create_Url_Geo()
    {
        _urlComplete = _MiniURL;
        _urlComplete += $"/geo/1.0/direct?q={Ville_InputField.text}";
        _urlComplete += $"&appid={_APIKey}";
        //Debug.Log("Create_Url_Geo Create_Url_Geo Create_Url_Geo : " + _urlComplete);
    }
    //------------ END - Create_URL - END----------------------------------------



    public void Receive(string receive_lat, string receive_long)
    {
        Create_URL_Coordonate(receive_lat, receive_long);
        StartCoroutine(GetUnityWebRequestText(_urlComplete, "ApiCoord"));
        fuseauHoraire.receive(receive_lat, receive_long);

    }



}
