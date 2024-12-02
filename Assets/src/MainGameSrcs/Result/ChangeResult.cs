using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeResult : MonoBehaviour
{
    public TMP_Text goku1,goku2;
    public TMP_Text syuu1,syuu2;
    public TMP_Text fu1,fu2;
    public TMP_Text total1,total2;

    public TMP_Text maxComb1,maxComb2;

    public GameObject tatsuijin,bannin,deshi;
    public int result;

    // Start is called before the first frame update
    void Start()
    {
        goku1.text = PlayerPrefs.GetInt("great").ToString();
        goku2.text = PlayerPrefs.GetInt("great").ToString();

        syuu1.text = PlayerPrefs.GetInt("good").ToString();
        syuu2.text = PlayerPrefs.GetInt("good").ToString();

        fu1.text = PlayerPrefs.GetInt("miss").ToString();
        fu2.text = PlayerPrefs.GetInt("miss").ToString();

        total1.text = (PlayerPrefs.GetInt("result")/10).ToString();
        total2.text = (PlayerPrefs.GetInt("result")/10).ToString();
        result = PlayerPrefs.GetInt("result");

        maxComb1.text = PlayerPrefs.GetInt("maxCombo").ToString();
        maxComb2.text = PlayerPrefs.GetInt("maxCombo").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        ResultEvaluation();
    }
    
    void ResultEvaluation(){
        switch(PlayerPrefs.GetString("SelectDifficulty")){
            case "veryeasy":
                if(result > 60000){
                    tatsuijin.SetActive(true);
                    bannin.SetActive(false);
                    deshi.SetActive(false);
                }
               else if(result > 30000){
                    bannin.SetActive(true);
                    deshi.SetActive(false);
                    tatsuijin.SetActive(false);
                }
                else if(result > 0){
                    deshi.SetActive(true);
                    bannin.SetActive(false);
                    tatsuijin.SetActive(false);
                }
                break;
            case "easy":
                if(result > 300000){
                    tatsuijin.SetActive(true);
                    bannin.SetActive(false);
                    deshi.SetActive(false);
                }
               else if(result > 200000){
                    bannin.SetActive(true);
                    deshi.SetActive(false);
                    tatsuijin.SetActive(false);
                }
                else if(result > 0){
                    deshi.SetActive(true);
                    bannin.SetActive(false);
                    tatsuijin.SetActive(false);
                }
                break;
            case "normal":
                if(result > 150000){
                    tatsuijin.SetActive(true);
                    bannin.SetActive(false);
                    deshi.SetActive(false);
                }
               else if(result > 100000){
                    bannin.SetActive(true);
                    deshi.SetActive(false);
                    tatsuijin.SetActive(false);
                }
                else if(result > 0){
                    deshi.SetActive(true);
                    bannin.SetActive(false);
                    tatsuijin.SetActive(false);
                }
                break;
            default:
                break;
        }
    }
}
