using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HardCreateNotes : MonoBehaviour
{
    int  condimentRedNum=1,condimentBlueNum=1,condimentGreenNum=1,wokLongNum=1,wokSingleNum=1,ladleLongNum=1,ladleSingleNum=1;
    int select = 0,selectNum = 0;
    int[] selectData = new int[]{0, 1, 6, 4, 2, 1, 6, 4, 4, 0, 2, 6, 6, 4, 4, 0, 2, 2, 6, 4, 4, 2 ,0 ,6 ,6 ,4 ,4 ,1 ,0 ,6 ,6 ,4 ,4 ,4 ,0 ,2 ,6, 4, 4, 4, 0, 0, 6, 4, 4 ,4 ,0, 2, 6, 6, 4, 4, 4, 6, 6, 6, 2, 6, 4, 0, 6, 4, 2, 0, 6, 4, 4, 4, 4, 2, 0, 6, 6, 4, 4, 0, 0, 6, 6, 4, 4, 1, 6, 4, 4, 4, 4, 0, 0, 6, 6, 4, 2, 0, 4, 4, 1, 6, 4, 1, 2, 4, 4, 4, 4, 6, 6, 6,
0, 1, 4, 4, 4, 4, 6, 6, 6, 6, 4, 4, 4, 4, 4, 6, 6, 1, 0, 4, 4, 4, 4, 1, 2, 2, 2, 6 ,6, 4, 4, 4, 4, 2, 1, 6, 6, 4, 2, 1, 6, 6, 6, 6, 4, 4, 4, 4, 1, 6, 4,
1, 2, 6, 4, 4, 4, 2, 2, 6, 6, 4, 4, 0, 1, 2, 6, 6, 6, 4, 4, 4, 4, 4, 1, 1, 6, 6, 6, 6, 4, 4, 4, 4, 2, 2, 1, 6, 4, 4, 4, 4, 4, 2, 1, 6, 6, 4, 2, 6, 4, 4, 2, 1, 2, 6, 6, 4, 4, 2, 4, 4, 4, 4
};
    // CubeプレハブをGameObject型で取得
    [SerializeField] GameObject condimentRed,condimentBlue,condimentGreen,wokLong,wokSingle,ladleLong,ladleSingle;
    // Start is called before the first frame update

    public void Onpu()//ノーツ生成
    {
        select = selectData[selectNum];
        Debug.Log(select);
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
            default:
            break;
        }
        selectNum++;
    }
    public void CreateAndRename(GameObject createObj,ref int createObjNum){
        createObj.name = createObj.name + createObjNum;
        Instantiate (createObj, new Vector3(12035,-3747,-200), Quaternion.identity);
        createObjNum++;
    }
}
