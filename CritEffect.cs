using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritEffect : MonoBehaviour
{
    public ParticleSystem partsys;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(partsys, transform.position, Quaternion.identity).Play();

    }
}
