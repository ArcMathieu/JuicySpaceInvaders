using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuicyManager : MonoBehaviour
{
    public List<ParticleJuicy> particles = new List<ParticleJuicy>();
    public List<LightJuicy> lights = new List<LightJuicy>();
    public bool isParticle;
    public bool isSound;
    public bool isUI;
    public bool isMovement;
    public bool isLight;
    public bool isAnimation;
    public bool isCamera;

    private void Start() {
        ParticleSwitch();
        SoundSwitch();
        UiSwitch();
        MovementSwitch();
        LightSwitch();
        AnimationSwitch();
        CameraSwitch();
    }
    public void ParticleSwitch() {
        isParticle = !isParticle;
        if (isParticle) {
            for (int i = 0; i < particles.Count; i++) {
                particles[i].gameObject.SetActive(true);
            }
        } else {
            for (int i = 0; i < particles.Count; i++) {
                particles[i].gameObject.SetActive(false);
            }
        }
    }

    public void SoundSwitch() {
        isSound = !isSound;
        if (isSound) {
            Gino.instance.soundsManager.gameObject.SetActive(true);
        } else {
            Gino.instance.soundsManager.gameObject.SetActive(false);
        }
    }

    public void UiSwitch(){
        isUI = !isUI;
        if (isUI) {
            Gino.instance.uiManager.uiJuicyParent.gameObject.SetActive(true);
        } else {
            Gino.instance.uiManager.uiJuicyParent.gameObject.SetActive(false);
        }
    }

    public void MovementSwitch() {
        isMovement = !isMovement;
    }

    public void LightSwitch() {
        isLight = !isLight;
        if (isLight) {
            for (int i = 0; i < particles.Count; i++) {
                lights[i].gameObject.SetActive(true);
            }
        } else {
            for (int i = 0; i < particles.Count; i++) {
                lights[i].gameObject.SetActive(false);
            }
        }
    }

    public void AnimationSwitch() {
        isAnimation = !isAnimation;
    }

    public void CameraSwitch() {
        isCamera = !isCamera;
    }
    public void AllSwitch()
    {
        //isCamera = !isCamera;
    }
}
