using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePitScript : MonoBehaviour
{
    public float timer = 10;
    float timeF = 0;
    private void Update()
    {
        timeF += Time.deltaTime;
        if (timeF > timer)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player_Control>().AugmentHealth(-1);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce((transform.position - collision.gameObject.transform.position) * -1500);
        }
    }
}
