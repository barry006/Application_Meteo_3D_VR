using UnityEngine;

public class MyInput : MonoBehaviour
{
    [SerializeField] API_WebRequest aPI_WebRequest;
    [SerializeField] Autocomplete autocomplete;
    bool b;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && aPI_WebRequest.Ville_InputField.text != string.Empty)
        {           
            StartCoroutine(aPI_WebRequest.GetApiGeo());
            autocomplete.ClearResults();
            autocomplete.panel.SetActive(false);           
        }
    }
    
}
