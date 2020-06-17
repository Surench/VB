using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    [SerializeField] AudioSource jumpAudio;
    [SerializeField] AudioSource clickAudio;
    [SerializeField] AudioSource diamondAudio;
    [SerializeField] AudioSource deathAudio;

    public void InitSoundManager(){
        ResetClickPitch();
        ResetDiamondPitch();
    }

    public void JumpSound(){
        if (GameManager.SoundEnabled) jumpAudio.Play();
    }


    [SerializeField] private Vector2 DiamondPitch;
    [SerializeField] private float DiamondPitchDelta = 0.15f;
    float DiamondPitchT = 0;


    public void diamondSound()
    {
        if (GameManager.SoundEnabled){
            diamondAudio.Play();

            diamondAudio.pitch = Mathf.Lerp(DiamondPitch.x, DiamondPitch.y, DiamondPitchT);
            DiamondPitchT += DiamondPitchDelta;
        }
    }
    public void ResetDiamondPitch()
    {
        DiamondPitchT = 0;
        diamondAudio.pitch = DiamondPitch.x;
    }

    public void deathSound()
    {
        if (GameManager.SoundEnabled) deathAudio.Play();
    }

    [SerializeField] private Vector2 ClickPitch;
    [SerializeField] private float clickPitchDelta = 0.15f;
    float clickPitchT = 0;

    public void ClickSound()
    {
        if (GameManager.SoundEnabled){
            clickAudio.Play();
            clickAudio.pitch = Mathf.Lerp(ClickPitch.x, ClickPitch.y, clickPitchT);
            clickPitchT += clickPitchDelta;
        }
        
    }
    public void ResetClickPitch(){
        clickPitchT = 0;
        clickAudio.pitch = ClickPitch.x;
    }

}
