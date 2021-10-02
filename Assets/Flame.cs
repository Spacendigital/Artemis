using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    ParticleSystem[] Flames;

    // Start is called before the first frame update
    void Start()
    {
        Flames = GetComponentsInChildren<ParticleSystem>();

        foreach (ParticleSystem flame in Flames)
        {
            flame.Stop();
        }
    }
}
