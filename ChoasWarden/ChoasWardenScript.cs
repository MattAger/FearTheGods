using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoasWardenScript : MonoBehaviour
{
    public int health;
    public int damage;
    public float movespeed;
    public float max_speed;
    public float acceleration;
    public float[] attack_probabiity;
    GameObject player;

    public Enemy_Class self;
    int prev_health;
    SpriteRenderer main_img;

    public GameObject turret;
    public GameObject fireWall;
    public GameObject chaosBouncer;
    

    public float attack0Timer;
    public float attack1Timer;
    public float attack2Timer;

    public bool finalBoss = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        self = new Enemy_Class(health, damage, movespeed, max_speed, acceleration, attack_probabiity, player, gameObject);
        prev_health = self.health;
        main_img = gameObject.GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        self.Navigate_Random();
        self.attack_timer += Time.deltaTime;

        if (finalBoss)
        {
            self.attack_timer -= (Time.deltaTime * .66f);
        }


        if (PlayerPrefs.GetInt("ChallengeMode", 0) == 1)
        {
            self.attack_timer += (Time.deltaTime * 1.5f);
        }

        

        if (self.attack_timer > 0)
        {
            Invoke("Attack" + self.attack().ToString(), 0);
        }



        if (prev_health != self.health)
        {
            prev_health = self.health;
            StartCoroutine("RedEffect");
        }
    }

    public void Attack100()
    {

    }

    public void Attack0()
    {
        self.attack_timer -= attack0Timer;
        Instantiate(turret, new Vector3(Random.Range(-175f, 175f), Random.Range(100f, -100f), 0), Quaternion.identity);
    }

    public void Attack1()
    {
        self.attack_timer -= attack1Timer;
        if (transform.position.x > 0)
        {
            GameObject tempWall = Instantiate(fireWall, transform.position, Quaternion.identity);
            tempWall.GetComponent<FireWallScript>().goingLeft = true;
        }
        else
        {
            GameObject tempWall = Instantiate(fireWall, transform.position, Quaternion.identity);
            tempWall.GetComponent<FireWallScript>().goingLeft = false;
        }
    }

    public void Attack2()
    {
        self.attack_timer -= attack2Timer;
        GameObject tempBouncer = Instantiate(chaosBouncer, transform.position, Quaternion.identity);
        tempBouncer.GetComponent<Rigidbody2D>().velocity = Random.rotation.eulerAngles.normalized * 200;
    }

    private IEnumerator RedEffect()
    {

        main_img.color = new Color(1f, 0, 0, 1);
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;

        main_img.color = new Color(.765f, 0, 0, 1);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("testBrick"))
        {
            self.Reverse_Velocity();
        }
    }
}

