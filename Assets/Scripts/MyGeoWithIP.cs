using UnityEngine;
using System.Net;
using Newtonsoft.Json;

public class MyGeoWithIP : MonoBehaviour
{
    [SerializeField] API_WebRequest aPI_WebRequest;
    string _url;

    void Start()
    {
        /* L'appel API est effectué via la classe WebClient, qui utilise des threads d'E / S asynchrones
        pour effectuer les appels réseau. Par conséquent, l'appel n'est pas bloquant et ne nécessite
        pas d'être placé dans une coroutine.*/

        using (WebClient webClient = new WebClient()) //utiliser une clause "using" pour s'assurer que les ressources sont correctement libérées.
        {
            string publicIp = webClient.DownloadString("https://api.ipify.org");

            string publicGeo = webClient.DownloadString("http://www.geoplugin.net/json.gp?ip=" + publicIp);

            ChangeInfo(publicGeo);
        }
    }

    void ChangeInfo(string data)
    {
        geoPluginResponse response = JsonConvert.DeserializeObject<geoPluginResponse>(data);
        string _city = response.City;
        //Debug.Log(_city);

        _url = "https://api.openweathermap.org/";
        _url += $"/geo/1.0/direct?q={_city}";
        _url += $"&appid=9e87513ef7f34a0b9dbcf2c387617b30";
        //Debug.Log("_url_url_url" + _url);
        StartCoroutine(aPI_WebRequest.GetUnityWebRequestText(_url, "ApiGeo"));
        

    }
    class geoPluginResponse
    {
        [JsonProperty("geoplugin_city")] public string City { get; set; }
    }
}