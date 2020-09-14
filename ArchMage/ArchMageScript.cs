using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchMageScript : MonoBehaviour
{
    public int health;
    public int damage;
    public float movespeed;
    public float max_speed;
    public float acceleration;
    public float[] attack_probabiity;
    GameObject player;

    Rigidbody2D rb;

    public Enemy_Class self;
    int prev_health;
    SpriteRenderer main_img;

    float teleportTime = -3;

    public float attack0Timer;
    public float attack1Timer;
    public float attack2Timer;
    public float attack3Timer;

    public ParticleSystem selfMagicPuff;
    public ParticleSystem invisPoof;
    public GameObject mageShot;
    public GameObject followOrb;
    public GameObject firePit;

    public SpriteRenderer[] selfSprites;
    public ParticleSystem[] selfParticles;

    public bool finalBoss = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        self = new Enemy_Class(health, damage, movespeed, max_speed, acceleration, attack_probabiity, player, gameObject);
        prev_health = self.health;
        main_img = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
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
            self.attack_timer += Time.deltaTime;
        }

        float dist = Vector2.Distance(self.player.transform.position, transform.position);
        dist = 100 - dist;
        if (dist < 0)
        {
            dist = 0;
        }
        dist = dist / 6;

        rb.AddForce((self.player.transform.position - transform.position) * dist * -3f);

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

        teleportTime += Time.deltaTime;
        if (teleportTime > 0)
        {
            StartCoroutine("teleport");
            teleportTime = -3;
        }
    }

    public void Attack100()
    {

    }

    public void Attack0()
    {
        self.attack_timer -= attack0Timer;

        GameObject tempShot = Instantiate(mageShot, transform.position, Quaternion.identity);
        lookAtPlayer(tempShot);
        tempShot.GetComponent<Rigidbody2D>().AddForce(tempShot.transform.up * 20000);
        tempShot.transform.eulerAngles = new Vector3(0, 0, tempShot.transform.eulerAngles.z - 180);
    }

    public void Attack1()
    {
        self.attack_timer -= attack1Timer;
        GameObject tempShot = Instantiate(followOrb, new Vector3(transform.position.x + 60, transform.position.y, 0), Quaternion.identity);
        //tempShot.transform.parent = gameObject.transform;
        //tempShot.transform.localScale = new Vector3(2, 2, 2);


    }

    public void Attack2()
    {
        if (selfSprites[0].color.a > .8f)
        {
            self.attack_timer -= attack2Timer;
            Instantiate(invisPoof, transform.position, Quaternion.identity);
            StartCoroutine("invisiblility");
        }
    }

    public void Attack3()
    {
        self.attack_timer -= attack3Timer;
        Instantiate(firePit, new Vector3(Random.Range(-175f, 175f), Random.Range(100f, -100f), 0), Quaternion.identity); 
    }


    public void FreeAttack0()
    {

    }

    IEnumerator teleport()
    {
        Instantiate(selfMagicPuff, transform.position, Quaternion.identity).Play();

        float timer = 0;
        while (timer < .5f)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        transform.position = new Vector3(Random.Range(-175f, 175f), Random.Range(100f, -100f), 0);
        yield return null;
        
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

    private IEnumerator RedEffect()
    {

        main_img.color = new Color(.78f, 0, .7f, main_img.color.a);
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;

       
        main_img.color = new Color(.48f, 0, .6f, main_img.color.a);

    }

    IEnumerator invisiblility()
    {
        foreach (SpriteRenderer spR in selfSprites)
        {
            spR.color = new Color(spR.color.r, spR.color.g, spR.color.b, .03f);
        }
        foreach (ParticleSystem myParticle in selfParticles)
        {
            myParticle.gameObject.transform.position = new Vector3(1000, 1000, 1);
        }


        float timerTemp = 0;
        while (timerTemp < 4)
        {
            timerTemp += Time.deltaTime;
            yield return null;
        }

        foreach (SpriteRenderer spR in selfSprites)
        {
            spR.color = new Color(spR.color.r, spR.color.g, spR.color.b, 1);
        }

        bool xxix = false;
        foreach (ParticleSystem myParticle in selfParticles)
        {
            if (xxix)
            {
                myParticle.gameObject.transform.localPosition = new Vector3(6.5f, -.5f, 0);
            }
            else
            {
                myParticle.gameObject.transform.localPosition = new Vector3(-6.5f, -.5f, 0);
            }
            xxix = true;
        }
    }
}
