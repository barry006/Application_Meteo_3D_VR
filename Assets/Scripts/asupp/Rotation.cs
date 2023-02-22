using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private  float xTour = 0; 
    [SerializeField] private float yTour = -20; 
    [SerializeField] private float zTour = 0; 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // rotation de la planete sur un seul axe y 
        transform.Rotate(new Vector3(xTour, yTour, zTour) * Time.deltaTime);
    }
}
