using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {
    [SerializeField] Rigidbody rb;
    [SerializeField] AnimationCurve ballJumpCurve;

    [SerializeField] float maxHeight;
    [SerializeField] float JumpTime;

    [SerializeField] GameObject camera;

    [SerializeField] Animator animator;

    [SerializeField] GameObject SplashPrefab;
    [SerializeField] GameObject DeathEffectsPrefab;

    [SerializeField] GameObject NormalEffects;
    [SerializeField] GameObject FeverEffects;

    [SerializeField] GameObject BallVisual;


    Coroutine jumpAnimationRoutine;

    private bool CameraMovmendEnabled;
    private bool jumpEnabled;

    private Vector3 ballStartPosition;

    [SerializeField] MeshRenderer meshRenderer;

    [SerializeField] float velocityMultiplyer;
    [SerializeField] float maxVelocity;
    [SerializeField] float feverTimer;

    private void Awake()
    {
        ballStartPosition = transform.position;
    }

    public void InitBall(){
        jumpEnabled = true;
        transform.position = ballStartPosition;
        camera.transform.position = new Vector3(camera.transform.position.x, transform.position.y + 9, camera.transform.position.z);

        exitFever(true);

        BallVisual.SetActive(true);

    }


    private void Jump(GameObject triggeredBlock)
    {
        if (!jumpEnabled) return; // do not jump if collided to red

        GameObject platform = triggeredBlock.transform.parent.parent.parent.gameObject;
        PlatformController platformController = platform.GetComponent<PlatformController>();

        if (platformController.isPlatformPassed) return;

        platformController.Jump(feverMode);

        JumpEffects(platform);

        CameraMovmendEnabled = false;
        if (jumpAnimationRoutine != null)
        {
            StopCoroutine(jumpAnimationRoutine);
            jumpAnimationRoutine = null;
        }

        jumpAnimationRoutine = StartCoroutine(JumpAnimation());

        animator.Play("BallBounce",-1,0);
        animator.speed = 1/JumpTime;


        GameManager.self.soundManager.JumpSound();
        GameManager.self.soundManager.ResetClickPitch();
        GameManager.self.soundManager.ResetDiamondPitch();

        GameManager.self.scoreManager.resetCombo();


        camera.transform.position = new Vector3(camera.transform.position.x, triggeredBlock.transform.position.y + 9, camera.transform.position.z);


#if UNITY_IOS
        if (GameManager.TapticEnabled) TapticEngine.TriggerMedium();
#endif


        jumpEnabled = false;
        Invoke("enableJump_",0.1f);

    }
    void enableJump_() { jumpEnabled = true; }

    IEnumerator JumpAnimation()
    {
        Vector3 ballStartPos = rb.transform.position;
        float startTime = Time.time;

        float vel = 0;
        Vector3 ballLastFramePos= ballStartPos;

        float t = 0;
        while (t < 1)
        {
            t = (Time.time - startTime) / JumpTime;

            float ballPos = ballJumpCurve.Evaluate(t)* maxHeight;

            vel = (rb.transform.position.y - ballLastFramePos.y) / Time.deltaTime;

            ballLastFramePos = rb.transform.position;
            rb.transform.position = ballStartPos + new Vector3(0, ballPos, 0);           


            yield return new WaitForFixedUpdate();
        }

        vel = Mathf.Abs(vel);

        float freeFalldeltaVelocity = 0;

        float freeFallStartTime = Time.time;

        while (true)
        {
            if(Time.time-freeFallStartTime > feverTimer && !feverMode){
                enterFever();
            }

            rb.transform.position -= new Vector3(0, vel * (1 + freeFalldeltaVelocity) * Time.deltaTime, 0);

            if(ScoreManager.comboMultiplyer>1 || feverMode){
                freeFalldeltaVelocity = maxVelocity;
                //freeFalldeltaVelocity += velocityMultiplyer;
                //if (freeFalldeltaVelocity >= maxVelocity) freeFalldeltaVelocity = maxVelocity;
            }

            if (CameraMovmendEnabled)
            {
                camera.transform.position = new Vector3(camera.transform.position.x, rb.transform.position.y + 9, camera.transform.position.z);
            }
           
            yield return new WaitForFixedUpdate();
        }
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Block")
        {
            Jump(other.gameObject);
        }else
        if (other.gameObject.tag == "Platform")
        {
            AddScore(other.gameObject);
        }
        else
        if (other.gameObject.tag == "Red")
        {
            GameOver(other.gameObject);
        }
    }

    private void AddScore(GameObject platform)
    {
        if (!jumpEnabled) return; //if already jumped, do not add a score

        platform = platform.transform.parent.gameObject;
        PlatformController platformController = platform.GetComponent<PlatformController>();


        GameManager.self.scoreManager.AddScore(platformController.alreadyJumped);
    

        bool levelPassed = GameManager.self.levelManager.PlatformPassed();

        CameraMovmendEnabled = true;
        platformController.PlatformPassed();


        if (!platformController.platformBreaked) GameManager.self.soundManager.ClickSound();
    }

    private void JumpEffects(GameObject platform)
    {
        if(!feverMode){
            Vector3 splashPos = new Vector3(rb.transform.position.x, platform.transform.position.y, rb.transform.position.z);
            GameObject go = Instantiate(SplashPrefab, splashPos, Quaternion.identity, platform.transform);

            go.transform.localEulerAngles = new Vector3(0, Random.Range(-120, 360), 0);
        }


    }

    bool feverMode = false;
    [SerializeField] Material BallFeverMaterial;

    public void enterFever(){
        feverMode = true;

        NormalEffects.SetActive(false);
        FeverEffects.SetActive(true);

        meshRenderer.material = BallFeverMaterial;

    }
    public void exitFever(bool force = false){
        if(feverMode || force){
            feverMode = false;

            NormalEffects.SetActive(true);
            FeverEffects.SetActive(false);

            meshRenderer.material = GameManager.self.colorManager.BallMaterials[ColorManager.CurrentColorIndex];
        }


    }




    private void GameOver(GameObject redBlock){

        if (feverMode) return;

        if (jumpAnimationRoutine != null)
        {
            StopCoroutine(jumpAnimationRoutine);
            jumpAnimationRoutine = null;
        }

        jumpEnabled = false;

        GameObject parent = redBlock.transform.parent.parent.gameObject;
        Vector3 DeathEffectsPos = new Vector3(rb.transform.position.x, parent.transform.position.y, rb.transform.position.z);
        Instantiate(DeathEffectsPrefab, DeathEffectsPos, Quaternion.identity, parent.transform);

        BallVisual.SetActive(false);

        GameManager.self.GameOver();

        exitFever();
        GameManager.self.soundManager.deathSound();

    }

    public void LevelPassed(){
        CameraMovmendEnabled = false;
    }



}//end
