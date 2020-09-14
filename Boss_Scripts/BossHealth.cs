using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public Enemy_Class self;
    public GameObject player;
    public ParticleSystem deathExplosion;

    int bossNum;

    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        if (gameObject.name == "TrainingBoss")
        {
            self = gameObject.GetComponent<TrainingBossScript>().self;
            bossNum = 0;
        }
        else if (gameObject.name == "AssasinGod")
        {
            self = gameObject.GetComponent<AssasinBossScript>().self;
            bossNum = 1;
        }
        else if (gameObject.name == "EarthTitan")
        {
            self = gameObject.GetComponent<EarthTitanScript>().self;
            bossNum = 2;
        }
        else if (gameObject.name == "GraveKeeper")
        {
            self = gameObject.GetComponent<GraveKeeperScript>().self;
            bossNum = 3;
        }
        else if (gameObject.name == "ArchMage")
        {
            self = gameObject.GetComponent<ArchMageScript>().self;
            bossNum = 4;
        }
        else if (gameObject.name == "TwinKnightsHealth")
        {
            self = gameObject.GetComponent<TwinKnightsBossHealth>().self;
            bossNum = 5;
        }
        else if (gameObject.name == "ChaosWarden")
        {
            self = gameObject.GetComponent<ChoasWardenScript>().self;
            bossNum = 6;
        }
        else if (gameObject.name == "God")
        {
            self = gameObject.GetComponent<GodScript>().self;
            bossNum = 7;
        }
    }

    public void AugmentHealth(int health)
    {
        
        if (bossNum == 0 && !gameObject.GetComponent<TrainingBossScript>().self.AugmentHealth(health))
        {
            if (PlayerPrefs.GetInt("HighestLevelBeat", 0) < 1)
            {
                PlayerPrefs.SetInt("HighestLevelBeat", 1);
                PlayerPrefs.SetInt("avaliblePoints", PlayerPrefs.GetInt("avaliblePoints", 0) + 3);
            }

            if (player.GetComponent<Player_Control>().player.health == player.GetComponent<Player_Control>().player.max_health)
            {
                PlayerPrefs.SetInt("PerfectTutorial", 1);
            }
            if (PlayerPrefs.GetInt("ChallengeMode", 0) == 1)
            {
                PlayerPrefs.SetInt("ChallengeCompleteBoss0", 1);
            }
            
            Instantiate(deathExplosion, transform.position, Quaternion.identity).Play();
            player.GetComponent<SceneManagerScript>().LoadVictoryScene(5);
            Destroy(gameObject);
        }
        else if (bossNum == 1 && !gameObject.GetComponent<AssasinBossScript>().self.AugmentHealth(health))
        {
            if (PlayerPrefs.GetInt("HighestLevelBeat", 0) < 2)
            {
                PlayerPrefs.SetInt("HighestLevelBeat", 2);
                PlayerPrefs.SetInt("avaliblePoints", PlayerPrefs.GetInt("avaliblePoints", 0) + 4);
            }
            Instantiate(deathExplosion, transform.position, Quaternion.identity).Play();
            player.GetComponent<SceneManagerScript>().LoadVictoryScene(5);
            Destroy(gameObject);

            if (player.GetComponent<Player_Control>().player.health == player.GetComponent<Player_Control>().player.max_health)
            {
                PlayerPrefs.SetInt("PerfectAssasin", 1);
            }

            if (PlayerPrefs.GetInt("ChallengeMode", 0) == 1)
            {
                PlayerPrefs.SetInt("ChallengeCompleteBoss1", 1);
            }
        }
        else if (bossNum == 2 && !gameObject.GetComponent<EarthTitanScript>().self.AugmentHealth(health))
        {
            if (PlayerPrefs.GetInt("HighestLevelBeat", 0) < 3)
            {
                PlayerPrefs.SetInt("HighestLevelBeat", 3);
                PlayerPrefs.SetInt("avaliblePoints", PlayerPrefs.GetInt("avaliblePoints", 0) + 4);
            }
            Instantiate(deathExplosion, transform.position, Quaternion.identity).Play();
            player.GetComponent<SceneManagerScript>().LoadVictoryScene(5);
            Destroy(gameObject);

            if (player.GetComponent<Player_Control>().player.health == player.GetComponent<Player_Control>().player.max_health)
            {
                PlayerPrefs.SetInt("PerfectEarthTitan", 1);
            }

            if (PlayerPrefs.GetInt("ChallengeMode", 0) == 1)
            {
                PlayerPrefs.SetInt("ChallengeCompleteBoss2", 1);
            }
        }
        else if (bossNum == 3 && !gameObject.GetComponent<GraveKeeperScript>().self.AugmentHealth(health))
        {
            if (PlayerPrefs.GetInt("HighestLevelBeat", 0) < 4)
            {
                PlayerPrefs.SetInt("HighestLevelBeat", 4);
                PlayerPrefs.SetInt("avaliblePoints", PlayerPrefs.GetInt("avaliblePoints", 0) + 4);
            }
            Instantiate(deathExplosion, transform.position, Quaternion.identity).Play();
            player.GetComponent<SceneManagerScript>().LoadVictoryScene(5);
            Destroy(gameObject);

            if (player.GetComponent<Player_Control>().player.health == player.GetComponent<Player_Control>().player.max_health)
            {
                PlayerPrefs.SetInt("PerfectGraveKeeper", 1);
            }

            if (PlayerPrefs.GetInt("ChallengeMode", 0) == 1)
            {
                PlayerPrefs.SetInt("ChallengeCompleteBoss3", 1);
            }
        }
        else if (bossNum == 4 && !gameObject.GetComponent<ArchMageScript>().self.AugmentHealth(health))
        {
            if (PlayerPrefs.GetInt("HighestLevelBeat", 0) < 5)
            {
                PlayerPrefs.SetInt("HighestLevelBeat", 5);
                PlayerPrefs.SetInt("avaliblePoints", PlayerPrefs.GetInt("avaliblePoints", 0) + 5);
            }
            Instantiate(deathExplosion, transform.position, Quaternion.identity).Play();
            player.GetComponent<SceneManagerScript>().LoadVictoryScene(5);
            Destroy(gameObject);

            if (player.GetComponent<Player_Control>().player.health == player.GetComponent<Player_Control>().player.max_health)
            {
                PlayerPrefs.SetInt("PerfectArchMage", 1);
            }

            if (PlayerPrefs.GetInt("ChallengeMode", 0) == 1)
            {
                PlayerPrefs.SetInt("ChallengeCompleteBoss4", 1);
            }
        }

        else if (bossNum == 5 && !gameObject.GetComponent<TwinKnightsBossHealth>().AugmentHealth(health))
        {
            
            if (PlayerPrefs.GetInt("HighestLevelBeat", 0) < 6)
            {
                PlayerPrefs.SetInt("HighestLevelBeat", 6);
                PlayerPrefs.SetInt("avaliblePoints", PlayerPrefs.GetInt("avaliblePoints", 0) + 7);
            }
            Instantiate(deathExplosion, transform.position, Quaternion.identity).Play();
            player.GetComponent<SceneManagerScript>().LoadVictoryScene(5);
            Destroy(gameObject);

            if (player.GetComponent<Player_Control>().player.health == player.GetComponent<Player_Control>().player.max_health)
            {
                PlayerPrefs.SetInt("PerfectTwinKnights", 1);
            }

            if (PlayerPrefs.GetInt("ChallengeMode", 0) == 1)
            {
                PlayerPrefs.SetInt("ChallengeCompleteBoss5", 1);
            }
        }
        else if (bossNum == 6 && !gameObject.GetComponent<ChoasWardenScript>().self.AugmentHealth(health))
        {
            
            if (PlayerPrefs.GetInt("HighestLevelBeat", 0) < 7)
            {
                PlayerPrefs.SetInt("HighestLevelBeat", 7);
                PlayerPrefs.SetInt("avaliblePoints", PlayerPrefs.GetInt("avaliblePoints", 0) + 9);
            }
            Instantiate(deathExplosion, transform.position, Quaternion.identity).Play();
            player.GetComponent<SceneManagerScript>().LoadVictoryScene(5);
            Destroy(gameObject);

            if (player.GetComponent<Player_Control>().player.health == player.GetComponent<Player_Control>().player.max_health)
            {
                PlayerPrefs.SetInt("PerfectChaosWarden", 1);
            }

            if (PlayerPrefs.GetInt("ChallengeMode", 0) == 1)
            {
                PlayerPrefs.SetInt("ChallengeCompleteBoss6", 1);
            }
        }

        else if (bossNum == 7 && !gameObject.GetComponent<GodScript>().self.AugmentHealth(health))
        {
            player.GetComponent<Player_Control>().player.allowedToShoot = false;
            if (PlayerPrefs.GetInt("HighestLevelBeat", 0) < 8)
            {
                PlayerPrefs.SetInt("HighestLevelBeat", 8);
                PlayerPrefs.SetInt("avaliblePoints", PlayerPrefs.GetInt("avaliblePoints", 0) + 14);
            }
            Instantiate(deathExplosion, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity).Play();
            player.GetComponent<SceneManagerScript>().LoadGodVictory(10);
            Destroy(gameObject);

            if (player.GetComponent<Player_Control>().player.health == player.GetComponent<Player_Control>().player.max_health)
            {
                PlayerPrefs.SetInt("PerfectGod", 1);
            }

            if (PlayerPrefs.GetInt("ChallengeMode", 0) == 1)
            {
                PlayerPrefs.SetInt("ChallengeCompleteBoss7", 1);
            }
        }

    }
}
