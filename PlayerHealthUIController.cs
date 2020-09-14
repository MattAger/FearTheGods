using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUIController : MonoBehaviour
{
    int max_health;
    GameObject player;
    
    public int current_health;

    int prev_current_health;

    public List<GameObject> hearts = new List<GameObject>();

    public Image fullHeart;
    public Image emptyHeart;

    int xix = 0;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        /*max_health = player.GetComponent<Player_Control>().max_health;
        Debug.Log(max_health);
        current_health = max_health;
        prev_current_health = current_health;

        for (int i = 0; i < hearts.Count; i++)
        {
            if (current_health > i)
            {
                hearts[i].GetComponent<Image>().sprite = fullHeart.sprite;
            }
            else if (max_health > i)
            {
                hearts[i].GetComponent<Image>().sprite = emptyHeart.sprite;
            }
            else
            {
                hearts[i].GetComponent<Image>().color = new Color(0, 0, 0, 0);
            }
        }*/
    }

   
    void Update()
    {
        if (xix == 0)
        {
            max_health = 3 + PlayerPrefs.GetInt("healthPoints", 0);
            //Debug.Log(max_health);
            current_health = max_health;
            prev_current_health = current_health;

            for (int i = 0; i < hearts.Count; i++)
            {
                if (current_health > i)
                {
                    hearts[i].GetComponent<Image>().sprite = fullHeart.sprite;
                }
                else if (max_health > i)
                {
                    hearts[i].GetComponent<Image>().sprite = emptyHeart.sprite;
                }
                else
                {
                    hearts[i].GetComponent<Image>().color = new Color(0, 0, 0, 0);
                }
            }
        }

        //current_health = player.GetComponent<Player_Control>().player.health;
        
        if (current_health != prev_current_health)
        {
            //Debug.Log(current_health);
            prev_current_health = current_health;
            for (int i = 0; i < hearts.Count; i++)
            {
                if (current_health > i)
                {
                    hearts[i].GetComponent<Image>().sprite = fullHeart.sprite;
                }
                else if (max_health > i)
                {
                    hearts[i].GetComponent<Image>().sprite = emptyHeart.sprite;
                }
                else
                {
                    hearts[i].GetComponent<Image>().color = new Color(0, 0, 0, 0);
                }
            }
            
        }

        xix++;

        
    }
}
