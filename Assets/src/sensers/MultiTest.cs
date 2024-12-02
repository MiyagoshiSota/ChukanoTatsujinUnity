using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO.Ports;
using System.Threading;
using DG.Tweening;
using UniRx;

public class MultiTest : MonoBehaviour
{
    public delegate void SerialDataReceivedEventHandler(string message);
    public event SerialDataReceivedEventHandler OnFryingpanDataReceived;
    public event SerialDataReceivedEventHandler OnLadleDataReceived;

    public string fryingpanPortName;
    public string ladlePortName;
    public int baudRate;

    private SerialPort fryingpan_SerialPort_;
    private SerialPort ladle_SerialPort_;
    private Thread fryingpan_Thread_;
    private Thread ladle_Thread_;
    private bool isRunning_ = false;

    private string fryingpan_Message_;
    private string ladle_Message_;
    private bool fryingpan_IsNewMessageReceived_ = false;
    private bool ladle_IsNewMessageReceived_ = false;

    public static MultiTest instance;

    void Start()
    {
        PlayerPrefs.SetInt("playFinish",0);
        PlayerPrefs.SetInt("okTutrial",0);
    }
    void Awake()
    {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            Open();
        }else{
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        Debug.Log(fryingpan_Message_ + " " + ladle_Message_);
        PlayerPrefs.SetString("FryingpanData",fryingpan_Message_);
        PlayerPrefs.SetString("LadleData",ladle_Message_);
        //Debug.Log(fryingpan_Message_ + " " + ladle_Message_);
        if (Input.GetKeyDown(KeyCode.V)) {
            SceneManager.LoadScene("Tutrial");
        }
    }

    private void Open() // 通信開始
    {
        isRunning_ = true;
        try {
            fryingpan_SerialPort_ = new SerialPort(fryingpanPortName, baudRate, Parity.None, 8, StopBits.One);
            fryingpan_SerialPort_.Open();
        } catch (System.Exception e) {
            Debug.LogWarning(e.Message);
            isRunning_ = false;
        }

        try{
            ladle_SerialPort_ = new SerialPort(ladlePortName, baudRate, Parity.None, 8, StopBits.One);
            ladle_SerialPort_.Open();
        }
        catch (System.Exception e) {
            Debug.LogWarning(e.Message);
            isRunning_ = false;
        }

        Debug.Log(isRunning_);

        fryingpan_Thread_ = new Thread(FryingpanRead);
        fryingpan_Thread_.Start(); // スレッドスタート

        ladle_Thread_ = new Thread(LadleRead);
        ladle_Thread_.Start(); // スレッドスタート
        Debug.Log("Open");
    }

    private void FryingpanRead() // 読み込み
    {
        while (isRunning_ && fryingpan_SerialPort_ != null && fryingpan_SerialPort_.IsOpen) {
            try {
                if (fryingpan_SerialPort_.BytesToRead > 0) {
                    fryingpan_Message_ = fryingpan_SerialPort_.ReadLine();
                    fryingpan_IsNewMessageReceived_ = true;
                }
            } catch (System.Exception e) {
                Debug.LogWarning(e.Message);
            }
        }
    }

    private void LadleRead() // 読み込み
    {
        while (isRunning_ && ladle_SerialPort_ != null && ladle_SerialPort_.IsOpen) {
            try {
                if (ladle_SerialPort_.BytesToRead > 0) {
                    ladle_Message_ = ladle_SerialPort_.ReadLine();
                    ladle_IsNewMessageReceived_ = true;
                }
            } catch (System.Exception e) {
                Debug.LogWarning(e.Message);
            }
        }
    }
    private void StopThreads()
    {
        isRunning_ = false;

        if (fryingpan_SerialPort_ != null && fryingpan_SerialPort_.IsOpen)
        {
            fryingpan_SerialPort_.Close();
            fryingpan_SerialPort_ = null;
        }

        if (ladle_SerialPort_ != null && ladle_SerialPort_.IsOpen)
        {
            ladle_SerialPort_.Close();
            ladle_SerialPort_ = null;
        }

        if (fryingpan_Thread_ != null && fryingpan_Thread_.IsAlive)
        {
            fryingpan_Thread_.Join(); // スレッドの終了を待つ
            fryingpan_Thread_ = null;
        }

        if (ladle_Thread_ != null && ladle_Thread_.IsAlive)
        {
            ladle_Thread_.Join(); // スレッドの終了を待つ
            ladle_Thread_ = null;
        }
    }

    private void OnDestroy()
    {
        Debug.Log("Destroy");
        StopThreads();
    }
}