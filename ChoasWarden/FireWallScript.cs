using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWallScript : MonoBehaviour
{
    public float distance;
    public float time;
    float timer = 0;

    public bool goingLeft;

    float distPerStep;

    public int damage = 1;

    BoxCollider2D boxCollider;

    public float destructionTime = 6.5f;

    GameObject player;

    float timeInFire = 0;
    bool inFire = false;
    void Start()
    {
        distPerStep = distance/time;
        boxCollider = gameObject.GetComponent<BoxCollider2D>();

        StartCoroutine("movement");
        Destroy(gameObject, destructionTime);

        player = GameObject.FindGameObjectWithTag("Player");
    }

    
    void Update()
    {
        if (inFire)
        {
            timeInFire += Time.deltaTime;
            if (timeInFire > .25f)
            {
                timeInFire = 0;
                player.GetComponent<Player_Control>().AugmentHealth(-damage);
            }
        }
        else
        {
            timeInFire = 0;
        }
    }

    IEnumerator movement()
    {
        while (timer < time)
        {
            if (goingLeft)
            {
                timer += Time.deltaTime;
                transform.position = new Vector3(transform.position.x - (distPerStep * Time.deltaTime), transform.position.y, 0);
                boxCollider.size = new Vector2(boxCollider.size.x + (distPerStep * Time.deltaTime), boxCollider.size.y);
                boxCollider.offset = new Vector2((boxCollider.size.x + (distPerStep * Time.deltaTime))/2, 0);
            }
            else
            {
                timer += Time.deltaTime;
                transform.position = new Vector3(transform.position.x + (distPerStep * Time.deltaTime), transform.position.y, 0);
                boxCollider.size = new Vector2(boxCollider.size.x + (distPerStep * Time.deltaTime), boxCollider.size.y);
                boxCollider.offset = new Vector2((boxCollider.size.x - (distPerStep * Time.deltaTime)) / 2, 0);
            }
            yield return null;


        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player_Control>().AugmentHealth(-damage);
            inFire = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player_Control>().AugmentHealth(-damage);
            inFire = false;

        }
    }
}
