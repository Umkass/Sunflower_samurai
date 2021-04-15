using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public static ParticleController instance = null;
    [SerializeField]
    GameObject dust;
    [SerializeField]
    GameObject smoke;
    [SerializeField]
    GameObject petalEffect;
    [SerializeField]
    GameObject petalGravityEffect;
    [SerializeField]
    GameObject petalInfinityEffect;
    [SerializeField]
    GameObject[] spotsBlood;
    [SerializeField]
    GameObject dropsBlood;
    float lifeTimeBlood=0.9f;
    float lifeTimePetal=0.3f;

    public GameObject Dust { get => dust; set => dust = value; }
    public GameObject Smoke { get => smoke; set => smoke = value; }

    public void DeathEffectStart(Transform transformDieObject)
    {
        var spot = Instantiate(spotsBlood[Random.Range(0, spotsBlood.Length)], transformDieObject.position, Quaternion.identity, transformDieObject);
        Vector3 tempVectorDrops = new Vector3(transformDieObject.position.x, transformDieObject.position.y + 0.185f);
        var drops = Instantiate(dropsBlood, tempVectorDrops,Quaternion.identity, transformDieObject);
        Destroy(drops, lifeTimeBlood);
    }
    public void DustEffectPlay()
    {
        dust.GetComponent<ParticleSystem>().Play();
    }
    public void DustEffectPause()
    {
        dust.GetComponent<ParticleSystem>().Pause();
    }

    public void DustEffectStop()
    {
        if (dust != null)
        {
            dust.GetComponent<ParticleSystem>().Stop();
            Destroy(dust);
        }
    }

    public void SmokeEffectPlay()
    {
        smoke.GetComponent<ParticleSystem>().Play();
    }

    public void SmokeEffectStop()
    {
        if (smoke != null)
        {
            smoke.GetComponent<ParticleSystem>().Stop();
            Destroy(smoke);
        }
    }

    public void PetalEffectStart(Transform transform)
    {
        var petal = Instantiate(petalEffect, transform.position, Quaternion.identity, transform);
        Destroy(petal, lifeTimePetal);
    }

    public void PetalGravityEffectStart(Transform transform)
    {
        var petalGravity = Instantiate(petalGravityEffect, transform.position, Quaternion.identity, transform);
        Destroy(petalGravity, lifeTimePetal);
    }

    public void PetalInfinityEffectStart(Transform transform)
    {
        var petalInfinity = Instantiate(petalInfinityEffect, transform.position, Quaternion.identity, transform);
        Destroy(petalInfinity, lifeTimePetal);
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
