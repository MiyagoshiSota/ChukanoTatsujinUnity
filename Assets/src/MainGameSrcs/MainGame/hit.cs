using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using System.IO.Ports;
using UnityEngine.SceneManagement;
using UniRx;

public class hit : MonoBehaviour
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
    public int baurate;
    public string firstSerialData = "5",secondSerialData = "5",thirdSerialDate;
    public MultiTest MultiTest;

    //判定用
    public int long_poi = 0,loop_end = 0,result=0,successCount=0,goodCount=0,greatCount=0;
    public bool successCondiment = false,long_click_bl=false,single_click_bool=false,once = false,ladleSingleBool = true,wakSingleBool = true,redSingleBool = true,yellowSingleBool = true,greenSingleBool = true;
    public string hitObjName = "none";

    //コンボ用
    public int comboCount = 0;

    //SE用
    private AudioSource audioSource;
    public bool DragonBrethBoolFirst = true,DragonBrethBoolSecond = false,DragonBrethBoolTherd = false,DragonBrethBoolFourth = false;

    [SerializeField]
    private AudioClip single,successSingle,seasoning,longNotes,dragonBreth,dragonHoeru;
    //スコアバー用
    public GameObject flameThrower;

    //タイマー用
    [SerializeField]
	private int minute;
	[SerializeField]
	private float seconds;
    public AudioSource m_MyAudioSource;
	//　前のUpdateの時の秒数
	private float oldSeconds;

    // Start is called before the first frame update
    void Start()
    {
        DragonBrethBoolFirst = true; DragonBrethBoolSecond = false;DragonBrethBoolTherd = false;DragonBrethBoolFourth = false;
        audioSource = GetComponent<AudioSource>();
        PlayerPrefs.SetInt("maxCombo", 0);
        color_plate = GameObject.Find("color_plate");
    }

    // Update is called once per frame
    void Update()
    {
        firstSerialData = PlayerPrefs.GetString("FryingpanData");
        secondSerialData = PlayerPrefs.GetString("LadleData");
        thirdSerialDate = PlayerPrefs.GetString("ButtonData");
        Debug.Log("firstSerialData:" + firstSerialData + "secondSerialData:" + secondSerialData);
        Setint();
        timeCount();
        CreateFlameThrower();
        if(hitObj == null){
            /*if(firstSerialData.Contains("1")|| secondSerialData.Contains("1") || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.W)){
                ChangeSingleSE();
            }else if(thirdSerialDate.Contains("1")|| thirdSerialDate.Contains("2") || thirdSerialDate.Contains("3") || Input.GetKeyDown(KeyCode.Q)||Input.GetKeyDown(KeyCode.A)||Input.GetKeyDown(KeyCode.Z)){
                ChangeSingleSE();
            }*/
        }
        else{
        //ロングノーツの判定処理 HACK:ロングノーツ別で分けてもいいかも
        if(long_click_bl){
            /*if(thirdSerialDate.Contains("1")|| thirdSerialDate.Contains("2") || thirdSerialDate.Contains("3") || Input.GetKeyDown(KeyCode.Q)||Input.GetKeyDown(KeyCode.A)||Input.GetKeyDown(KeyCode.Z)){
                ChangeSingleSE();
            }*/
            if(hitObj.name.Contains("wokLong")){
                if (firstSerialData.Contains("1") || Input.GetKeyDown(KeyCode.T)) {
                    long_poi++;
                    GetScore();
                    if(long_poi == 1){
                        DelayAndVidivleMusic(1200,"wokLong");
                    }
                    Combo();
                }
            }
            //おたま
            else if(hitObj.name.Contains("ladleLong")){
                if(secondSerialData.Contains("1") || Input.GetKeyDown(KeyCode.R)){
                    long_poi++;
                    GetScore();
                    Combo();
                    if(long_poi == 1){
                        DelayAndVidivleMusic(1200,"ladleLong");
                    }
                }
            }
        }
        //シングルノーツの判定処理
        else if(single_click_bool){
            /*if(firstSerialData.Contains("1")|| secondSerialData.Contains("1") || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.W)){
                ChangeSingleSE();
            }else if(thirdSerialDate.Contains("1")|| thirdSerialDate.Contains("2") || thirdSerialDate.Contains("3") || Input.GetKeyDown(KeyCode.Q)||Input.GetKeyDown(KeyCode.A)||Input.GetKeyDown(KeyCode.Z)){
                ChangeSingleSE();
            }*/
            //中華鍋
            if(hitObj.name.Contains("wokSingle")){
                if(firstSerialData.Contains("1")|| Input.GetKeyDown(KeyCode.E)){
                    successCount++;
                    Destroy(hitObj);
                    GetScore();
                    single_click_bool = false;
                    DelayAndVidivleMusic(100,"wokSingle");
                    Combo();
                }
            }
            //おたま
            else if(hitObj.name.Contains("ladleSingle")){
                if(secondSerialData.Contains("1")|| Input.GetKeyDown(KeyCode.W)){
                    successCount++;
                    Destroy(hitObj);
                    GetScore();
                    single_click_bool = false;
                    DelayAndVidivleMusic(100,"ladleSingle");
                    Combo();
                }
            }
            //調味料
            else if(hitObj.name.Contains("condiment")){
                /*if(firstSerialData.Contains("1")|| secondSerialData.Contains("1") || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.W)){
                    audioSource.PlayOneShot(single);
                }*/
                //red
                if(hitObj.name.Contains("condimentRed")){
                    if(thirdSerialDate.Contains("2") || Input.GetKeyDown(KeyCode.Q)){
                        color_plate.GetComponent<Renderer>().material.color = new Color(1.0f, 0.0f, 0.0f, 1f);
                        setColorParet("condimentRed");
                        successCount++;
                        Destroy(hitObj); 
                        GetScore();
                        DelayAndVidivleMusic(100,"seasoning");
                        Combo();    
                    } 
                }
                //blue
                else if(hitObj.name.Contains("condimentBlue")){
                    if(thirdSerialDate.Contains("1") || Input.GetKeyDown(KeyCode.A)){
                        color_plate.GetComponent<Renderer>().material.color = new Color(0.0f, 0.0f, 1.0f, 1f); 
                        setColorParet("condimentBlue");
                        successCount++;
                        Destroy(hitObj);
                        GetScore();
                        DelayAndVidivleMusic(100,"seasoning");
                        Combo();
                        }
                }
                //green
                else if(hitObj.name.Contains("condimentGreen")){
                    if(thirdSerialDate.Contains("3") || Input.GetKeyDown(KeyCode.Z)){
                        color_plate.GetComponent<Renderer>().material.color = new Color(0.0f, 1.0f, 0.0f, 1f);
                        setColorParet("condimentGreen");
                        successCount++;
                        Destroy(hitObj);
                        GetScore();
                        DelayAndVidivleMusic(100,"seasoning");
                        Combo();
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
        SetBar();
        objectPosition = GameObject.Find(objectName).transform.position;
        if(objectPosition.x < -1180 && objectPosition.x > -3000){
            greatGif.SetActive (true);
            DelayAndVidivle(300,"greatGif");
            greatCount++;
        }else if((objectPosition.x < 61 && objectPosition.x > -1179) || (objectPosition.x < -3000 && objectPosition.x > -7700)){
            goodGif.SetActive (true);
            DelayAndVidivle(110,"goodGif");
            goodCount++;
        }
    }

    //スコアの加点
    void GetScore(){
        if(hitObj != null){
            if(hitObj.name.Contains("wokSingle") || hitObj.name.Contains("ladleSingle") || hitObj.name.Contains("condiment")){//ノーマルノーツの処理
                GetTimming(hitObjName);
                result += 100*successCount;
            }
            else if(hitObj.name.Contains("wokLong") || hitObj.name.Contains("ladleLong") ){
                result += 10*successCount;
            }
        }
    }
    void SetBar(){
        if(PlayerPrefs.GetInt("currentHp") + 5 > 100){
            PlayerPrefs.SetInt("currentHp",100);
        }
        else{
            PlayerPrefs.SetInt("currentHp",PlayerPrefs.GetInt("currentHp") + 5);
        }
    }
    //ブレスの生成
    void CreateFlameThrower(){
        switch(PlayerPrefs.GetString("SelectDifficulty")){
            case "veryeasy":
                if(DragonBrethBoolFourth && result > 700000){
                    DragonBreth();
                    DragonBrethBoolFourth = false;
                }
               else if(DragonBrethBoolTherd && result > 60000){
                    DragonBreth();
                    DragonBrethBoolTherd = false;
                    DragonBrethBoolFourth = true;
                }
                else if(DragonBrethBoolSecond && result > 50000){
                    DragonBreth();
                    DragonBrethBoolSecond = false;
                    DragonBrethBoolTherd = true;
                }
                else if(DragonBrethBoolFirst && result > 30000){
                    DragonBreth();
                    DragonBrethBoolFirst = false;
                    DragonBrethBoolSecond = true;
                }
                break;
            case "easy":
                if(DragonBrethBoolFourth && result > 350000){
                    DragonBreth();
                    DragonBrethBoolFourth = false;
                }
               else if(DragonBrethBoolTherd && result > 300000){
                    DragonBreth();
                    DragonBrethBoolTherd = false;
                    DragonBrethBoolFourth = true;
                }
                else if(DragonBrethBoolSecond && result > 200000){
                    DragonBreth();
                    DragonBrethBoolSecond = false;
                    DragonBrethBoolTherd = true;
                }
                else if(DragonBrethBoolFirst && result > 100000){
                    DragonBreth();
                    DragonBrethBoolFirst = false;
                    DragonBrethBoolSecond = true;
                }
                break;
            case "normal":
                if(DragonBrethBoolFourth && result > 170000){
                    DragonBreth();
                    DragonBrethBoolFourth = false;
                }
               else if(DragonBrethBoolTherd && result > 150000){
                    DragonBreth();
                    DragonBrethBoolTherd = false;
                    DragonBrethBoolFourth = true;
                }
                else if(DragonBrethBoolSecond && result > 100000){
                    DragonBreth();
                    DragonBrethBoolSecond = false;
                    DragonBrethBoolTherd = true;
                }
                else if(DragonBrethBoolFirst && result > 50000){
                    DragonBreth();
                    DragonBrethBoolFirst = false;
                    DragonBrethBoolSecond = true;
                }
                break;
            default:
                break;
        }
    }
    void DragonBreth(){
        // 生成位置
        Vector3 pos = new Vector3(-958,3207, -50);
        // プレハブを指定位置に生成
        Instantiate(flameThrower, pos, Quaternion.Euler(0, 90, 0));
        audioSource.PlayOneShot(dragonBreth);
        audioSource.PlayOneShot(dragonHoeru);
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
        else if(noteName == "wokLong" || noteName == "ladleLong"){
            audioSource.PlayOneShot(longNotes);
        }else{
            audioSource.PlayOneShot(seasoning);
        }
    }

    //コンボ数を加算
    void Combo(){
        comboCount = PlayerPrefs.GetInt("combo");
        comboCount++;
        PlayerPrefs.SetInt("combo", comboCount);
        if(comboCount > PlayerPrefs.GetInt("maxCombo")){
            PlayerPrefs.SetInt("maxCombo", comboCount);
        }
    }

    void Setint(){
        PlayerPrefs.SetInt("great",greatCount);
        PlayerPrefs.SetInt("good",goodCount);
        PlayerPrefs.SetInt("result",result);

        Debug.Log(PlayerPrefs.GetInt("great").ToString() + " " + PlayerPrefs.GetInt("good").ToString() + " " + PlayerPrefs.GetInt("result").ToString());

        PlayerPrefs.Save();
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

    void timeCount(){
        seconds += Time.deltaTime;
		if(seconds >= 60f) {
			minute++;
			seconds = seconds - 60;
		}
		oldSeconds = seconds;
    }

    //Boolをtrueにする
    void ChangeSingleBool(){
        if(firstSerialData.Contains("0")){
            wakSingleBool = true;
        }
        if(secondSerialData.Contains("0")){
            ladleSingleBool = true;
        }
        if(thirdSerialDate.Contains("null")){
            redSingleBool = true;
            greenSingleBool = true;
            yellowSingleBool = true;
        }
    }

    /*シリアル通信*/
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