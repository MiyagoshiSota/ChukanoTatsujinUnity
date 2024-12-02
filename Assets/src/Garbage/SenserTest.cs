using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using System.IO.Ports;
using UnityEngine.SceneManagement;
using UniRx;

public class SenserTest : MonoBehaviour
{
    //センサー用 
    private SerialPort firstSerial,secondSerial,thirdSerial;
    public string firstSerialName,secondSerialName,thirdSerialName;
    public int firstSerialDate = 0,secondSerialDate = 0,thirdSerialDate = 0,baurate;
    

    // Start is called before the first frame update
    void Start()
    {
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

        /*
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
        }*/
        Scheduler.ThreadPool.Schedule (() => ReadData ()).AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void ReadSenserData(){
        firstSerialDate = PlayerPrefs.GetInt("firstSerialDate");
    }

    //シリアル通信
    void ReadData(){
        while (true)
        {
            ReadSenserData();
            //Debug.Log("ReadData");
            firstSerialDate = this.firstSerial.ReadByte();
            secondSerialDate = this.secondSerial.ReadByte();

            if(this.thirdSerial.BytesToRead!=0){
                thirdSerialDate = this.thirdSerial.ReadByte();
            }
        }
    }
}
