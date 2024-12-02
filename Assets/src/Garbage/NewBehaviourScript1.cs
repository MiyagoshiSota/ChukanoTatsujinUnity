using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript1 : MonoBehaviour
{
    [SerializeField] protected Image _gaugeImage;//ゲージとして使うImage
    float _currentHp = 0; //現在のHP
    float _maxHp = 100; //maxHp

    [SerializeField]
	private int minute;
	[SerializeField]
	private float seconds;
	//　前のUpdateの時の秒数
	private float oldSeconds;

    // Start is called before the first frame update
    void Start()
    {
        _gaugeImage.fillAmount = 0; //fillAmountを更新
        minute = 0;
		seconds = 0f;
		oldSeconds = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        seconds += Time.deltaTime;
        _gaugeImage.fillAmount = (float)(seconds / 114.0);
    }
}
