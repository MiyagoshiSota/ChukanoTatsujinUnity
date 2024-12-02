using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveLine : MonoBehaviour
{
    Vector3 objectPosition;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
                //トランスフォームの取得
        Transform myTransform = this.transform;
 
        //座標の取得
        Vector3 pos = myTransform.position;
 
        //z方向の速度
        pos.x -= 100f;
        pos.y = -269;
 
        //座標の設定
        myTransform.position = pos;

        objectPosition = this.transform.position;

        if(objectPosition.x < -9575 ){
            objectPosition.x = 10876;
            this.transform.position = objectPosition;

        }
    }
}
