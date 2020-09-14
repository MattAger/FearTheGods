using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosTurret : MonoBehaviour
{
    GameObject player;

    public float reloadTime = .5f;

    float timer = 0;

    public int health;

    public GameObject chaosShot;

    public ParticleSystem deathExplosion;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        lookAtPlayer();

        if (timer >= reloadTime)
        {
            timer = 0;
            shoot();
        }
    }

    void lookAtPlayer()
    {
        if (player.transform.position.x > transform.position.x && player.transform.position.y > transform.position.y)
        {
            float z_angle = Mathf.Atan2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
            //Debug.Log(z_angle);
            transform.eulerAngles = new Vector3(0, 0, 360 - (z_angle * Mathf.Rad2Deg));
        }
        else if (player.transform.position.x < transform.position.x && player.transform.position.y > transform.position.y)
        {
            float z_angle = Mathf.Atan2(transform.position.x - player.transform.position.x, player.transform.position.y - transform.position.y);
            //Debug.Log(z_angle);
            transform.eulerAngles = new Vector3(0, 0, (z_angle * Mathf.Rad2Deg));
        }
        else if (player.transform.position.x < transform.position.x && player.transform.position.y < transform.position.y)
        {
            float z_angle = Mathf.Atan2(transform.position.x - player.transform.position.x, transform.position.y - player.transform.position.y);
            //Debug.Log(z_angle);
            transform.eulerAngles = new Vector3(0, 0, 90 - (z_angle * Mathf.Rad2Deg) - 270);
        }
        else
        {
            float z_angle = Mathf.Atan2(player.transform.position.x - transform.position.x, transform.position.y - player.transform.position.y);
            //Debug.Log(z_angle);
            transform.eulerAngles = new Vector3(0, 0, 180 + (z_angle * Mathf.Rad2Deg));
        }
    }

    void shoot()
    {
        GameObject tempShot = Instantiate(chaosShot, transform.position, Quaternion.identity);
        lookAtPlayer(tempShot);
        tempShot.GetComponent<Rigidbody2D>().velocity = tempShot.transform.up * 200;
    }

    void lookAtPlayer(GameObject tempThrowingRock)
    {
        if (player.transform.position.x > transform.position.x && player.transform.position.y > transform.position.y)
        {
            float z_angle = Mathf.Atan2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
            //Debug.Log(z_angle);
            tempThrowingRock.transform.eulerAngles = new Vector3(0, 0, 360 - (z_angle * Mathf.Rad2Deg));
        }
        else if (player.transform.position.x < transform.position.x && player.transform.position.y > transform.position.y)
        {
            float z_angle = Mathf.Atan2(transform.position.x - player.transform.position.x, player.transform.position.y - transform.position.y);
            //Debug.Log(z_angle);
            tempThrowingRock.transform.eulerAngles = new Vector3(0, 0, (z_angle * Mathf.Rad2Deg));
        }
        else if (player.transform.position.x < transform.position.x && player.transform.position.y < transform.position.y)
        {
            float z_angle = Mathf.Atan2(transform.position.x - player.transform.position.x, transform.position.y - player.transform.position.y);
            //Debug.Log(z_angle);
            tempThrowingRock.transform.eulerAngles = new Vector3(0, 0, 90 - (z_angle * Mathf.Rad2Deg) - 270);
        }
        else
        {
            float z_angle = Mathf.Atan2(player.transform.position.x - transform.position.x, transform.position.y - player.transform.position.y);
            //Debug.Log(z_angle);
            tempThrowingRock.transform.eulerAngles = new Vector3(0, 0, 180 + (z_angle * Mathf.Rad2Deg));
        }
    }

    public void AugmentHealth(int hp)
    {
        health += hp;
        if (health <= 0)
        {
            if (deathExplosion != null)
            {
                Instantiate(deathExplosion, transform.position, Quaternion.identity).Play();
            }
            Destroy(gameObject);
        }
    }
}
