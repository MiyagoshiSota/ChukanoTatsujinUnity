using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Threading.Tasks;
using System.IO.Ports;
using UnityEngine.SceneManagement;
using UniRx;

public class TutrialHit : MonoBehaviour
{
    private SpriteRenderer SpriteRenderer { get; set; }

    GameObject a;
    public GameObject color_plate,red,blue,green;
    GameObject hitObj;
    [SerializeField] GameObject greatGif,goodGif,singleMusic,longMusic;
    Vector3 objectPosition;

    //センサー用 
    private SerialPort firstSerial,secondSerial,thirdSerial;
    public string firstSerialName,secondSerialName,thirdSerialName;
    public string thirdSerialData;
    public string firstSerialData = "5",secondSerialData = "5";
    public MultiTest MultiTest;
    public int baurate;


    //判定用
    public int long_poi = 0,loop_end = 0,result=0,successCount=0,goodCount=0,greatCount=0,okCount=0,oldFirst,oldSecond;
    public bool successCondiment = false,long_click_bl=false,single_click_bool=false,once = true,ladleSingleBool = true,wakSingleBool = true,redSingleBool = true,yellowSingleBool = true,greenSingleBool = true;
    public string hitObjName = "none";

    //SE用
    private AudioSource audioSource;
    public bool longSEBool = true;

    [SerializeField]
    private AudioClip single,successSingle,seasoning,longNotes,dragonBreth,dragonHoeru;

    // Start is called before the first frame update
    void Start()
    {
        longSEBool = true;
        audioSource = GetComponent<AudioSource>();

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 240;

        color_plate = GameObject.Find("color_plate");
        PlayerPrefs.SetInt("okTutrial",0);
    }

    // Update is called once per frame
    void Update()
    {
        firstSerialData = PlayerPrefs.GetString("FryingpanData");
        secondSerialData = PlayerPrefs.GetString("LadleData");
        thirdSerialData = PlayerPrefs.GetString("ButtonData");
        Debug.Log("firstSerialData:" + firstSerialData + " secondSerialData:" + secondSerialData + " thirdSerialData:" + thirdSerialData);
        ChangeSingleBool();
            if(hitObj == null){ 
                /*if(firstSerialData.Contains("1") || secondSerialData.Contains("1") || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.W)){
                    ChangeSingleSE();
                }else if(thirdSerialData.Contains("1") || thirdSerialData.Contains("2") || thirdSerialData.Contains("3") || Input.GetKeyDown(KeyCode.Q)||Input.GetKeyDown(KeyCode.A)||Input.GetKeyDown(KeyCode.Z)){
                    ChangeSingleSE();
                }*/
            }
            else{
                //ロングノーツの判定処理
                    if(long_click_bl){
                        if(hitObj.name.Contains("wokLong")){
                            if (firstSerialData.Contains("1") || Input.GetKeyDown(KeyCode.T)) {
                                long_poi++;
                                GetScore();
                                if(long_poi == 1){
                                    DelayAndVidivleMusic(1200,"wokLong");
                                }
                            }
                        }
                        //おたま
                        else if(hitObj.name.Contains("ladleLong")){
                            if(secondSerialData.Contains("1") || Input.GetKeyDown(KeyCode.R)){
                                long_poi++;
                                GetScore();
                                if(long_poi == 1){
                                    DelayAndVidivleMusic(1200,"ladleLong");
                                }
                            }
                        }
                    }
                    //シングルノーツの判定処理
                    else if(single_click_bool){
                        //中華鍋
                        /*if(firstSerialData.Contains("1")|| secondSerialData.Contains("1") || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.W)){
                            ChangeSingleSE();
                        }else if(thirdSerialData.Contains("1")|| thirdSerialData.Contains("2") || thirdSerialData.Contains("3") || Input.GetKeyDown(KeyCode.Q)||Input.GetKeyDown(KeyCode.A)||Input.GetKeyDown(KeyCode.Z)){
                            ChangeSingleSE();
                        }*/
                        if(hitObj.name.Contains("wokSingle")){
                            if(firstSerialData.Contains("1")|| Input.GetKeyDown(KeyCode.E)){
                                Debug.Log("wokSingle");
                                setPrehub();
                                Destroy(hitObj);
                                GetScore();
                                single_click_bool = false;
                                DelayAndVidivleMusic(100,"wokSingle");
                            }
                        }
                        //おたま
                        else if(hitObj.name.Contains("ladleSingle")){
                            if(secondSerialData.Contains("1")|| Input.GetKeyDown(KeyCode.W)){
                                Debug.Log("ladleSingle");
                                setPrehub();
                                Destroy(hitObj);
                                GetScore();
                                single_click_bool = false;
                                DelayAndVidivleMusic(100,"ladleSingle");
                            }
                        }
                        //調味料
                        else if(hitObj.name.Contains("condiment")){
                            //red
                            if(hitObj.name.Contains("condimentRed")){
                                if(thirdSerialData.Contains("2") || Input.GetKeyDown(KeyCode.Q)){
                                    color_plate.GetComponent<Renderer>().material.color = new Color(1.0f, 0.0f, 0.0f, 1f);
                                    setColorParet("condimentRed");
                                    setPrehub();
                                    Destroy(hitObj); 
                                    DelayAndVidivleMusic(100,"seasoning");
                                    GetScore();    
                                } 
                            }
                            //blue
                            else if(hitObj.name.Contains("condimentBlue")){
                                if(thirdSerialData.Contains("1") || Input.GetKeyDown(KeyCode.A)){
                                    setPrehub();
                                    color_plate.GetComponent<Renderer>().material.color = new Color(0.0f, 0.0f, 1.0f, 1f); 
                                    setColorParet("condimentBlue");
                                    successCount++;
                                    Destroy(hitObj);
                                    DelayAndVidivleMusic(100,"seasoning");
                                    GetScore();
                                    }
                            }
                            //green
                            else{
                                if(thirdSerialData.Contains("3") || Input.GetKeyDown(KeyCode.Z)){
                                    setPrehub();
                                    color_plate.GetComponent<Renderer>().material.color = new Color(0.0f, 1.0f, 0.0f, 1f);
                                    setColorParet("condimentGreen");
                                    successCount++;
                                    Destroy(hitObj);
                                    DelayAndVidivleMusic(100,"seasoning");
                                    GetScore();
                                }
                            }
                        }
                    }
            }
    }

