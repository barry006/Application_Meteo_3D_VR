using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Mdp : MonoBehaviour
{
    [SerializeField] TMP_InputField mdp;
    [SerializeField] string scene = "";




    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ClickMDP();
        }
    }



    public void ClickMDP()
    {

        if (mdp.text.ToLower() == "simplon")
        {
            SceneManager.LoadScene(scene);           
        }
    }






}
