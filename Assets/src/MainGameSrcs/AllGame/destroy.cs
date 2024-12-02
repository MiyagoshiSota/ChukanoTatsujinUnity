using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class destroy : MonoBehaviour
{
    [SerializeField] GameObject missGif;
    int missCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(PlayerPrefs.GetInt("combo"));
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name.Contains("wokLong") || collision.gameObject.name.Contains("ladleLong") || collision.gameObject.name.Contains("wok_long_end") || collision.gameObject.name.Contains("ladle_long_end")){
        }else{
            missCount++;
            PlayerPrefs.SetInt("miss",missCount);
            PlayerPrefs.SetInt("combo", 0);
            Destroy(collision.gameObject);
            missGif.SetActive (true);
            DelayAndVidivle(120,"missGif");
            SetBar();
        }
    }
    void SetBar(){
        if(PlayerPrefs.GetInt("currentHp") - 10 < 0){
            PlayerPrefs.SetInt("currentHp",0);
        }else{
            PlayerPrefs.SetInt("currentHp",PlayerPrefs.GetInt("currentHp") - 10);
        }
    }

    async void DelayAndVidivle(int delayTime,string gifNameString)
    {
        await Task.Delay(delayTime);
        missGif.SetActive (false);
    }
}
