using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using System.IO.Ports;
using UnityEngine.SceneManagement;
using UniRx;
using System.Threading;

public class ReadSenserData : MonoBehaviour
{
    //センサー用 
    private SerialPort firstSerial,secondSerial,thirdSerial;
    public string firstSerialName,secondSerialName,thirdSerialName;
    public int thirdSerialData = 0,baurate;
    public string firstSerialData = "5",secondSerialData;

    public string a;

    private string fryingpanData;
    private string ladleData;
    private bool once;

    private Thread fryingpan_Thread_;
    

    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 240;

        this.firstSerial = new SerialPort (firstSerialName, baurate, Parity.None, 8, StopBits.One);
        this.secondSerial = new SerialPort (secondSerialName, baurate, Parity.None, 8, StopBits.One);
        this.thirdSerial = new SerialPort (thirdSerialName, baurate, Parity.None, 8, StopBits.One);

        try
        {
            this.firstSerial.Open();
        } 
        catch(Exception e)
        {
            Debug.Log ("can not open serial port");
        }

        fryingpan_Thread_ = new Thread(OnFryingpanDataReceived);
        fryingpan_Thread_.Start(); // スレッドスタート
        
        /*try{
            this.thirdSerial.Open();

            thirdSerial.NewLine = "\n";
            thirdSerial.DtrEnable = true;  
            thirdSerial.RtsEnable = true;  
            thirdSerial.DiscardInBuffer();  
            thirdSerial.ReadTimeout = 20;
        }
        catch(Exception e){
            Debug.Log ("can not open serial port:2");
        }*/

        //Scheduler.ThreadPool.Schedule (() => ReadData ()).AddTo(this);
    }

    void Update(){
    }

    //センサーデータの読み込みと判定
    private void OnFryingpanDataReceived() // 読み込み
    {
        while (firstSerial != null && firstSerial.IsOpen) {
            try {
                if (firstSerial.BytesToRead > 0) {
                    firstSerialData = firstSerial.ReadLine();
                    Debug.Log(firstSerialData);
                }
            } catch (System.Exception e) {
                Debug.LogWarning(e.Message);
            }
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
