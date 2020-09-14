using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHeathUIController : MonoBehaviour
{
    public GameObject BossHealthBar;
    public Enemy_Class boss_self;
    public float max_health;

    float current_health;
    float prev_current_health;

    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        BossHealthBar = GameObject.Find("BossHealthJuice");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (i <= 0)
        {
            boss_self = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossHealth>().self;
            max_health = boss_self.health;
        }

        current_health = boss_self.health;
        if (i <= 0)
        {
            prev_current_health = current_health; 
        }

        if (System.Math.Abs(prev_current_health - current_health) < .01f)
        {
            AdjustBossHealthBar(current_health);
            prev_current_health = current_health;
        }

        if (i < 10) { i++; }
    }

    public void AdjustBossHealthBar(float hp)
    {
        BossHealthBar.GetComponent<RectTransform>().localScale = new Vector3(hp/max_health, 1, 1);
    }
}
