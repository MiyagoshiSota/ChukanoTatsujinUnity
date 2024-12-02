using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public ButtonGet ButtonGet;

    private string buttonData;
    private bool once;

    void Start()
    {
        ButtonGet.OnButtonDataReceived += OnButtonDataReceived;
        PlayerPrefs.SetString("ButtonData","0");
    }

    void Update()
    {
        if(buttonData.Contains("1")) {
           PlayerPrefs.SetString("ButtonData","1");;
        } else if(buttonData.Contains("2")) {
            PlayerPrefs.SetString("ButtonData","2");;
        } else if(buttonData.Contains("3")) {
            PlayerPrefs.SetString("ButtonData","3");;
        } else {
            PlayerPrefs.SetString("ButtonData","null");;
        }
        //Debug.Log(buttonData);
    }

    void OnButtonDataReceived(string message) // 受け取り用関数
    {
        try {
            buttonData = message;
        } catch (System.Exception e) {
            Debug.LogWarning(e.Message);
        }
    }
}