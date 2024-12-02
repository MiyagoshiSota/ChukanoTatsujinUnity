using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class One : MonoBehaviour
{
    public SpriteRenderer imageRenderer; // 画像のSpriteRendererコンポーネント
    public int targetVariableValue = 1; // 透明にするターゲットの変数値
    public string thirdSerialData;

    private int variable = 0; // 例として初期値は0とします

    private void Update()
    {
        
        // 上矢印キーを押したら変数を減少させる
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            variable = Mathf.Max(variable - 1, 0);
        }
        // 下矢印キーを押したら変数を増加させる
        else if ((Input.GetKeyDown(KeyCode.DownArrow)) && variable < 2)
        {
            variable = Mathf.Min(variable + 1, 2); // 例として最大値を3とします
        }

        // 変数の値に応じて透明度を設定
        if (variable == targetVariableValue)
        {
            // 透明度を0に設定することで完全に透明にします
            Color newColor = imageRenderer.color;
            newColor.a = 1;
            imageRenderer.color = newColor;
        }
        else
        {
            // 不透明度を元に戻す
            Color newColor = imageRenderer.color;
            newColor.a = 0;
            imageRenderer.color = newColor;
        }
    }
}
