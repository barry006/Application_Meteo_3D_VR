using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.IO;
using Newtonsoft.Json;
using System.Net;

public class Pagination : MonoBehaviour
{
    //---------------------Test script Autocomplete to add system pagination------------------
    [SerializeField] int resultsPerPage = 20;
    public TMP_InputField inputField;
    public GameObject panel;
    [SerializeField] public RectTransform resultsParent;
    [SerializeField] RectTransform prefab;
    [SerializeField] List<MyObject> listName = new List<MyObject>();
    [SerializeField] List<string> listName2 = new List<string>();
    [SerializeField] bool panelActived;
    [SerializeField] ScrollRect myScrollRect;
    [SerializeField] int nombreDeResultatDansLaBarreDeRecherche = 0;


    [System.Serializable]
    public class MyObject
    {
        public string name;
    }

    private List<string> GetResults(string input, int page, int pageSize)
    {
        List<string> results = new List<string>();

        int startIndex = (page - 1) * pageSize;
        int endIndex = startIndex + pageSize;

        nombreDeResultatDansLaBarreDeRecherche = 0;
        foreach (string s in listName2)
        {
            if (s.StartsWith(input, StringComparison.CurrentCultureIgnoreCase))
            {
                nombreDeResultatDansLaBarreDeRecherche++;
                if (nombreDeResultatDansLaBarreDeRecherche > endIndex)
                {
                    break;
                }
                else if (nombreDeResultatDansLaBarreDeRecherche > startIndex)
                {
                    results.Add(s);
                }
            }
        }

        return results;
    }

    private void FillResults(List<string> results, int page)
    {
        results.Sort();

        for (int resultIndex = 0; resultIndex < results.Count; resultIndex++)
        {
            RectTransform child = Instantiate(prefab) as RectTransform;
            child.GetComponentInChildren<Text>().text = results[resultIndex];
            child.SetParent(resultsParent);
        }
    }

    public void ClearResults()
    {
        for (int i = 0; i < resultsParent.transform.childCount; i++)
        {
            Destroy(resultsParent.transform.GetChild(i).gameObject);
        }
    }

    private void OnInputValueChanged(string newText)
    {
        if (inputField.text != string.Empty)
        {
            panel.SetActive(true);
            ClearResults();
            FillResults(GetResults(newText, 0, resultsPerPage), 0);
            myScrollRect.verticalNormalizedPosition = 1f;
            panelActived = true;
        }
        else
        {
            inputField.text = string.Empty;
            panel.SetActive(false);
            panelActived = false;
            ClearResults();
        }
    }

    public void PreviousPage()
    {
        int currentPage = GetCurrentPage();
        if (currentPage > 0)
        {
            int newPage = currentPage - 1;
            ClearResults();
            FillResults(GetResults(inputField.text, newPage, resultsPerPage), newPage);
            myScrollRect.verticalNormalizedPosition = 1f;
        }
    }

    public void NextPage()
    {
        int currentPage = GetCurrentPage();
        int lastPage = GetLastPage();
        if (currentPage < lastPage)
        {
            int newPage = currentPage + 1;
            ClearResults();
            FillResults(GetResults(inputField.text, newPage, resultsPerPage), newPage);
            myScrollRect.verticalNormalizedPosition = 1f;
        }
    }

    private int GetCurrentPage()
    {
        if (nombreDeResultatDansLaBarreDeRecherche == 0)
        {
            return 0;
        }
        return Mathf.FloorToInt((float)(resultsParent.transform.childCount - 1) / (float)resultsPerPage) + 1;
    }

    private int GetLastPage()
    {
        return Mathf.CeilToInt((float)nombreDeResultatDansLaBarreDeRecherche / (float)resultsPerPage);
    }

    private void OnInputValueChanged2(string newText)
    {
        if (inputField.text != string.Empty)
        {
            panel.SetActive(true);
            ClearResults();
            int page = 0; // première page à afficher
            FillResults(GetResults(newText, page, resultsPerPage), page);
            myScrollRect.verticalNormalizedPosition = 1f;
            panelActived = true;
        }
        else
        {
            inputField.text = string.Empty;
            panel.SetActive(false);
            panelActived = false;
            ClearResults();
        }
    }
}