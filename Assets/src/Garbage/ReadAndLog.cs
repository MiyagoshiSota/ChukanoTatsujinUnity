using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using System.IO.Ports;
using UnityEngine.SceneManagement;
using UniRx;

public class ReadAndLog : MonoBehaviour
{
    //センサー用 
    private SerialPort firstSerial,secondSerial,thirdSerial;
    public string firstSerialName,secondSerialName,thirdSerialName;
    public int baurate;
    public string firstSerialDate,secondSerialDate;
    public int thirdSerialDate;
    

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
            //this.secondSerial.Open();
        } 
        catch(Exception e)
        {
            Debug.Log ("can not open serial port");
        }

        try{
            this.thirdSerial.Open();

            thirdSerial.NewLine = "\n";
            thirdSerial.DtrEnable = true;  
            thirdSerial.RtsEnable = true;  
            thirdSerial.DiscardInBuffer();  
            thirdSerial.ReadTimeout = 20;
        }
        catch(Exception e){
            Debug.Log ("can not open serial port:2");
        }
        Scheduler.ThreadPool.Schedule (() => ReadData ()).AddTo(this);
    }

    void Update(){
        if(this.thirdSerial.BytesToRead != 0){
            thirdSerialDate = this.thirdSerial.ReadByte();
        }
    }

    public void ReadData(){
        while(true){
            firstSerialDate = this.firstSerial.ReadLine();
            //secondSerialDate = this.secondSerial.ReadLine();
            Debug.Log("firstSerialDate:"+firstSerialDate+" secondSerialDate:"+secondSerialDate+" thirdSerialDate:"+thirdSerialDate);
        }
    }
}
