using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
public static class Recup_Langue_Culture 
{
    public static string R_Langue_Culture(string cc)
    {
        string countryCode = cc;
        RegionInfo region = new RegionInfo(countryCode);
        string cultureCode = region.TwoLetterISORegionName.ToLower() + "-" + region.TwoLetterISORegionName.ToUpper();   
        return cultureCode;
    } 
}
