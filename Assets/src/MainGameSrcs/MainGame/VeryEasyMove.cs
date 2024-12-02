using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VeryEasyMove : MonoBehaviour
{
    private Coroutine moveCoroutine;
    private float speed = -11000f;

    private void Start()
    {
        moveCoroutine = StartCoroutine(MoveObjectCoroutine());
    }

    private IEnumerator MoveObjectCoroutine()
    {
        while (true)
        {
            if(PlayerPrefs.GetInt("StopMove") == 1){
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
