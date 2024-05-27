using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleTest : MonoBehaviour
{
    public ParticleSystem particleSystem;

    void Start()
    {
        if (particleSystem != null)
        {
            Debug.Log("Play");
            particleSystem.Play();
        }
        else
        {
            Debug.LogError("Particle System is not assigned.");
        }
    }
}
