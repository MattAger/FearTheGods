using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowOrbScaleRegulator : MonoBehaviour
{
    float maxtimeAlive;
    float timeAlive;
    float maxScale;

    // Start is called before the first frame update
    void Start()
    {
        maxtimeAlive = gameObject.GetComponent<Self_Destruct>().destructTime;
        maxScale = gameObject.transform.localScale.x;
        
    }

    // Update is called once per frame
    void Update()
    {
        timeAlive += Time.deltaTime;
        float scale = (maxtimeAlive - timeAlive) / maxtimeAlive;
        transform.localScale = new Vector3((scale * maxScale) +4, (scale * maxScale) + 4, 1);
        
    }
}
