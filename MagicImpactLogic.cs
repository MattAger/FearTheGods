using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicImpactLogic : MonoBehaviour
{

    public ParticleSystem defualtExplosion;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.tag == "PlayerAttack")
        {
            if (collision.gameObject.tag == "PlayerAttack" || collision.gameObject.tag == "Player")
            {
                return;
            }
            if (!gameObject.name.Contains("Crit"))
            {
                Instantiate(defualtExplosion, transform.position, Quaternion.identity).Play();
            }
            else
            {
                Instantiate(defualtExplosion, transform.position, Quaternion.identity).Play();
                foreach (Collider2D hitTarg in Physics2D.OverlapCircleAll(transform.position, 30))
                {
                    
                    if (hitTarg.gameObject.name.Contains("Assasin") && hitTarg.gameObject.tag == "Minion" && hitTarg.gameObject != collision.gameObject)
                    {
                        hitTarg.gameObject.GetComponent<MiniAssasinBoss>().augmentHp(gameObject.GetComponent<Self_Destruct>().damage * -1);
                    }
                    else if (hitTarg.gameObject.tag == "Boss" && hitTarg.gameObject != collision.gameObject)
                    {
                        if (hitTarg.gameObject.name.Contains("TwinKnight"))
                        {
                            if (hitTarg.gameObject.name == "TwinKnightMage")
                            {
                                hitTarg.gameObject.GetComponent<TwinKightMage>().DoDamage(gameObject.GetComponent<Self_Destruct>().damage * -1);
                            }
                            else if (hitTarg.gameObject.name == "TwinKnightGuard")
                            {
                                hitTarg.gameObject.GetComponent<TwinKightGuard>().DoDamage(gameObject.GetComponent<Self_Destruct>().damage * -1);
                            }
                        }
                        else
                        {
                            hitTarg.gameObject.GetComponent<BossHealth>().AugmentHealth(gameObject.GetComponent<Self_Destruct>().damage * -1);
                        }
                        
                        //GameObject.Find("Canvas").GetComponent<BossHeathUIController>().AdjustBossHealthBar(collision.gameObject.GetComponent<BossHealth>().self.health);
                    }
                    else if (hitTarg.gameObject.name.Contains("MiniGhost") && hitTarg.gameObject != collision.gameObject)
                    {
                        hitTarg.gameObject.GetComponent<MiniGhostScript>().AugmentHealth(gameObject.GetComponent<Self_Destruct>().damage * -1);
                    }
                    else if (hitTarg.gameObject.name.Contains("graveStone") && hitTarg.gameObject != collision.gameObject)
                    {
                        hitTarg.gameObject.GetComponent<GraveStoneScript>().AugmentHp(gameObject.GetComponent<Self_Destruct>().damage * -1);
                    }
                    else if (hitTarg.gameObject.name.Contains("ChaosTurret") && hitTarg.gameObject != collision.gameObject)
                    {
                        hitTarg.gameObject.GetComponent<ChaosTurret>().AugmentHealth(gameObject.GetComponent<Self_Destruct>().damage * -1);
                    }
                }
            }

            

            if (collision.gameObject.tag == "Wall")
            {
                Destroy(gameObject);
            }
            else if (collision.gameObject.tag == "Boss")
            {
                if (collision.gameObject.name.Contains("TwinKnight"))
                {
                    if (collision.gameObject.name == "TwinKnightMage")
                    {
                        collision.gameObject.GetComponent<TwinKightMage>().DoDamage(gameObject.GetComponent<Self_Destruct>().damage * -1);
                    }
                    else if (collision.gameObject.name == "TwinKnightGuard")
                    {
                        collision.gameObject.GetComponent<TwinKightGuard>().DoDamage(gameObject.GetComponent<Self_Destruct>().damage * -1);
                    }
                }
                else
                {
                    collision.gameObject.GetComponent<BossHealth>().AugmentHealth(gameObject.GetComponent<Self_Destruct>().damage * -1);
                }
                
                
                //GameObject.Find("Canvas").GetComponent<BossHeathUIController>().AdjustBossHealthBar(collision.gameObject.GetComponent<BossHealth>().self.health);
                Destroy(gameObject);
                
            }
            else if (collision.gameObject.tag == "Minion")
            {
                if (collision.gameObject.name.Contains("Assasin"))
                {
                    collision.gameObject.GetComponent<MiniAssasinBoss>().augmentHp(gameObject.GetComponent<Self_Destruct>().damage * -1);
                    Destroy(gameObject);
                }
                else if (collision.gameObject.name.Contains("MiniGhost"))
                {
                    collision.gameObject.GetComponent<MiniGhostScript>().AugmentHealth(gameObject.GetComponent<Self_Destruct>().damage * -1);
                    Destroy(gameObject);
                }
                else if (collision.gameObject.name.Contains("graveStone"))
                {
                    collision.gameObject.GetComponent<GraveStoneScript>().AugmentHp(gameObject.GetComponent<Self_Destruct>().damage * -1);
                    Destroy(gameObject);
                }
                else if (collision.gameObject.name.Contains("ChaosTurret"))
                {
                    collision.gameObject.GetComponent<ChaosTurret>().AugmentHealth(gameObject.GetComponent<Self_Destruct>().damage * -1);
                    Destroy(gameObject);
                }
            }

            
            
            else
            {
                if (collision.gameObject.name.Contains("ChaosShot"))
                {
                    Destroy(collision.gameObject);
                }
                Destroy(gameObject);
            }
        }
        else
        {
            if (collision.gameObject.tag == "Wall")
            {
                
                if (gameObject.name.Contains("ThrowingRock"))
                {
                    Instantiate(defualtExplosion, transform.position, Quaternion.identity).Play();
                }
                else if (gameObject.name.Contains("darkShot"))
                {
                    Instantiate(defualtExplosion, transform.position, Quaternion.identity).Play();
                }
                else if (gameObject.name.Contains("MageBullet"))
                {
                    Instantiate(defualtExplosion, transform.position, Quaternion.identity).Play();
                }
                else if (gameObject.name.Contains("SwordParticleBlast"))
                {
                    Instantiate(defualtExplosion, transform.position, Quaternion.identity).Play();
                }
                else if (gameObject.name.Contains("FollowOrb") || gameObject.name.Contains("TrackingFireBall") || gameObject.name.Contains("ChaosShot") || gameObject.name.Contains("ChaosBouncer"))
                {
                    return;
                }
                Destroy(gameObject);
            }
            else if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<Player_Control>().AugmentHealth(gameObject.GetComponent<Self_Destruct>().damage * -1);

                if (gameObject.name.Contains("ThrowingRock"))
                {
                    Instantiate(defualtExplosion, transform.position, Quaternion.identity).Play();
                }
                else if (gameObject.name.Contains("darkShot"))
                {
                    Instantiate(defualtExplosion, transform.position, Quaternion.identity).Play();
                }
                else if (gameObject.name.Contains("MageBullet"))
                {
                    Instantiate(defualtExplosion, transform.position, Quaternion.identity).Play();
                }
                else if (gameObject.name.Contains("SwordParticleBlast"))
                {
                    Instantiate(defualtExplosion, transform.position, Quaternion.identity).Play();
                }
                else if (gameObject.name.Contains("ChaosShot"))
                {
                    Instantiate(defualtExplosion, transform.position, Quaternion.identity).Play();
                }

                Destroy(gameObject);
            }
            else
            {
                if (collision.gameObject.tag == "PlayerAttack")
                {
                    return;
                }
                if (gameObject.name.Contains("ThrowingRock"))
                {
                    //Instantiate(defualtExplosion, transform.position, Quaternion.identity).Play();
                   // Destroy(gameObject);
                }
                else if (gameObject.name.Contains("darkShot"))
                {
                    Instantiate(defualtExplosion, transform.position, Quaternion.identity).Play();
                    Destroy(gameObject);
                }
                else if (gameObject.name.Contains("MageBullet"))
                {
                    Instantiate(defualtExplosion, transform.position, Quaternion.identity).Play();
                }
                else if (gameObject.name.Contains("SwordParticleBlast"))
                {
                    Instantiate(defualtExplosion, transform.position, Quaternion.identity).Play();
                }


            }
            
        }
        
    }
}
