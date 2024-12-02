using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSE : MonoBehaviour
{
    //SE用
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip single;

    //センサー用 
    public string firstSerialData = "5",secondSerialData = "5",thirdSerialDate;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        firstSerialData = PlayerPrefs.GetString("FryingpanData");
        secondSerialData = PlayerPrefs.GetString("LadleData");
        thirdSerialDate = PlayerPrefs.GetString("ButtonData");
        if(firstSerialData.Contains("1")|| Input.GetKeyDown(KeyCode.E)){
            audioSource.PlayOneShot(single);
        }
        
    }
}
