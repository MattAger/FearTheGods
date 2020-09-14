using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailDesolver : MonoBehaviour
{
    SpriteRenderer spR;
    // Start is called before the first frame update
    void Start()
    {
        spR = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        spR.color = new Color(spR.color.r, spR.color.g, spR.color.b, spR.color.a - 1.5f * Time.deltaTime);
    }
}
