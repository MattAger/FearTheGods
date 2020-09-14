using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Class
{
    public int health;
    public int damage;
    public float movespeed;
    public float max_speed;
    public float acceleration;
    public float[] attack_probabiity;
    public GameObject player;

    GameObject self;

    public float attack_timer = 0;

    float x_vel = 0;
    float y_vel = 0;
    public Rigidbody2D self_rb;

    int max_health;

    float movementTimer = 0;

    public Enemy_Class(int health_, int damage_, float movespeed_, float max_speed_, float acceleration_, float[] attack_probability_, GameObject player_, GameObject self_)
    {
        health = health_;
        max_health = health;
        damage = damage_;
        movespeed = movespeed_;
        max_speed = max_speed_;
        acceleration = acceleration_;
        attack_probabiity = attack_probability_;
        player = player_;
        self = self_;
        self_rb = self.GetComponent<Rigidbody2D>();
    }

    public void Navigate_Avoid()
    {
        Vector3 dir = (self.transform.position - player.transform.position);
        if (Mathf.Abs(self_rb.velocity.magnitude) < max_speed)
        {
            self_rb.AddForce(dir * acceleration * Time.deltaTime / 1000);
        }
        else
        {
            self_rb.AddForce(self_rb.velocity.normalized * -acceleration * Time.deltaTime / 1000);
        }
    }

    public void Navigate_Track()
    {
        Vector3 dir = (self.transform.position - player.transform.position);
        if (Mathf.Abs(self_rb.velocity.magnitude) < max_speed)
        {
            self_rb.AddForce(dir * -acceleration * Time.deltaTime / 1000);
        }
        else
        {
            self_rb.AddForce(self_rb.velocity.normalized * acceleration * Time.deltaTime / 1000);
        }
    }

    public void Navigate_Random()
    {
        //Mathf.PerlinNoise(Time.deltaTime, Time.deltaTime);

        x_vel += Random.Range(-acceleration, acceleration) * Time.deltaTime;
        y_vel += Random.Range(-acceleration, acceleration) * Time.deltaTime;

        movementTimer += Time.deltaTime;

        self_rb.AddForce(new Vector2(Random.Range(-acceleration, acceleration) * Time.deltaTime, Random.Range(-acceleration, acceleration) * Time.deltaTime));
    }

    public void Reverse_Velocity()
    {
        self_rb.velocity = self_rb.velocity * -3;
    }

    public bool AugmentHealth(int hp)
    { 
        health += hp;

        //Debug.Log(health);
        GameObject.Find("BossHeathJuice").GetComponent<RectTransform>().localScale = new Vector3((float)health/max_health, 1, 1);
        return alive();
    }

    public bool alive()
    {
        if (health <= 0)
        {
            return false;
        }
        return true;
    }

    public int attack()
    {
        
        if (attack_timer > 1)
        {
            float rand_percent = Random.value;
            for (int i = 0; i < attack_probabiity.Length; i++)
            {
                if (rand_percent >= attack_probabiity[i] && rand_percent <= attack_probabiity[i+1])
                {
                    return i;
                }
                //Debug.Log(rand_percent);
            }
        }
        else { return 100; }
        return 100;
    }
}

