using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMaxScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
      PlayerPrefs.GetInt("result");

      if(PlayerPrefs.GetInt("result") > PlayerPrefs.GetInt("maxScore")){
        PlayerPrefs.SetInt("maxScore", PlayerPrefs.GetInt("result"));
      }  
    }
}