    /*衝突判定 */
    //入った時
    void OnCollisionEnter(Collision collision)
    {
        hitObj = collision.gameObject;
        hitObjName = collision.gameObject.name;

        //ロングノーツが入った時 
        if(collision.gameObject.name.Contains("wokLong") || collision.gameObject.name.Contains("ladleLong")){
            long_click_bl = true;
        }
        //シングルノーツが入った時
        else{
            single_click_bool = true;
        }
    }
    //出た時
    void OnCollisionExit(Collision collision){
        //ロングノーツが出た時
        if(collision.gameObject.name.Contains("wokLong") || collision.gameObject.name.Contains("ladleLong")){
            long_click_bl = false;
            GetScore();
            if(long_poi > 0){
                setPrehub();
            }
            long_poi = 0;
        }
        else if(collision.gameObject.name.Contains("wok_long_end") || collision.gameObject.name.Contains("ladle_long_end")){
            longMusic.SetActive(false);
        }
        //シングルノーツが出た時
        else{
            single_click_bool = false;
        }
        hitObj = null;
    }

    //タイミング判定
    void GetTimming(string objectName){
        objectPosition = GameObject.Find(objectName).transform.position;
        if(objectPosition.x < -1180 && objectPosition.x > -4000){
            greatGif.SetActive (true);
            DelayAndVidivle(300,"greatGif");
            greatCount++;
        }else if((objectPosition.x < 61 && objectPosition.x > -1179) || (objectPosition.x < -4001 && objectPosition.x > -7700)){
            goodGif.SetActive (true);
            DelayAndVidivle(110,"goodGif");
            goodCount++;
        }
    }

