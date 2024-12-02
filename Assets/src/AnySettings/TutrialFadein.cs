using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutrialFadein : MonoBehaviour
{
    public GameObject targetObject;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2f); // Wait for 2.8 seconds.
        targetObject.SetActive(false); // Deactivate the target object.
    }
}
