using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StartScene : MonoBehaviour
{

    private int first = 0;
    private bool firstBool = true;
    public string thirdSerialDate;
    public GameObject targetObject;

    void Start()
    {
        targetObject.SetActive(false);
        firstBool = true;
    }

    void Update()
    {
        if(firstBool){
            thirdSerialDate = PlayerPrefs.GetString("ButtonData");
        }
        if (firstBool && Input.GetKeyDown(KeyCode.Q) || thirdSerialDate=="1"||thirdSerialDate=="2"||thirdSerialDate=="3") {
            StartCoroutine ("next");
            firstBool = false;
        }
        if(first == 0){
            firstSettings();
            first = 1;
        }
    }

    private IEnumerator next()
    {
        targetObject.SetActive(true);
        yield return new WaitForSeconds(2.8f); // Wait for 2.8 seconds.
        SceneManager.LoadScene("Select");
    }

    void firstSettings(){
        PlayerPrefs.SetInt("playFinish",0);
        PlayerPrefs.SetInt("okTutrial",0);
        PlayerPrefs.SetInt("okNormal",0);
        PlayerPrefs.SetInt("SelectDifficulty",0);
    }
}