    //スコアの加点
    void GetScore(){
        //ここエラーが出る
            if(hitObj == null){
            }
            else if(hitObj.name.Contains("wokSingle") || hitObj.name.Contains("ladleSingle") || hitObj.name.Contains("condiment")){//ノーマルノーツの処理
                GetTimming(hitObjName);
            }
            else if(hitObj.name.Contains("wokLong") || hitObj.name.Contains("ladleLong") ){
            }
    }


    //DelayしてGifを消す
    async void DelayAndVidivle(int delayTime,string gifNameString)
    {
        await Task.Delay(delayTime);
        switch (gifNameString)
        {
            case "greatGif":
                greatGif.SetActive (false);
                break;
            case "goodGif":
                goodGif.SetActive (false);
                break;
            default:
                break;
        } 
    }

    //Delayして音を鳴らす
    async void DelayAndVidivleMusic(int delayTime,string noteName)
    {
        if(noteName == "wokSingle" || noteName == "ladleSingle"){
            audioSource.PlayOneShot(successSingle);
        }
        else if((noteName == "wokLong" || noteName == "ladleLong")){
            Debug.Log("long");
            audioSource.PlayOneShot(longNotes);
        }else{
            audioSource.PlayOneShot(seasoning);
        }
    }

    void Setint(){
        PlayerPrefs.SetInt("great",greatCount);
        PlayerPrefs.SetInt("good",goodCount);
        PlayerPrefs.SetInt("result",result);

        Debug.Log(PlayerPrefs.GetInt("great").ToString() + " " + PlayerPrefs.GetInt("good").ToString() + " " + PlayerPrefs.GetInt("result").ToString());

        PlayerPrefs.Save();
    }

    /* PlayerPrefsの取得(チュートリアルデータとセンサーデータ) */
    void setPrehub(){
        okCount = PlayerPrefs.GetInt("okTutrial")+1;
        PlayerPrefs.SetInt("okTutrial",okCount);
        PlayerPrefs.Save();
    }
    void GetSenserData(){
        firstSerialData = PlayerPrefs.GetString("firstSerialData");
        secondSerialData = PlayerPrefs.GetString("secondSerialData");
        thirdSerialData = PlayerPrefs.GetString("ButtonData");
    }

    //調味料の色で背景色を変える
    void setColorParet(string name){
        switch (name)
        {
            case "condimentRed":
                red.SetActive(true);
                blue.SetActive(false);
                green.SetActive(false);
                break;
            case "condimentBlue":
                red.SetActive(false);
                blue.SetActive(true);
                green.SetActive(false);
                break;
            case "condimentGreen":
                red.SetActive(false);
                blue.SetActive(false);
                green.SetActive(true);
                break;
            default:
                break;
        }
    }

    //SEを一回だけ鳴らすようにする
    void ChangeSingleSE(){
        if(wakSingleBool){
            audioSource.PlayOneShot(successSingle);
            wakSingleBool = false;
        }else if(ladleSingleBool){
            audioSource.PlayOneShot(successSingle);
            ladleSingleBool = false;
        }else if(redSingleBool){
            redSingleBool = false;
            audioSource.PlayOneShot(seasoning);
        }else if(greenSingleBool){
            greenSingleBool = false;
            audioSource.PlayOneShot(seasoning);
        }else if(yellowSingleBool){
            yellowSingleBool = false;
            audioSource.PlayOneShot(seasoning);
        }
    }
    //Boolをtrueにする
    void ChangeSingleBool(){
        if(firstSerialData.Contains("0")){
            wakSingleBool = true;
        }
        if(secondSerialData.Contains("0")){
            ladleSingleBool = true;
        }
        if(thirdSerialData.Contains("null")){
            redSingleBool = true;
            greenSingleBool = true;
            yellowSingleBool = true;
        }
    }

    /* シリアル通信 */
    void OnFryingpanDataReceived(string message)//中華鍋
    {
        try {
            firstSerialData = message;
        } catch (System.Exception e) {
            Debug.LogWarning(e.Message);
        }
    }

    void OnLadleDataReceived(string message)//おたま
    {
        try {
            secondSerialData = message;
        } catch (System.Exception e) {
            Debug.LogWarning(e.Message);
        }
    }
}