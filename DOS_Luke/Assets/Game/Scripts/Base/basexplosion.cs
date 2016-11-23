using UnityEngine;
using System.Collections;

public class basexplosion : MonoBehaviour
{

    public ParticleSystem ExplosionParticles;

    // Update is called once per frame
    void Update()
    {
        if (Gobal.Heath == 0)
        {
            ExplosionParticles.Play();
        }
    }
}

