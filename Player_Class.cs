using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Class
{
    public float speed;
    public string race;
    public int ranged_damage;
    public float ranged_projectile_speed;
    public float ranged_recharge_time;
    public int meele_damage;
    public int health;
    
    public float dash_power;
    public float dash_recharge_time;

    public int max_health;

    public bool dash_shield = false;
    public float dash_shield_timer = 0;
    float dash_shield_time = .2f;

    int dashBombsNum = 0;
    float dashBombsChance = 0;
    int dashBombsDamage = 0;
    int dashBombsRange = 0;


    float dualShotChance = 0;

    public Rigidbody2D self_rb;
    public GameObject self;

    float movement_timer;
    float ranged_timer;

    public bool allowedToShoot = true;

    public bool justHit = false;

    Text player_hp_text;

    public bool dashing;

    public Player_Class(float speed_, string race_, int ranged_damage_, float ranged_projectile_speed_, float ranged_recharge_time_, int meele_damage_, int health_, float dash_power_, float dash_recharge_time_, GameObject self_)
    {
        speed = speed_;
        race = race_;
        ranged_damage = ranged_damage_ + PlayerPrefs.GetInt("damagePoints", 0);
        ranged_projectile_speed = ranged_projectile_speed_;
        ranged_recharge_time = ranged_recharge_time_ - (.08f * ranged_recharge_time_ * PlayerPrefs.GetInt("rateOfFirePoints", 0));
        if (ranged_recharge_time < .05f)
        {
            ranged_recharge_time = .05f;
        }
        meele_damage = meele_damage_;
        health = health_ + PlayerPrefs.GetInt("healthPoints", 0);
        if (health > 10)
        {
            health = 10;
        }
        max_health = health;

        dashBombsNum = PlayerPrefs.GetInt("dashBlastPoints", 0);
        
        dashBombsChance = PlayerPrefs.GetInt("dashBlastPoints", 0) * .13f;
        dashBombsDamage = PlayerPrefs.GetInt("dashBlastPoints", 0) * 5;
        dashBombsDamage = 5;
        dashBombsRange = PlayerPrefs.GetInt("dashBlastPoints", 0) * 10 + 20;
        dashBombsRange = 35;


        dualShotChance = PlayerPrefs.GetInt("dualShotPoints", 0) * .25f;


        dash_power = dash_power_ + (.2f * dash_power_ * PlayerPrefs.GetInt("dashPoints", 0));
        dash_recharge_time = dash_recharge_time_ - (.1f * dash_recharge_time_ * PlayerPrefs.GetInt("dashPoints", 0));
        dash_shield_time = dash_shield_time + (.1f * dash_recharge_time_ * PlayerPrefs.GetInt("dashPoints", 0));

        self = self_;
        self_rb = self.GetComponent<Rigidbody2D>();
        max_health = health;
        //player_hp_text = GameObject.Find("PlayerHP").GetComponent<Text>();
        //player_hp_text.text = health.ToString();
    }

    public void Player_Movement()
    {
        movement_timer += Time.deltaTime;
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(self.transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        self.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

        dashing = false;

        if (Input.GetKeyDown(KeyCode.LeftShift) && movement_timer > dash_recharge_time)
        {
            dashing = true;
            movement_timer = 0;
        }
        
        Vector2 movement_vals = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.A))
        {
            movement_vals = new Vector2(movement_vals.x - 1, movement_vals.y);
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement_vals = new Vector2(movement_vals.x + 1, movement_vals.y);
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement_vals = new Vector2(movement_vals.x, movement_vals.y - 1);
        }
        if (Input.GetKey(KeyCode.W))
        {
            movement_vals = new Vector2(movement_vals.x, movement_vals.y + 1);
        }

        if (movement_vals.x > .9 && movement_vals.y > .9)
        {
            movement_vals.x = .707f;
            movement_vals.y = .707f;
        }


        self_rb.AddForce(new Vector3(movement_vals.x, movement_vals.y, 0) * speed * Time.deltaTime);
        if (dashing == true)
        {
            dash_shield = true;
            
            self_rb.AddForce(new Vector3(movement_vals.x, movement_vals.y, 0) * speed * dash_power);
        }
        if (dash_shield == true)
        {
            dash_shield_timer += Time.deltaTime;
            if (dash_shield_timer > dash_shield_time)
            {
                dash_shield = false;
                dash_shield_timer = 0;
            }
            
        }
    }


    public int Ranged_Attack()
    {
        ranged_timer += Time.deltaTime;
        if (ranged_timer > ranged_recharge_time && allowedToShoot)
        {
            
            ranged_timer = 0;
            if (dualShotChance >= Random.value)
            {
                return 2;
            }
            else
            {
                return 1;
            }
            
        }
        return -1;
    }

    public bool AugmentHealth(int hp)
    {
        if (!dash_shield)
        {
            health += hp;
            justHit = true;
            if (health > max_health)
            {
                health = max_health;
            }

            //player_hp_text.text = health.ToString();

            if (health <= 0)
            {
                return false;
            }
            return true;
        }
        return true;
    }

    public int[] dashBlastCall()
    {
        int[] bombInfo = new int[3];
        if (allowedToShoot == false)
        {
            bombInfo[0] = -1;
            return bombInfo;
        }
        
        if (dashBombsChance > Random.value)
        {
            bombInfo = new int[3] { dashBombsNum, dashBombsDamage, dashBombsRange };
            return bombInfo;
        }
       

        bombInfo[0] = -1;
        return bombInfo;
    }
    
}
