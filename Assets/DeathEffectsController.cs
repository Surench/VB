using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffectsController : MonoBehaviour {

    // Use this for initialization
    [SerializeField] ParticleSystem particle;

    void Start()
    {
        Invoke("deleteObj", 5);

        ParticleSystemRenderer renderer = particle.GetComponent<ParticleSystemRenderer>();

        renderer.material = GameManager.self.colorManager.SplashMaterials[ColorManager.CurrentColorIndex]; ;
    }

    void deleteObj()
    {
        Destroy(gameObject);
    }

}
