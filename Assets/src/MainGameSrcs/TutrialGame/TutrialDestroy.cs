using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class TutrialDestroy : MonoBehaviour
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
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name.Contains("wokLong") || collision.gameObject.name.Contains("ladleLong") || collision.gameObject.name.Contains("wok_long_end") || collision.gameObject.name.Contains("ladle_long_end")){
        }else{
            Destroy(collision.gameObject);
            missGif.SetActive (true);
            DelayAndVidivle(120,"missGif");
        }
    }

    async void DelayAndVidivle(int delayTime,string gifNameString)
    {
        await Task.Delay(delayTime);
        missGif.SetActive (false);
    }
}
