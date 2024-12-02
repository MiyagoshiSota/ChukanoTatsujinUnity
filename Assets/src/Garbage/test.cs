using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO.Ports;

public class test : MonoBehaviour {

    public string portName;
    public int baurate;
    //public GunController tama;
    //public TextController text;

    private SerialPort serial;
    private int date;

    void Start () 
    {
        this.serial = new SerialPort (portName, baurate, Parity.None, 8, StopBits.One);

        try
        {
            // this.serial.Open();
            serial.Open();  
            serial.NewLine = "\n";
            serial.DtrEnable = true;  
            serial.RtsEnable = true;  
            serial.DiscardInBuffer();  
            serial.ReadTimeout = 20; 
        } 
        catch(Exception e)
        {
            Debug.Log ("can not open serial port");
        }
    }

    void OnDestroy()
    {
        this.serial.Close ();
    }

    // public int getdate() {
    //     return this.date;
    // }

    void Update() {
        //date = this.serial.ReadByte();
        if(this.serial.BytesToRead!=0){
            date = this.serial.ReadByte();
        }
        
        Debug.Log(date);
        // if(date == 1) {
        //     tama.Shot();
        // }
        //text.Write("a");
    }
}