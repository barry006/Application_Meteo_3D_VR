using UnityEngine;
using TMPro;
using System;

public class InfoAssignation : MonoBehaviour
{
    public TextMeshProUGUI Zone, HeureDate; // FuseauHoraire void Assign().
    [HideInInspector] public string Ico;
    [SerializeField] TextMeshProUGUI ville, QuelleTempIlFait, temperature, tempMini, tempMax, date_heures, ventMS, ventDirection, pression, humidite;
    [SerializeField] API_WebRequest api_WebRequest;

    [SerializeField] bool _activedChangeImages = false;
    void Update()
    {
        date_heures.text = DateTime.Now.ToString("dddd dd MMMM yyyy HH'H'mm:ss");
    }


    public void AssignChangeInfo(ClassOpenWeatherMap.Parent _class)
    {
        Ico = _class.weather[0].icon;
        //-----------
        //-----------
        ville.text = _class.name;
        ville.text = (ville.text != "") ? _class.name : "No man's land";
        //-----------
        QuelleTempIlFait.text = _class.weather[0].description;
        //-----------
        temperature.text = _class.main.temp.ToString() + "°";
        tempMini.text = "TMini : " + _class.main.temp_min.ToString() + "°";
        tempMax.text = "TMax : " + _class.main.temp_max.ToString() + "°";
        //-----------
        ventMS.text = _class.wind.speed.ToString() + " mètre/sec";
        ventDirection.text = _class.wind.deg.ToString() + "°";

        pression.text = _class.main.pressure + " hPa";
        humidite.text = _class.main.humidity + " %";


        if (_activedChangeImages)
        {
            assignImageWikipedia(_class.name);
        }
    }
    void assignImageWikipedia(string ville)
    {
        if (ville != string.Empty)
        {
            StartCoroutine(api_WebRequest.ApiCallSearchCityImage(ville));
            //api_WebRequest.ApiCallSearchCityImage(ville);
        }
        else
        {
            StartCoroutine(api_WebRequest.ApiCallSearchCityImage("No_man%27s_land"));
        }
    }
}
