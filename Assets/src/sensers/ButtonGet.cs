using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO.Ports;
using System.Threading;

public class ButtonGet : MonoBehaviour
{
    public delegate void SerialDataReceivedEventHandler(string message);
    public event SerialDataReceivedEventHandler OnButtonDataReceived;

    public string buttonPortName;
    public int baudRate;

    private SerialPort button_SerialPort_;
    private Thread button_Thread_;
    private bool isRunning_ = false;

    private string button_Message_;
    private bool button_IsNewMessageReceived_ = false;

    private static ButtonGet instance;

    void Awake()
    {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            Open();
        }
        else{
            Destroy(this.gameObject);
        }
    }

    void Start(){
        PlayerPrefs.SetInt("playFinish",0);
    }

    void Update()
    {
        if (button_IsNewMessageReceived_) {
            OnButtonDataReceived(button_Message_);
        }
        if(PlayerPrefs.GetInt("okTutrial") == 999){
            //OnDestroy();
        }
    }

    private void Open() // 通信開始
    {
        try {
            button_SerialPort_ = new SerialPort(buttonPortName, baudRate, Parity.None, 8, StopBits.One);
            button_SerialPort_.Open();
            isRunning_ = true;
        } catch (System.Exception e) {
            Debug.LogWarning(e.Message);
        }
        
        button_Thread_ = new Thread(ButtonRead);
        button_Thread_.Start(); // スレッドスタート
    }

    private void ButtonRead() // 読み込み
    {
        while (isRunning_ && button_SerialPort_ != null && button_SerialPort_.IsOpen) {
            try {
                if (button_SerialPort_.BytesToRead > 0) {
                    button_Message_ = button_SerialPort_.ReadLine();
                    button_IsNewMessageReceived_ = true;
                }
            } catch (System.Exception e) {
                Debug.LogWarning(e.Message);
            }
        }
    }
    private void StopButton()
    {
        isRunning_ = false;
        if(button_SerialPort_ != null && button_SerialPort_.IsOpen)
        {
            button_SerialPort_.Close();
            button_SerialPort_ = null;
        }
        if (button_Thread_ != null && button_Thread_.IsAlive)
        {
            button_Thread_.Join(); // スレッドの終了を待つ
            button_Thread_ = null;
        }
    }
    private void OnDestroy()
    {
        StopButton();
    }
}