using UnityEngine;
using TMPro;
using System;

using System.Linq;
using System.Collections.Generic;

public class InfoAssignation : MonoBehaviour
{
    [HideInInspector] public string Ico;
    [SerializeField] TextMeshProUGUI ville, temperature, QuelleTempIlFait, tempMini, tempMax, date_heures; 
   
    void Update()
    {
        date_heures.text = DateTime.Now.ToString("dddd dd MMMM yyyy HH'H'mm:ss");
    }


    public void AssignChangeInfo(ClassOpenWeatherMap.Parent _class)
    {
        ville.text = _class.name;
        ville.text = (ville.text != "") ? _class.name : "No man's land";

        temperature.text = _class.main.temp.ToString() + "°";
        QuelleTempIlFait.text = _class.weather[0].description;
        tempMini.text = "TMini : " + _class.main.temp_min.ToString() + "°";
        tempMax.text = "TMax : " + _class.main.temp_max.ToString() + "°";
        Ico = _class.weather[0].icon;
    }
}
