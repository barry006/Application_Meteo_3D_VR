using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.Globalization;
using System.Text.RegularExpressions;
using System.IO;
using System;

public class SendMeteo : MonoBehaviour
{


    public TextMeshProUGUI LatText;
    public TextMeshProUGUI LongText;
    public TextMeshProUGUI Résultat;
    public float Lat = 44.34f;
    public float Long = 10.99f;




    string URL = "https://api.openweathermap.org/data/2.5/weather";
    string APIKey = "9e87513ef7f34a0b9dbcf2c387617b30";

    //https://api.openweathermap.org/data/2.5/weather?lat=44.34&lon=10.99&appid=9e87513ef7f34a0b9dbcf2c387617b30 
    //https://api.openweathermap.org/data/2.5/weather?lat=44.34​&lon=10.99​&appid=9e87513ef7f34a0b9dbcf2c387617b30

    //https://api.openweathermap.org/data/2.5/weather?lat=44.34&lon=10.99&lang=fr&appid=9e87513ef7f34a0b9dbcf2c387617b30
    //https://api.openweathermap.org/data/2.5/weather?lat=44.34&lon=10.99&units=metric&appid=9e87513ef7f34a0b9dbcf2c387617b30





    // public InputField input;
    // public InputField iField;

    public void MyFunction2()
    {
        //var eee = LatText.text;
        //  var eepe  = System.Globalization.CultureInfo.InvariantCulture;
        //float f = float.Parse(LatText.text, System.Globalization.CultureInfo.InvariantCulture);
        //var LongText= CultureInfo.CurrentCulture.EnglishName; 
        //string stringUrl = URL+"lat="+Lat +"&lon="+Long+"&appid="+APIKey;
        //string stringUrl = URL+"lat="+LatText.text+"&lon="+LongText.text+"&appid="+APIKey;

        //string stringUrl = URL +"lat="+LatText.text+"&lon="+LongText.text+"&appid=" + APIKey;
        //string stringUrl = URL + "lat=" + LatText.text + "&lon=" + LongText.text + "&units=metric&appid=" + APIKey;
        
        //Lat = float.Parse(LatText.text, System.Globalization.CultureInfo.InvariantCulture);
        //Long = float.Parse(LatText.text, System.Globalization.CultureInfo.InvariantCulture);
        
      // LatText.text.ToString(System.Globalization.CultureInfo.InvariantCulture);
       
        //Long = LatText.text, System.Globalization.CultureInfo.InvariantCulture;



       // Debug.Log("zz" + v9.x.ToString(CultureInfo.InvariantCulture));
        //Debug.Log("zz "+goo.transform.position.x.ToString(CultureInfo.InvariantCulture));

        //Lat = float.Parse(LatText.text);

        //Debug.Log(Lat + " -- " + Long);
         
        StartCoroutine(GetText());


        // var i = LatText.text;
        // var i2 = LongText.text;
        // var Lat = int.Parse(i);
        // var Long = int.Parse(i2);
        //float num = float.Parse(LatText.text);

        // string abc = URL + "lat=" + LatText.text + "&lon=" + LongText.text + "&units=metric&appid=" + APIKey;
        // Debug.Log(abc);


        //  int number; 
        // int.TryParse(input.text, out int result); number = result;

        /*
        float f;
        float f2;
        if (float.TryParse(LatText.text, out f ) && (float.TryParse(LongText.text, out f2)))
        {
            string abc = URL + "lat=" + Lat + "&lon=" + Long + "&appid=" + APIKey;
            Debug.Log(abc.ToString());
        }
        else
        {
            Debug.Log("nullllll");
        }
        */
        /*
        string str = "12";
        int val = int.Parse(str);
        Debug.Log(val);
        */

    }

    IEnumerator GetText()
    {
        string stringUrl = URL;
        stringUrl += $"?lat={LatText.text.ToString(CultureInfo.InvariantCulture)}";
        stringUrl += $"&lon={LongText.text.ToString(CultureInfo.InvariantCulture)}";
        stringUrl += $"&appid={APIKey}";
        Debug.Log(stringUrl);
        UnityWebRequest uwr = UnityWebRequest.Get(stringUrl);
        yield return uwr.SendWebRequest();
        Résultat.text = uwr.downloadHandler.text;
    }
}