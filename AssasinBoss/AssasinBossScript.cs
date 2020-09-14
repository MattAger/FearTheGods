using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssasinBossScript : MonoBehaviour
{
    public int health;
    public int damage;
    public float movespeed;
    public float max_speed;
    public float acceleration;
    public float[] attack_probabiity;
    GameObject player;

    public GameObject dagger;

    public float attack0Timer;
    public float attack1Timer;
    public float attack2Timer;
    public float attack3Timer;

    public GameObject miniAssasin;

    
    public Enemy_Class self;
    int prev_health;
    SpriteRenderer main_img;

    public bool finalBoss = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        self = new Enemy_Class(health, damage, movespeed, max_speed, acceleration, attack_probabiity, player, gameObject);
        prev_health = self.health;
        main_img = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        self.Navigate_Track();
        self.attack_timer += Time.deltaTime;

        if (finalBoss)
        {
            self.attack_timer -= (Time.deltaTime * .66f);
        }

        if (PlayerPrefs.GetInt("ChallengeMode", 0) == 1)
        {
            self.attack_timer += (Time.deltaTime * 2);
        }

        lookAtPlayer();
        
        //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);

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
        //Debug.Log("NoAttack");
    }


    public void Attack0()
    {
        self.attack_timer -= attack0Timer;
        for (int i = 0; i < 3; i++)
        {
            GameObject temp_dagger = Instantiate(dagger, transform.position, Quaternion.identity);
            temp_dagger.transform.rotation = transform.rotation;
            temp_dagger.transform.Rotate(0, 0, (i * 15) - 15);
            temp_dagger.GetComponent<Rigidbody2D>().AddForce(10000 * temp_dagger.transform.up);
        }
        
    }

    public void Attack1()
    {
        self.attack_timer -= attack1Timer;

        StartCoroutine("Attack1_Coro");
        
    }

    public void Attack2()
    {
        self.attack_timer -= attack2Timer;
        gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 17000);
    }

    public void Attack3()
    {
        if (!finalBoss)
        {
            self.attack_timer -= attack3Timer;

            Instantiate(miniAssasin, transform.position, Quaternion.identity);
            Instantiate(miniAssasin, transform.position, Quaternion.identity);
        }
        
    }

    IEnumerator Attack1_Coro()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject temp_dagger = Instantiate(dagger, transform.position, Quaternion.identity);
            temp_dagger.transform.Rotate(0, 0, i * 18);
            temp_dagger.GetComponent<Rigidbody2D>().AddForce(10000 * temp_dagger.transform.up);

            GameObject temp_dagger2 = Instantiate(dagger, transform.position, Quaternion.identity);
            temp_dagger2.transform.Rotate(0, 0, 180 + (i * 18));
            temp_dagger2.GetComponent<Rigidbody2D>().AddForce(10000 * temp_dagger2.transform.up);
            yield return null;
            yield return null;
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

    private IEnumerator RedEffect()
    {

        main_img.color = new Color(.8f, 0, 0, 1);
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;

        main_img.color = new Color(.42f, 0, 0, 1);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player_Control>().AugmentHealth(-1);
        }
    }


}


