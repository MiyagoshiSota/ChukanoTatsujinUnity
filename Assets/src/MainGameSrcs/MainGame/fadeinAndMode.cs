using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeinAndMode : MonoBehaviour
{
    public GameObject targetObject,timeline,audio,Difficulity;

    void Awake(){
        timeline.SetActive(false);
        audio.SetActive(false);
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2f); // Wait for 2.8 seconds.
        targetObject.SetActive(false); // Deactivate the target object.
        timeline.SetActive(true);
        audio.SetActive(true);
    }
}
