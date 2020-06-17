using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        RotateDiamond();

    }
	
    void RotateDiamond(){
        StartCoroutine(DiamondRotation());
    }

    [SerializeField] Transform meshContainer;
    [SerializeField] float RotationSpeed;

    IEnumerator DiamondRotation(){

        Vector3 rotation = meshContainer.localEulerAngles;

        while (true){

            rotation += new Vector3(RotationSpeed, 0,0)*Time.deltaTime;
            meshContainer.localEulerAngles = rotation;

            yield return new WaitForEndOfFrame();
        }
    }

    [SerializeField] Animator animator;

    public void CollectDiamond(){
        animator.Play("collectDiamond", -1, 0);

        GameManager.self.soundManager.diamondSound();

        GameManager.self.scoreManager.AddDiamond();


#if UNITY_IOS
        if (GameManager.TapticEnabled) TapticEngine.TriggerMedium();
#endif
    }

}
