using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayFinish : MonoBehaviour
{
 
	[SerializeField]
	private int minute;
	[SerializeField]
	private float seconds;
    public AudioSource m_MyAudioSource;
	//　前のUpdateの時の秒数
	private float oldSeconds;

	public GameObject targetObject;
	public GameObject timebar;
 
	void Start () {
		minute = 0;
		seconds = 0f;
		oldSeconds = 0f;
	}
 
	void Update () {
		seconds += Time.deltaTime;
		if(seconds >= 60f) {
			minute++;
			seconds = seconds - 60;
		}
		//　値が変わった時だけテキストUIを更新
		if((int)seconds != (int)oldSeconds) {
            //Debug.Log("minute: " + minute + " seconds: " + (int)seconds);
		}
		CheckFinishTime();
		oldSeconds = seconds;
	}

    void CheckFinishTime(){
        if(minute == 1 && ( ((int)seconds == 52) || ((int)seconds == 53)) ){
			PlayerPrefs.SetInt("StopMove", 1);
			targetObject.SetActive(true); 
            m_MyAudioSource.volume = m_MyAudioSource.volume - 0.008f;
        }else if(minute == 1 && seconds > 54.7){
            SceneManager.LoadScene("Result");
        }
    }
}
