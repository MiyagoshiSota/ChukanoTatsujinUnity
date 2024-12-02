using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MultiTry : MonoBehaviour
{
    public MultiTest MultiTest;

    private string fryingpanData;
    private string ladleData;
    private bool once;

    void Start()
    {
        MultiTest.OnFryingpanDataReceived += OnFryingpanDataReceived;
        MultiTest.OnLadleDataReceived += OnLadleDataReceived;
    }

    void Update()
    {
        if(fryingpanData.Contains("1")) {
            if(once) {
                once = false;
                Debug.Log("fryingpan:1");
            } else {
                once = true;
            }
        }
        if(ladleData.Contains("1")) {
            if(once) {
                once = false;
                Debug.Log("ladle:1");
            } else {
                once = true;
            }
        }
    }

    void OnFryingpanDataReceived(string message)
    {
        try {
            fryingpanData = message;
        } catch (System.Exception e) {
            Debug.LogWarning(e.Message);
        }
    }

    void OnLadleDataReceived(string message)
    {
        try {
            ladleData = message;
        } catch (System.Exception e) {
            Debug.LogWarning(e.Message);
        }
    }
}