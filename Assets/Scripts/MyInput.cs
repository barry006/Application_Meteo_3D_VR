using UnityEngine;

public class MyInput : MonoBehaviour
{
    [SerializeField] API_WebRequest api_WebRequest;
    [SerializeField] Autocomplete autocomplete;
    bool b;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && api_WebRequest.Ville_InputField.text != string.Empty)
        {           
            StartCoroutine(api_WebRequest.GetApiGeo());
            autocomplete.ClearResults();
            autocomplete.Panel.SetActive(false);           
        }
    }
    
}
