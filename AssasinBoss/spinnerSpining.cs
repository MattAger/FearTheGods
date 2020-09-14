using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinnerSpining : MonoBehaviour
{
    float timer;
    private void Update()
    {
        timer += Time.deltaTime;
        transform.eulerAngles = new Vector3(0, 0, timer * 650);
        
    }
}
