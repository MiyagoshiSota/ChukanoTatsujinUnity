using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class result : MonoBehaviour
{
    public string thirdSerialDate;
    private bool firstBool = false;
    public GameObject targetObject;
    
    // Start is called before the first frame update
    void Start()
    {
        targetObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        thirdSerialDate = PlayerPrefs.GetString("ButtonData");
        if (Input.GetKeyDown(KeyCode.Q) || thirdSerialDate=="1"||thirdSerialDate=="2"||thirdSerialDate=="3") {
            StartCoroutine ("next");
        }
    }

    private IEnumerator next()
    {
        targetObject.SetActive(true);
        yield return new WaitForSeconds(2.8f); // Wait for 2.8 seconds.
        SceneManager.LoadScene("Start");
    }
}
