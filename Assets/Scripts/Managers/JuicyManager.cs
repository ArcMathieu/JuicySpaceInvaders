using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuicyManager : MonoBehaviour
{
    public List<ParticleJuicy> particles = new List<ParticleJuicy>();
    public List<LightJuicy> lights = new List<LightJuicy>();
    public List<GameObject> toggles = new List<GameObject>();
    public GameObject juicyManagerCanvas;
    public bool isJuicyManager;
    public bool isAll;
    public bool isParticle;
    public bool isSound;
    public bool isUI;
    public bool isMovement;
    public bool isLight;
    public bool isAnimation;
    public bool isCamera;

    private void Start() {
       isAll = false;
       AllSwitch();
    }
    private void Update() {
        if (Input.GetKeyDown("e")) {
            isJuicyManager = !isJuicyManager;
            juicyManagerCanvas.SetActive(isJuicyManager);
        }
        if (Input.GetKeyDown("1")) {
            UiSwitch();
        }
        if (Input.GetKeyDown("2")) {
            ParticleSwitch();
        }
        if (Input.GetKeyDown("3")) {
            LightSwitch();
        }
        if (Input.GetKeyDown("4")) {
            AnimationSwitch();
        }
        if (Input.GetKeyDown("5")) {
            CameraSwitch();
        }
        if (Input.GetKeyDown("6")) {
            MovementSwitch();
        }
        if (Input.GetKeyDown("7")) {
            SoundSwitch();
        }
        if (Input.GetKeyDown("8")) {
            AllSwitch();
        }
    }


    public void UiSwitch(){
        isUI = !isUI;
        toggles[0].SetActive(isUI);
        if (isUI) {
            Gino.instance.uiManager.uiJuicyParent.gameObject.SetActive(true);
        } else {
            Gino.instance.uiManager.uiJuicyParent.gameObject.SetActive(false);
        }
    }
    public void ParticleSwitch() {
        isParticle = !isParticle;
        toggles[1].SetActive(isParticle);
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
    public void LightSwitch() {
        isLight = !isLight;
        toggles[2].SetActive(isLight);
        if (isLight) {
            for (int i = 0; i < lights.Count; i++) {
                lights[i].gameObject.SetActive(true);
            }
        } else {
            for (int i = 0; i < lights.Count; i++) {
                lights[i].gameObject.SetActive(false);
            }
        }
        
    }
    public void AnimationSwitch() {
        isAnimation = !isAnimation;
        toggles[3].SetActive(isAnimation);
    }
    public void CameraSwitch() {
        isCamera = !isCamera;
        toggles[4].SetActive(isCamera);
        if (isCamera)
        {
            Gino.instance.cameraManager.cantMove = false;
        }
        else
        {
            Gino.instance.cameraManager.cantMove = true;
        }
    }
    public void MovementSwitch() {
        isMovement = !isMovement;
        toggles[5].SetActive(isMovement);
    }
    public void SoundSwitch() {
        isSound = !isSound;
        toggles[6].SetActive(isSound);
        if (isSound) {
            Gino.instance.soundsManager.StartSound();
        } else {
            Gino.instance.soundsManager.StopSound();
        }
    }
    public void AllSwitch() {
        isAll = !isAll;
        toggles[7].SetActive(isAll);
        isCamera = !isAll;
        isAnimation = !isAll;
        isLight = !isAll;
        isMovement = !isAll;
        isUI = !isAll;
        isSound = !isAll;
        isParticle = !isAll;
        ParticleSwitch();
        SoundSwitch();
        UiSwitch();
        MovementSwitch();
        LightSwitch();
        AnimationSwitch();
        CameraSwitch();
    }
}
