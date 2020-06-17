using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashController : MonoBehaviour {

    // Use this for initialization
    [SerializeField] ParticleSystem particle;
    [SerializeField] SpriteRenderer splashImg;

	void Start () {
        Invoke("deleteObj",5);

        splashImg.color = GameManager.self.colorManager.SplashColors[ColorManager.CurrentColorIndex];

        ParticleSystemRenderer renderer = particle.GetComponent<ParticleSystemRenderer>();

        renderer.material = GameManager.self.colorManager.SplashMaterials[ColorManager.CurrentColorIndex];;
    }
	
    void deleteObj(){
        Destroy(gameObject);
    }
}
