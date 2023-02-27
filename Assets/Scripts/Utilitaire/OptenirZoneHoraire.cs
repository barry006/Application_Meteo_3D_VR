using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// a finir
public class OptenirZoneHoraire : MonoBehaviour
{
    public List<string> timeZones = new List<string>();


    void Start()
    {
        // GetTimeZoneNamesByCity("paris");
        //Debug.Log(DateTime.Now.ToString(new CultureInfo("en-US")) + DateTime.Now.Kind);
    }




    public string[] GetTimeZoneNamesByCity(string cityName)
    {
        foreach (TimeZoneInfo timeZone in TimeZoneInfo.GetSystemTimeZones())
        {


            timeZones.Add(timeZone.Id);

        }
        return timeZones.ToArray();
    }
}
