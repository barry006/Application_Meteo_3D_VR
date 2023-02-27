using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ClickOnEarth : MonoBehaviour
{

    [HideInInspector] public Vector2 LongLat;
    public RaycastHit RecupHit;
    public GameObject Indicator;

    [SerializeField] TextMeshProUGUI latText;
    [SerializeField] TextMeshProUGUI longText;
    
    Vector3 _offset;
    string direction;

    private void Awake()
    {
        latText.text = "0";
        longText.text = "0";
    }
    void Update()
    {

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
        {
            _offset = hit.collider.transform.InverseTransformPoint(hit.point);
            LongLat = ToSpherical(_offset);


            // latText.text = LongLat.x.ToString();
            // longText.text = LongLat.y.ToString();    

            latText.text =  ConvertLatLongToGoodData(LongLat.x, true);
            longText.text = ConvertLatLongToGoodData(LongLat.y, false);      

            RecupHit = hit;
        }
    }


    public string ConvertLatLongToGoodData(float value, bool b)
    {
        direction = b ? direction = value >= 0 ? "N" : "S" : value >= 0 ? "E" : "W";  //Si la valeur est positive, direction est "", sinon c'est ""

        float absValue = Mathf.Abs(value);                                                          // On prend la valeur absolue de la variable value
        int degrees = (int)absValue;                                                                // On récupère la partie entière de la valeur absolue
        float minutes = (absValue - degrees) * 60f;                                                 // On calcule la partie décimale en minutes
        string formattedValue = string.Format("{0}°{1}{2:0.000}", degrees, direction, minutes);     // On formate le résultat avec les spécificateurs de format

        return formattedValue;
    }



    // Calcule la position sur la sphere------------------------------------------------
    public Vector2 ToSpherical(Vector3 rotation)
    {
        rotation.Normalize();                                               // Convert to a unit vector so our y coordinate is in the range -1...1.       
        float lat = Mathf.Asin(rotation.y) * Mathf.Rad2Deg;                 // The vertical coordinate (y) varies as the sine of latitude, not the cosine.        
        float lon = Mathf.Atan2(rotation.x, rotation.z) * Mathf.Rad2Deg;    // Use the 2-argument arctangent, which will correctly handle all four quadrants.
        return new Vector2(lat, -lon);
    }
    // END - Calcule la position sur la sphere - END ------------------------------------



}