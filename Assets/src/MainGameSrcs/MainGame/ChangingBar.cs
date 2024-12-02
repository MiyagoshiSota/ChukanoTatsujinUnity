using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangingBar : MonoBehaviour
{
    [SerializeField] protected Image _gaugeImage;//ゲージとして使うImage
    int _currentHp = 0; //現在のHP
    float _maxHp = 100; //maxHp

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("currentHp",0);
        _gaugeImage.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(PlayerPrefs.GetInt("currentHp"));
        _gaugeImage.fillAmount = (float)PlayerPrefs.GetInt("currentHp") / _maxHp; //fillAmountを更新
    }
}
