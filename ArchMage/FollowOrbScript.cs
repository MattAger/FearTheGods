using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowOrbScript : MonoBehaviour
{
    GameObject boss;

    bool tracking = false;

    Rigidbody2D rb;

    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        boss = GameObject.FindGameObjectWithTag("Player");
        rb.velocity = Vector3.up * 150;
    }

    // Update is called once per frame
    void Update()
    {
        if (!tracking)
        {
            rb.AddForce((boss.transform.position - transform.position).normalized * 140);
        }
    }
}
