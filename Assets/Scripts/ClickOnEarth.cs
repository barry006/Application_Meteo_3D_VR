using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ClickOnEarth : MonoBehaviour
{

    [HideInInspector] public Vector2 LongLat;
    [SerializeField] TextMeshProUGUI latText;
    [SerializeField] TextMeshProUGUI longText;
    Vector3 _offset;
    void Update()
    {

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
        {
            _offset = hit.collider.transform.InverseTransformPoint(hit.point);
            LongLat = ToSpherical(_offset);
            latText.text = LongLat.x.ToString();
            longText.text = LongLat.y.ToString();
        }
    }

    public Vector2 ToSpherical(Vector3 rotation)
    {
        rotation.Normalize();                                               // Convert to a unit vector so our y coordinate is in the range -1...1.       
        float lat = Mathf.Asin(rotation.y) * Mathf.Rad2Deg;                 // The vertical coordinate (y) varies as the sine of latitude, not the cosine.        
        float lon = Mathf.Atan2(rotation.x, rotation.z) * Mathf.Rad2Deg;    // Use the 2-argument arctangent, which will correctly handle all four quadrants.
        return new Vector2(lat, -lon);
    }
}