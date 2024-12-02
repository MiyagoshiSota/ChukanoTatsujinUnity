using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class total: MonoBehaviour
{
    public TMP_Text variableText;
    public int myVariable;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        myVariable = 10000;
        // TextMeshProUGUIに変数の値を表示
        variableText.text = myVariable.ToString();

        // 変数の値をPlayerPrefsに保存
        PlayerPrefs.SetInt("total",myVariable);
        PlayerPrefs.GetInt("total");
    }
}
