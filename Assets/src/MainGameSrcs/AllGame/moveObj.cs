using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moveObj : MonoBehaviour
{
     private Coroutine moveCoroutine;
    private float speed = -8000f;

    private void Start()
    {
        moveCoroutine = StartCoroutine(MoveObjectCoroutine());
    }

    private IEnumerator MoveObjectCoroutine()
    {
        while (true)
        {
            if(PlayerPrefs.GetInt("TutrialStopMove") == 1){
                OnDestroy();
            }
            // オブジェクトの移動処理
            transform.Translate(speed * Time.deltaTime, 0, 0);
            yield return null;
        }
    }

    private void OnDestroy()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
    }
}
