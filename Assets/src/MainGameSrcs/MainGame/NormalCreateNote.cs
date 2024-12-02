using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NormalCreateNote : MonoBehaviour
{
    int  condimentRedNum=1,condimentBlueNum=1,condimentGreenNum=1,wokLongNum=1,wokSingleNum=1,ladleLongNum=1,ladleSingleNum=1;
    int select = 4,nowOder = 0;
    int[] noteOrder = new int[]{0,6,0,6,6,4,4,4,0,6,0,6,4,4,4,4,4,4,0,5,3,4,4,4,0,6,0,6,4,4,4,4,4,0,5,3,4,4,0,6,0,6,0,6,6,0,6,0,6,0,6,6,4,4,4,4,4,4,4,0,6,0,6,4,4,0,6,0,6,4,4,4,4,0,6,6,4,4,4};
    string difficulty;
    // CubeプレハブをGameObject型で取得
    [SerializeField] GameObject condimentRed,condimentBlue,condimentGreen,wokLong,wokSingle,ladleLong,ladleSingle,point;
    // Start is called before the first frame update
    void Start()
    {
        difficulty = PlayerPrefs.GetString("SelectDifficulty");
    }

    public void Onpu()//ノーツ生成
    {
        if(noteOrder.Length == nowOder){
        }
        else{
            if(noteOrder[nowOder] == 0){
                select = Random.Range(0,3);
            }
            else{
                select = noteOrder[nowOder];
            }
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
            nowOder++;
        }
    }
    public void CreateAndRename(GameObject createObj,ref int createObjNum){
        createObj.name = createObj.name + createObjNum;
        Instantiate (createObj, new Vector3(12035,-3747,-200), Quaternion.identity);
        createObjNum++;
    }
    static async void DelaySample(int waitTime)
    {
        await Task.Delay(waitTime);
    }
}
