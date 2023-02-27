using UnityEngine;
using System.Net;
using Newtonsoft.Json;
using System.Collections;


public class FuseauHoraire : MonoBehaviour
{
    string timeZoneId;


    private void Start()
    {
        StartCoroutine(GetTimeZoneFromGeoNamesAPI(10, 10));
    }

    public IEnumerator GetTimeZoneFromGeoNamesAPI(float latitude, float longitude)
    {
        WebClient webClient = new WebClient();
        string url = "http://api.geonames.org/timezoneJSON?lat=" + latitude + "&lng=" + longitude + "&username=bplc06";
        timeZoneId = webClient.DownloadString(url);
        
        yield return timeZoneId;
        Debug.Log("3............................................"+url);
        Desera(timeZoneId);
    }

    /*
    public void GetTimeZoneFromGeoNamesAPI(float latitude, float longitude)
    {

        WebClient webClient = new WebClient();
        string url = "http://api.geonames.org/timezoneJSON?lat=" + latitude + "&lng=" + longitude + "&username=bplc06";
        timeZoneId = webClient.DownloadString(url);
        ChangeInfo(timeZoneId);
    }
    */

    void Desera(string data)
    {
        Rootobject response = JsonConvert.DeserializeObject<Rootobject>(data);
        string tID = response.timezoneId;
        Debug.Log("nnnnnnnnnnnnn" + tID);
        Debug.Log("ddddddddddddd" + response.time);
    }

    public class Rootobject
    {
        public string sunrise { get; set; }
        public int lng { get; set; }
        public string countryCode { get; set; }
        public int gmtOffset { get; set; }
        public int rawOffset { get; set; }
        public string sunset { get; set; }
        public string timezoneId { get; set; }
        public int dstOffset { get; set; }
        public string countryName { get; set; }
        public string time { get; set; }
        public int lat { get; set; }
    }


}
