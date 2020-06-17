using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {

    [SerializeField] GameObject[] NormalBlocks;
    [SerializeField] GameObject[] RedBlocks;


    

    void Start ()
    {
        UpdateColors();
    }

    public void InitPlatform(PlatformSettings platformSettings, float offsetRotation, int platformIndex)
    {

        for (int i = 0; i < platformSettings.DisabledBlocks.Count; i++)
        {
            int index = i;
            int blockToDisable = platformSettings.DisabledBlocks[index];

            NormalBlocks[blockToDisable].SetActive(false);
        }

        if(platformIndex > 4){
            for (int i = 0; i < platformSettings.EnabledRedBlocks.Count; i++)
            {
                int index = i;
                int blockToEnable = platformSettings.EnabledRedBlocks[index];

                RedBlocks[blockToEnable].SetActive(true);
            }

            for (int i = 0; i < platformSettings.RotatingRedBlocks.Count; i++)
            {
                int index = i;
                int blockToRotate = platformSettings.RotatingRedBlocks[index];

                redRotationRoutines.Add(StartCoroutine(rotatingBlocksAnimation(RedBlocks[blockToRotate], platformSettings)));

            }
            for (int i = 0; i < platformSettings.RotatingNormalBlocks.Count; i++)
            {
                int index = i;
                int blockToRotate = platformSettings.RotatingNormalBlocks[index];

                normalRotationRoutines.Add(StartCoroutine(rotatingBlocksAnimation(NormalBlocks[blockToRotate], platformSettings)));

            }
        }



        transform.localEulerAngles = new Vector3(0,offsetRotation,0);


    }


    List<Coroutine> redRotationRoutines = new List<Coroutine>();
    List<Coroutine> normalRotationRoutines = new List<Coroutine>();

    IEnumerator rotatingBlocksAnimation(GameObject Block,PlatformSettings platformSettings){

        float startRotation = Block.transform.localEulerAngles.y - platformSettings.rotatingRedAngle / 2;
        float endRotation = Block.transform.localEulerAngles.y+ platformSettings.rotatingRedAngle/2;

        float t = 0;
        bool dirRight = false;

        while (true){
            if(GameManager.GameStarted){

                t += platformSettings.rotatingRedSpeed * Time.deltaTime;

                float rotation = Block.transform.localEulerAngles.y;
                if (t<1){

                    float t1 = t;
                    if (dirRight) t1 = 1 - t;

                    rotation = Mathf.SmoothStep(startRotation, endRotation, t1);
                }else{
                    dirRight = !dirRight;
                    t = 0;
                }

                Block.transform.localEulerAngles = new Vector3(0, rotation,0);
            }


            yield return new WaitForEndOfFrame();
        }

    }


    public bool alreadyJumped = false;
    public void Jump(bool isFeverMode){
        alreadyJumped = true;

        if(isFeverMode) BreakPlatform();
    }

    [SerializeField] Animator[] BrickAnimators;
    [SerializeField] GameObject RedBlocksContainer;

    public bool platformBreaked = false;

    void BreakPlatform(){
        platformBreaked = true;

        RedBlocksContainer.SetActive(false);

        for (int i = 0; i < BrickAnimators.Length; i++)
        {
            BrickAnimators[i].enabled = true;
            BrickAnimators[i].Play("BreakBlock",-1,0);
        }

        for (int i = 0; i < NormalBlockMeshes.Length; i++)
        {
            NormalBlockMeshes[i].material = GameManager.self.colorManager.BallMaterials[ColorManager.CurrentColorIndex];
        }

    }

    public bool isPlatformPassed = false;

    public void PlatformPassed(){
        isPlatformPassed = true;

        StartCoroutine(PlatformHideAnimations());
    }


    IEnumerator PlatformHideAnimations(){
        yield return new WaitForSeconds(0.3f);

        Destroy(gameObject);
    }



    [SerializeField] MeshRenderer[] NormalBlockMeshes;
    [SerializeField] MeshRenderer[] RedBlockMeshes;

    [SerializeField] ParticleSystem[] BreakBlockParticles;

    [SerializeField] 

    void UpdateColors(){
        for (int i = 0; i < NormalBlockMeshes.Length; i++)
        {
            NormalBlockMeshes[i].material = GameManager.self.colorManager.BlockMaterials[ColorManager.CurrentColorIndex];
        }
        
        for (int i = 0; i < RedBlockMeshes.Length; i++)
        {
            RedBlockMeshes[i].material = GameManager.self.colorManager.RedBlockMaterials[ColorManager.CurrentColorIndex];
        }


        for (int i = 0; i < BreakBlockParticles.Length; i++)
        {
            ParticleSystemRenderer particleRenderer = BreakBlockParticles[i].GetComponent<ParticleSystemRenderer>();
            particleRenderer.material = GameManager.self.colorManager.BallMaterials[ColorManager.CurrentColorIndex];
        }




    }

}//end
