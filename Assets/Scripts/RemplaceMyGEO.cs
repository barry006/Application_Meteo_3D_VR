using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemplaceMyGEO : MonoBehaviour
{
    
   [SerializeField] API_WebRequest api_WebRequest;
 
    void Start()
    {        
        // On start sur --- Nice --- vu que API Call g�ocalisation n'est pas acc�pt� sur unity play.
        api_WebRequest.Receive("43.70094", "7.268391");
    }

}

