using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectDifficulty : MonoBehaviour
{
    public string serectDifficulty;
    public string thirdSerialDate;
    private bool selectBool = false;
    public GameObject targetObject;
    public SpriteRenderer imageRenderer1,imageRenderer2,imageRenderer3;

    void Start()
    {        
        targetObject.SetActive(false);
        Color newColor1 = imageRenderer1.color;
        newColor1.a = 0;
        imageRenderer1.color = newColor1;

        Color newColor2 = imageRenderer2.color;
        newColor2.a = 0;
        imageRenderer2.color = newColor2;

        Color newColor3 = imageRenderer3.color;
        newColor3.a = 0;
        imageRenderer3.color = newColor3;
        selectBool = false;
    }

    // Update is called once per frame
    void Update()
    {
        thirdSerialDate = PlayerPrefs.GetString("ButtonData");
        // easy
        if(!selectBool && (Input.GetKeyDown(KeyCode.Q)||thirdSerialDate=="3")){
            StartCoroutine (SelectDifficultyScene("veryeasy"));
            Color newColor = imageRenderer1.color;
            newColor.a = 1;
            imageRenderer1.color = newColor;
            selectBool = true;
        }
        // normal
        else if(!selectBool && (Input.GetKeyDown(KeyCode.A)||thirdSerialDate=="1")){
            StartCoroutine (SelectDifficultyScene("normal"));
            Color newColor = imageRenderer2.color;
            newColor.a = 1;
            imageRenderer2.color = newColor;
            selectBool = true;
        }
        // hard
        else if(!selectBool && (Input.GetKeyDown(KeyCode.Z)||thirdSerialDate=="2")){
            StartCoroutine (SelectDifficultyScene("easy"));
            Color newColor = imageRenderer3.color;
            newColor.a = 1;
            imageRenderer3.color = newColor;
            selectBool = true;
        }
        else if(Input.GetKeyDown(KeyCode.X)){
            StartCoroutine (SelectDifficultyScene("hard"));
        }
    }

    // 難易度を選択したら、その難易度をPlayerPrefsに保存
    public IEnumerator SelectDifficultyScene(string serectDifficulty){
        PlayerPrefs.SetString("SelectDifficulty",serectDifficulty);
        targetObject.SetActive(true);
        yield return new WaitForSeconds(2.8f);
        Debug.Log(PlayerPrefs.GetString("SelectDifficulty"));
        SceneManager.LoadScene("Tutrial");
    }
}
