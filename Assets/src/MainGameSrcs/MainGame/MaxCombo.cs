using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MaxCombo : MonoBehaviour
{
    public TMP_Text maxCombo;
    // Start is called before the first frame update
    void Start()
    {
        maxCombo.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt("combo") == 0){
            maxCombo.text = "";
        }else{
            maxCombo.text = PlayerPrefs.GetInt("combo").ToString();
        }
    }
}
