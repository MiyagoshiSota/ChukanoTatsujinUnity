using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TutrialCreateCombo : MonoBehaviour
{
    public TMP_Text creaCombo;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("okTutrial",0);
        creaCombo.text = PlayerPrefs.GetInt("okTutrial").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        creaCombo.text = PlayerPrefs.GetInt("okTutrial").ToString();
    }
}