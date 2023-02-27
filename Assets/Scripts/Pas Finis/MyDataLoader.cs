using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using Newtonsoft.Json;

public class MyDataLoader : MonoBehaviour
{
    [SerializeField] GameObject loadingIndicator;
    string filePath = @"Assets/Json/JustName.json"; // chemin d'accès au fichier JSON
    private List<MyObject> myList = new List<MyObject>();
    [SerializeField] ProgressBar progressBar;
    private IEnumerator Start()
    {
        // Afficher un indicateur de chargement
        ShowLoadingIndicator();

        // Charger le fichier JSON de manière asynchrone
        yield return StartCoroutine(LoadJSONFileAsync(filePath));

        // Cacher l'indicateur de chargement
        HideLoadingIndicator();

        // Utiliser les données chargées
        Debug.Log("MyList contains " + myList.Count + " items");
    }

    private IEnumerator LoadJSONFileAsync(string filePath)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(filePath))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
                yield break;
            }

            // Convertir le JSON en liste d'objets
            myList = JsonConvert.DeserializeObject<List<MyObject>>(www.downloadHandler.text);
        }
    }

    private void ShowLoadingIndicator()
    {
        if (loadingIndicator != null)
        {
            loadingIndicator.SetActive(true);
            progressBar = loadingIndicator.GetComponentInChildren<ProgressBar>();

            for (int i = 0; i < 100; i++)
            {
                progressBar.SetProgress((float)i / 100f);
              
            }
        }
    }

    private void HideLoadingIndicator()
    {
        if (loadingIndicator != null)
        {
            loadingIndicator.SetActive(false);
        }
    }

    [Serializable]
    public class MyObject
    {
        public string name;
    }
}