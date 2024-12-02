using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyStartMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Start" || SceneManager.GetActiveScene().name == "Select"){
            DontDestroyOnLoad(gameObject);
        }else{ 
            Destroy(gameObject);
        }
    }
}
