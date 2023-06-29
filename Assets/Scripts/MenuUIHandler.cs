using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    string input;
    public string playerName = "Player";

    private InformationHandler informationHandler;

    public void Start()
    {
        informationHandler = GameObject.Find("InformationHandler").GetComponent<InformationHandler>();
    }

    
    public void StartNew()
    {
        informationHandler.SetPlayerName(playerName);
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }

    public void SetName(string name)
    {
        if(!string.IsNullOrWhiteSpace(name))
            playerName = name;
    }

    
}
