using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player_Control : MonoBehaviour
{
    public Player_Class player;
    public float speed;
    public string race;
    public int ranged_damage;
    public float ranged_projectile_speed;
    public float ranged_recharge_time;
    public int meele_damage;
    public int health;
    public float dash_power;
    public float dash_recharge_time;

    public GameObject shot;
    public GameObject critShot;
    int prev_health;
    public GameObject trail_obj;

    public ParticleSystem bloodSpurt;
    public ParticleSystem bloodSpurtBig;
    public ParticleSystem dustKickup;

    public GameObject dashBomb;

    public GameObject bloodStain;

    GameObject canvas;

    Camera Cam;

    public int max_health;
    // Start is called before the first frame update
    void Start()
    {
        //Time.timeScale = (.1f);
        canvas = GameObject.Find("Canvas");
        trail_obj.GetComponent<SpriteRenderer>().color = gameObject.GetComponent<SpriteRenderer>().color;
        player = new Player_Class(speed, race, ranged_damage, ranged_projectile_speed, ranged_recharge_time, meele_damage, health, dash_power, dash_recharge_time, gameObject);
        create_trail();
        max_health = player.max_health;
        prev_health = player.health;
        Cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        canvas.GetComponent<PlayerHealthUIController>().current_health = player.health;

        player.Player_Movement();
        if (player.dashing)
        {
            Instantiate(dustKickup, transform.position, Quaternion.identity).Play();
            int[] bombInfo = player.dashBlastCall();
            
            if (bombInfo[0] > 0)
            {
                for (int i = 0; i < bombInfo[0]; i++)
                {
                    GameObject tempBomb = Instantiate(dashBomb, transform.position, Quaternion.identity);
                    tempBomb.GetComponent<dashBombScript>().damage = bombInfo[1];
                    tempBomb.GetComponent<dashBombScript>().range = bombInfo[2];
                    tempBomb.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-2000, 2000), Random.Range(-2000, 2000)));
                }
               
            }
            
        }

        if (player.justHit)
        {
            player.justHit = false;
            Instantiate(bloodStain, new Vector3(transform.position.x, transform.position.y, 2), Quaternion.Euler(new Vector3(0, 0, Random.value * 360)));
        }

        int attackType = player.Ranged_Attack();

        if (attackType == 1)
        {
            if (((float)PlayerPrefs.GetInt("critPoints") * .05) + .05f > Random.value)
            {
                GameObject temp_shot = Instantiate(critShot, transform.position, Quaternion.identity);
                temp_shot.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + 90);
                temp_shot.GetComponent<Rigidbody2D>().velocity = temp_shot.transform.right * ranged_projectile_speed;
                Physics2D.IgnoreCollision(temp_shot.GetComponent<BoxCollider2D>(), gameObject.GetComponent<PolygonCollider2D>());
                temp_shot.GetComponent<Self_Destruct>().damage = player.ranged_damage * 5;
            }
            else
            {
                GameObject temp_shot = Instantiate(shot, transform.position, Quaternion.identity);
                temp_shot.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + 90);
                temp_shot.GetComponent<Rigidbody2D>().velocity = temp_shot.transform.right * ranged_projectile_speed;
                Physics2D.IgnoreCollision(temp_shot.GetComponent<BoxCollider2D>(), gameObject.GetComponent<PolygonCollider2D>());
                temp_shot.GetComponent<Self_Destruct>().damage = player.ranged_damage;
            }
            

        }

        //This One is Dual Cannon
        else if (attackType == 2)
        {
            if (((float)PlayerPrefs.GetInt("critPoints") * .05) + .05f > Random.value)
            {
                GameObject temp_shot = Instantiate(critShot, transform.position, Quaternion.identity);
                temp_shot.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + 90);
                temp_shot.transform.position = temp_shot.transform.position + temp_shot.transform.up * 5;
                temp_shot.GetComponent<Rigidbody2D>().velocity = temp_shot.transform.right * ranged_projectile_speed;
                Physics2D.IgnoreCollision(temp_shot.GetComponent<BoxCollider2D>(), gameObject.GetComponent<PolygonCollider2D>());
                temp_shot.GetComponent<Self_Destruct>().damage = player.ranged_damage * 5;

                temp_shot = Instantiate(critShot, transform.position, Quaternion.identity);
                temp_shot.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + 90);
                temp_shot.transform.position = temp_shot.transform.position + (temp_shot.transform.up * -5);
                temp_shot.GetComponent<Rigidbody2D>().velocity = temp_shot.transform.right * ranged_projectile_speed;
                Physics2D.IgnoreCollision(temp_shot.GetComponent<BoxCollider2D>(), gameObject.GetComponent<PolygonCollider2D>());
                temp_shot.GetComponent<Self_Destruct>().damage = player.ranged_damage * 5;
            }
            else
            {
                GameObject temp_shot = Instantiate(shot, transform.position, Quaternion.identity);
                temp_shot.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + 90);
                temp_shot.transform.position = temp_shot.transform.position + temp_shot.transform.up * 5;
                temp_shot.GetComponent<Rigidbody2D>().velocity = temp_shot.transform.right * ranged_projectile_speed;
                Physics2D.IgnoreCollision(temp_shot.GetComponent<BoxCollider2D>(), gameObject.GetComponent<PolygonCollider2D>());
                temp_shot.GetComponent<Self_Destruct>().damage = player.ranged_damage;

                temp_shot = Instantiate(shot, transform.position, Quaternion.identity);
                temp_shot.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + 90);
                temp_shot.transform.position = temp_shot.transform.position + (temp_shot.transform.up * -5);
                temp_shot.GetComponent<Rigidbody2D>().velocity = temp_shot.transform.right * ranged_projectile_speed;
                Physics2D.IgnoreCollision(temp_shot.GetComponent<BoxCollider2D>(), gameObject.GetComponent<PolygonCollider2D>());
                temp_shot.GetComponent<Self_Destruct>().damage = player.ranged_damage;
            }
        }

        if (prev_health != player.health)
        {
            StartCoroutine("camShake");
            Instantiate(bloodSpurt, transform.position, Quaternion.identity).Play();
            prev_health = player.health;
        }
    }

    public void create_trail()
    {
        Instantiate(trail_obj, transform.position, Quaternion.identity);
        Invoke("create_trail", .02f);
    }

    public void AugmentHealth(int hp)
    {
        if (!player.AugmentHealth(hp))
        {
            LoadDeathScene();
            //Instantiate(bloodSpurtBig, transform.position, Quaternion.identity);
            //gameObject.SetActive(false);
            
            //gameObject.GetComponent<SceneManagerScript>().LoadMainMenuFromArena();
            //Destroy(gameObject);
        }
    }

    IEnumerator camShake()
    {
        Vector3 orig_pos = new Vector3(0, 0, -10);
        for (int i = 0; i < 30; i++)
        {
            Cam.transform.position = new Vector3(Cam.transform.position.x + Random.Range(-2f, 2f), Cam.transform.position.y + Random.Range(-2f, 2f), Cam.transform.position.z);
            yield return null;
        }
        Cam.transform.position = orig_pos;
        yield return null;
    }

    public void LoadDeathScene()
    {
        gameObject.GetComponent<SceneManagerScript>().LoadDeathScene();
    }

}
