using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllPrafsDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftControl)){
            PlayerPrefs.DeleteAll();
        }
    }
}
