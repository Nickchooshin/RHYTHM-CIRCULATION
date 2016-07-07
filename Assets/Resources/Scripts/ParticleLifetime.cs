using UnityEngine;
using System.Collections;

public class ParticleLifetime : MonoBehaviour {

    public ParticleSystem particle;

    void Update()
    {
        if (!particle.IsAlive())
            Destroy(gameObject);
    }
}
