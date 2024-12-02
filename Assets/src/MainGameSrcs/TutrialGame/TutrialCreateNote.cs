using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using UniRx;
using TMPro;
using DG.Tweening;

public class TutrialCreateNote : MonoBehaviour
{
    int  condimentRedNum=1,condimentBlueNum=1,condimentGreenNum=1,wokLongNum=1,wokSingleNum=1,ladleLongNum=1,ladleSingleNum=1;
    int select = 4,nowOder = 0,count=0,roopBool = 0;
    //int[] noteOrder = new int[]{0,0,0,6,4,4,5,5,5,6,4,4,4,4,4,4,0,5,5,4,4,4,0,6,0,6,4,4,4,4,4,0,5,5,4,4,0,6,0,6,0,6,6,0,6,0,6,0,6,6,4,4,4,4,4,4,4,0,6,0,6,4,4,0,6,0,6,4,4,4,4,0,6,6,4,4,4};
    int[] noteOrder = new int[]{0,1,2,6,4,4,5,3,3,1,4,4,4,4,4,4,0,5,5,4,4,4,0,6,0,6,4,4,4,4,4,0,5,5,4,4,0,6,0,6,0,6,6,0,6,0,6,0,6,6,4,4,4,4,4,4,4,0,6,0,6,4,4,0,6,0,6,4,4,4,4,0,6,6,4,4,4};
    string difficulty;
    bool TutrialCrea = false,stopBool = false;
    // CubeプレハブをGameObject型で取得
    [SerializeField] GameObject condimentRed,condimentBlue,condimentGreen,wokLong,wokSingle,ladleLong,ladleSingle,point;
    public TMP_Text pointText;

    public GameObject targetObject;

    // Start is called before the first frame update
    private IEnumerator Start()
    {
        targetObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        difficulty = PlayerPrefs.GetString("SelectDifficulty");
        PlayerPrefs.SetInt("okTutrial",0);
        pointText.text = "hello";
        DelayAndCreate();
    }

    public void Onpu()//ノーツ生成
    {
        if(noteOrder.Length == nowOder){
        }
        else{
            select = noteOrder[nowOder];
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
        GameObject target = GameObject.Find(createObj.name + "(Clone)");
        target.AddComponent<moveObj>();
        createObjNum++;
    }

    void DelayAndCreate(){
        Onpu();

        Observable.Return(Unit.Default)
            .Delay(System.TimeSpan.FromMilliseconds(2000))
            .Subscribe(_ =>  Onpu());

        Observable.Return(Unit.Default)
            .Delay(System.TimeSpan.FromMilliseconds(4000))
            .Subscribe(_ =>  Onpu());

        Observable.Return(Unit.Default)
            .Delay(System.TimeSpan.FromMilliseconds(8000))
            .Subscribe(_ =>  Chech());
    }
     
    void Chech(){
        if(PlayerPrefs.GetInt("okTutrial") < 2){
            PlayerPrefs.SetInt("okTutrial",0);
            roopBool++;
            if(roopBool == 3){
                roopBool = 0;
                count++;
            }

            if(count == 0){
                nowOder = 0;
            }
            else if(count == 1){
                nowOder = 3;
            }
            else{
                nowOder = 6;
            }
        }
        else{
            PlayerPrefs.SetInt("okTutrial",0);
            roopBool = 0;
            count++;
        }

        if(count == 3){//3
            targetObject.SetActive(true);
            PlayerPrefs.SetInt("TutrialStopMove", 1);
            Observable.Return(Unit.Default)
                .Delay(System.TimeSpan.FromMilliseconds(2700))
                .Subscribe(_ =>  nextMode());
        }else{
            DelayAndCreate();
        }
    }
    


    private void nextMode(){
            PlayerPrefs.SetInt("okTutrial",0);

            switch (PlayerPrefs.GetString("SelectDifficulty"))
            {
                case "veryeasy":
                    SceneManager.LoadScene("VeryEasy");
                    break;
                case "easy":
                    SceneManager.LoadScene("Easy");
                    break;
                case "normal":
                    SceneManager.LoadScene("Normal");
                    break;
                case "hard":
                    SceneManager.LoadScene("Hard");
                    break;
                default:
                    break;
            }
    }

}