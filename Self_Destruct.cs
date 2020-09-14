using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Self_Destruct : MonoBehaviour
{
    public int damage;
    public float destructTime;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destructTime);
    }

    
}
