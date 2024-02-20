using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainParticleController : MonoBehaviour
{
    public ParticleSystem particleSystem;

    public void SetEmissionRate(float rate)
    {
        var emission = particleSystem.emission;
        emission.rateOverTime = rate;
    }
}
