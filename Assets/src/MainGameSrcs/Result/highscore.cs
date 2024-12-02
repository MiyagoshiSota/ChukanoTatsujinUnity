using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class highscore: MonoBehaviour
{
    public TMP_Text variableText;
    public int myVariable;

    public string value;

    // Start is called before the first frame update
    void Start()
    {
        value = (PlayerPrefs.GetInt("maxScore")/10).ToString();
        Debug.Log(value);
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("maxScore",(int.Parse(value)*10));
    }

    // Update is called once per frame
    void Update()
    {
        // TextMeshProUGUIに変数の値を表示
        variableText.text = value;
    }
}
