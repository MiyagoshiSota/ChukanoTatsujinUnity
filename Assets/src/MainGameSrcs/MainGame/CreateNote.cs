using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreateNote : MonoBehaviour
{
    int  condimentRedNum=1,condimentBlueNum=1,condimentGreenNum=1,wokLongNum=1,wokSingleNum=1,ladleLongNum=1,ladleSingleNum=1;
    int select = 1;
    // CubeプレハブをGameObject型で取得
    [SerializeField] GameObject condimentRed,condimentBlue,condimentGreen,wokLong,wokSingle,ladleLong,ladleSingle;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)){
            SceneManager.LoadScene("Result");
        }
    }

    public void Onpu()//ノーツ生成
    {
        switch (select)
        {
            case 0://condimentRed
                CreateAndRename(condimentRed,ref condimentRedNum);
                break;
            case 1://condimentBlue
                CreateAndRename(condimentBlue,ref condimentBlueNum);
                break;
            case 2://condimentBlue
                CreateAndRename(condimentGreen,ref condimentGreenNum);
                break;
            case 3:
                CreateAndRename(wokLong,ref wokLongNum);
                break;
            case 4:
                CreateAndRename(wokSingle,ref wokSingleNum);
                break;
            case 5:
                CreateAndRename(ladleLong,ref ladleLongNum);
                break;
            case 6:
                CreateAndRename(ladleSingle,ref ladleSingleNum);
                break;
            case 7:
                SceneManager.LoadScene("Result");
                break;
            default:
            break;
        }
        select = Random.Range(1, 6);//0～6の範囲でランダムな整数値が返る
    }
    public void CreateAndRename(GameObject createObj,ref int createObjNum){
        createObj.name = createObj.name + createObjNum;
        Instantiate (createObj, new Vector3(12035,-269,-200), Quaternion.identity);
        createObjNum++;
    }
}
