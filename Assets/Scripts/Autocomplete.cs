using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using TMPro;
using System.IO;
using Newtonsoft.Json;
using System.Net;
using UnityEngine.Networking;
using System.Collections;
public class Autocomplete : MonoBehaviour
{   //------------------------------------------------------------------------------------------------------------------
    //---------------------------------------------Scipt on TMP_InputField.---------------------------------------------
    //------------------------------------------------------------------------------------------------------------------
    public TMP_InputField inputField;
    public GameObject panel;

    [SerializeField] public RectTransform resultsParent;     //GameObject with rect trasnsfrom and vertical layout.    
    [SerializeField] RectTransform prefab;                   //Prefab with text.
    [SerializeField] ScrollRect _myScrollRect;
    [SerializeField] List<MyObject> listName = new List<MyObject>();
    [SerializeField] int nombreDeResultatDansLaBarreDeRecherche = 20;

    // string filePath = @"Assets/Json/JustName.json";
    string _url = "";
    bool _panelActived;
    [SerializeField] Autocomplete autocomplete;
    [SerializeField] API_WebRequest api_WebRequest;


    [SerializeField]
    List<string> _villeEnDur = new List<string>() { "Paris", "Marseille", "Lyon", "Toulouse", "Nice", "Nantes", "Strasbourg", "Montpellier", "Bordeaux", "Lille", "Rennes", "Reims", "Le Havre", "Saint-Etienne", "Toulon", "Grenoble", "Dijon", "Angers",
        "Nîmes", "Villeurbanne", "Le Mans", "Aix-en-Provence", "Clermont-Ferrand", "Brest", "Tours", "Limoges", "Amiens", "Annecy", "Perpignan", "Boulogne-Billancourt", "Metz", "Besançon", "Orléans", "Saint-Denis", "Mulhouse", "Rouen", "Saint-Paul", "Caen", "Nancy",
        "Argenteuil", "Montreuil", "Saint-Pierre", "Roubaix", "Tourcoing", "Nanterre", "Avignon", "Vitry-sur-Seine", "Créteil", "Dunkerque", "Poitiers", "Asnières-sur-Seine", "Courbevoie", "Versailles", "Colombes", "Fort-de-France", "Aulnay-sous-Bois", "Saint-Maur-des-Fossés",
        "Rueil-Malmaison", "Champigny-sur-Marne", "Aubervilliers", "Saint-Priest", "Le Tampon", "Béziers", "Saint-Nazaire", "La Rochelle", "La Seyne-sur-Mer", "Calais", "Choisy-le-Roi", "Pau", "Saint-André", "Ivry-sur-Seine", "Cergy", "Chelles", "Chambéry", "Saint-Brieuc",
        "Talence", "Castres", "Angoulême", "Douai", "Mérignac", "Tarbes", "Thionville", "Vannes", "Nevers", "Villeneuve-d'Ascq", "Montauban", "Lorient", "Massy", "Bobigny", "Saint-Malo", "Valenciennes", "Gennevilliers" };



    [System.Serializable]
    public class MyObject
    {
        public string name;
    }



    //---------------------Recup Json--------------------------------------
    private void Awake()
    {
        inputField.onValueChanged.AddListener(OnInputValueChanged);
       
        
        //StartCoroutine(AddToList_Town_With_Url2());



        /*
        using (WebClient webClient = new WebClient())
        {
            _url = webClient.DownloadString("https://raw.githubusercontent.com/barry006/Json/921b7d15028767575f094bedbfe452b65cd42d06/JustName.json");
            AddToList_Town_With_Url();
            
        }
        */


        /*
        if (File.Exists(filePath))
        {
            AddToList_Town_With_Path();
        }
        */
    }
    //---------------------Recup Json--------------------------------------



    //---------------------button--------------------------------------
    private void OnInputValueChanged(string newText)
    {
        if (inputField.text != string.Empty)
        {
            panel.SetActive(true);
            ClearResults();
            FillResults(GetResults(newText));
            _myScrollRect.verticalNormalizedPosition = 1f;    //Premier élément du group en haut du scrollRect.
            _panelActived = true;

        }
        else
        {
            inputField.text = string.Empty;
            panel.SetActive(false);
            _panelActived = false;
            ClearResults();
        }
    }
    public void ClearResults()
    {

        for (int childIndex = resultsParent.childCount - 1; childIndex >= 0; --childIndex)
        {
            Transform child = resultsParent.GetChild(childIndex);
            child.SetParent(null);
            Destroy(child.gameObject);
        }
    }
    //---------------------button--------------------------------------





    private void FillResults(List<string> results)
    {
        results.Sort();

        for (int resultIndex = 0; resultIndex < results.Count; resultIndex++)
        {
            RectTransform child = Instantiate(prefab) as RectTransform;
            // child.GetComponent<OnClickAutoCompletion>().RecupReff(autocomplete, api_WebRequest);
            child.GetComponentInChildren<Text>().text = results[resultIndex];
            child.SetParent(resultsParent);


        }
    }

    private List<string> GetResults(string input)
    {
        List<string> results = new List<string>();

        var count = 0;
        foreach (string s in _villeEnDur)
        {
            if (s.StartsWith(input, StringComparison.CurrentCultureIgnoreCase))
            {
                //results.Add(s);
                results.Insert(0, s);
                count++;

                if (count == nombreDeResultatDansLaBarreDeRecherche)
                {

                    break;
                }
            }
        }
        return results;
    }  
    
    private List<string> GetResults2(string input)
    {
        List<string> results = new List<string>();

        var count = 0;
        foreach (MyObject s in listName)
        {
            if (s.name.StartsWith(input, StringComparison.CurrentCultureIgnoreCase))
            {
                //results.Add(s);
                results.Insert(0, s.name);
                count++;

                if (count == nombreDeResultatDansLaBarreDeRecherche)
                {

                    break;
                }
            }
        }
        return results;
    }


    /*
    public void AddToList_Town_With_Url()
    {
        listName = JsonConvert.DeserializeObject<List<MyObject>>(_url);
    }



    public IEnumerator AddToList_Town_With_Url2()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://raw.githubusercontent.com/barry006/Json/921b7d15028767575f094bedbfe452b65cd42d06/JustName.json");
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Error: " + www.error);
        }
        else
        {
            listName = JsonConvert.DeserializeObject<List<MyObject>>(www.downloadHandler.text);
        }
    }

     public void AddToList_Town_With_Path()
     {
         string classData = File.ReadAllText(filePath);
         listName = JsonConvert.DeserializeObject<List<MyObject>>(classData);        
     } */
}