using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OnClickAutoCompletion : MonoBehaviour
{  
    [SerializeField] Text txt;

    GameObject _goAutocomplete;
    GameObject _goApi_WebRequest;
    Autocomplete _autocomplete;
    API_WebRequest _api_WebRequest;  

    // Prochainement enlever getcomponent !!!!!!!!!!!!!!!!!
    public void ClikAutoCompleted()
    {       
        //------------------------------------------------------------
        _goAutocomplete = GameObject.FindGameObjectWithTag("Recherche");
        _autocomplete = _goAutocomplete.GetComponent<Autocomplete>();
        _goApi_WebRequest = GameObject.FindGameObjectWithTag("Manager");
        _api_WebRequest = _goApi_WebRequest.GetComponent<API_WebRequest>();
        //------------------------------------------------------------


        _autocomplete.InputField.text = txt.text;
        _autocomplete.ClearResults();
      
        StartCoroutine(_api_WebRequest.GetApiGeo());
        _autocomplete.Panel.SetActive(false);
    }

    public void RecupReff(Autocomplete autocomplete, API_WebRequest api_WebRequest)
    {
        _autocomplete = autocomplete;
        _api_WebRequest = api_WebRequest;
    }


}
