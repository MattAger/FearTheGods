using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosShotHelper : MonoBehaviour
{
    public CircleCollider2D thiscol;
    public float time = .2f;

    public float acceleration;
    Rigidbody2D selfRb;

    public ParticleSystem boom;

    public float destructTime;
    // Start is called before the first frame update
    void Start()
    {
        selfRb = gameObject.GetComponent<Rigidbody2D>();
        Invoke("ReinableCollider", time);
        Invoke("Destruct", destructTime);
    }

    public void ReinableCollider()
    {
        thiscol.enabled = true;
    }

    public void Update()
    {
        selfRb.AddForce(transform.up * Random.Range(-acceleration, acceleration));
    }

    public void Destruct()
    {
        Instantiate(boom, transform.position, Quaternion.identity).Play();
        Destroy(gameObject);
    }
}
