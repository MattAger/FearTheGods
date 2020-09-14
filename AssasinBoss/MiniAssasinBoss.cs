using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniAssasinBoss : MonoBehaviour
{
    public int health;
    public int damage;
    public float movespeed;
    public float max_speed;
    public float acceleration;
    public float[] attack_probabiity;
    GameObject player;

    public GameObject dagger;
    
    public Enemy_Class self;
    int prev_health;
    SpriteRenderer main_img;

    public ParticleSystem deathExplosion;

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

        lookAtPlayer();

        if (self.attack_timer > .7f)
        {
            self.attack_timer = 0;
            MainAttack();
        }
        //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);

        

        if (prev_health != self.health)
        {
            prev_health = self.health;
            StartCoroutine("RedEffect");
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

    public void augmentHp(int hp)
    {
        self.health += hp;
        if (self.health <= 0)
        {
            Instantiate(deathExplosion, transform.position, Quaternion.identity).Play();
            Destroy(gameObject);
        }
    }

    public void MainAttack()
    {
        GameObject temp_dagger = Instantiate(dagger, transform.position, Quaternion.identity);
        temp_dagger.transform.rotation = transform.rotation;
        temp_dagger.GetComponent<Rigidbody2D>().AddForce(temp_dagger.transform.up * 12000);
    }

}
