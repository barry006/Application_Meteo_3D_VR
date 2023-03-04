using UnityEngine;
using System.Net;
using Newtonsoft.Json;
using System.Collections;
using UnityEngine.Networking;
using System.Globalization;


public class FuseauHoraire : MonoBehaviour
{
    string timeZoneId;
    [SerializeField] InfoAssignation InfoAssignation;

    private void Start()
    {
        //Temporaire---------------------------------------------------
        //NICE------
        StartCoroutine(GetTimeZoneFromGeoNamesAPI("43.70094", "7.268391"));
        //-------------------------------------------------------------
    }

    public void receive(string latitude, string longitude)
    {
        //InvariableCulture ne fonctionne pas a retester :/
        latitude = latitude.Replace(",", ".");
        longitude = longitude.Replace(",", ".");
        StartCoroutine(GetTimeZoneFromGeoNamesAPI(latitude, longitude));
    }
    public IEnumerator GetTimeZoneFromGeoNamesAPI(string latitude, string longitude)
    {
        string _url = "http://api.geonames.org/timezoneJSON?lat=" + latitude + "&lng=" + longitude + "&username=bplc06";
        UnityWebRequest uwr = UnityWebRequest.Get(_url);

        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Error: " + uwr.error);
            yield break;
        }
        else
        {
            string resultRequest = uwr.downloadHandler.text;
            Desera(resultRequest);
        }

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
        // Debug.Log("tID tID tID : " + tID);
        //Debug.Log("response.time response.time response.time : " + response.time);
        //Debug.Log("!!!!!!!!!!!!!!!!!!!! "+response.timezoneId);
        Assign(response);
    }
    public void Assign(Rootobject response)
    {
        InfoAssignation.HeureDate.text = response.time;
        InfoAssignation.Zone.text = (InfoAssignation.HeureDate.text != null) ? response.timezoneId : "No man's land";
    }

    public class Rootobject
    {
        // public string sunrise { get; set; }
        //public int lng { get; set; }
        // public string countryCode { get; set; }
        //public int gmtOffset { get; set; }
        //public float rawOffset { get; set; }
        // public string sunset { get; set; }
        public string timezoneId { get; set; }
        //public float dstOffset { get; set; }
        // public string countryName { get; set; }
        public string time { get; set; }
        // public int lat { get; set; }
    }


}
