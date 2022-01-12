using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;

    public Transform player;
    
    public bool InverseCam;

    public bool transitionDone;
    private float pathPos;

    public Transform camTransform;
    private Vector3 originalPos = Vector3.zero;
    public AnimationCurve curve;

    public class Shake {
        public float shakeAmount = 0;
        public float currentShakeAmount = 0;
        public float shakeDuration = 0;
        public float currentShakeDuration = 0;
    }
    List<Shake> shakes = new List<Shake>();
    // Start is called before the first frame update
    void Start()
    {
        originalPos = camTransform.localPosition;
        transitionDone = false;
        cam1.m_Lens.FieldOfView = 40;
        cam1.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 0;

        cam1.gameObject.SetActive(true);
        cam2.gameObject.SetActive(false);

        cam2.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 1;

    }

    // Update is called once per frame
    void Update()
    {
        CameraShake();
        switch (transitionDone)
        {
            case true:
                Cam2Movement();
                break;
            case false:
                UnZoomToCam2();
                break;
        }
    }

    public void UnZoomToCam2()
    {
        if (cam1.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition >= 1)
        {
            cam1.gameObject.SetActive(false);
            cam2.gameObject.SetActive(true);
            transitionDone = true;
        }
    }
    public float maxrange;
    public CinemachineSmoothPath dollytrack;
    public void Cam2Movement()
    {
        if (InverseCam)
        {
            pathPos = (player.position.x / -maxrange) + 1;
        }
        else
        {
            pathPos = (player.position.x / maxrange) + 1;
        }

        if (pathPos < 0) pathPos = 0;
        
        cam2.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = pathPos;
    }

    public void CameraShake() {
        float mainAmount = 0;
        List<Shake> toRemove = new List<Shake>();
        for (int i = 0; i < shakes.Count; i++) {
            if (shakes[i].currentShakeAmount > 0 && shakes[i].currentShakeDuration > 0) {
                shakes[i].currentShakeDuration -= Time.deltaTime;
                if (shakes[i].currentShakeDuration < 0) {
                    shakes[i].currentShakeDuration = 0;
                }
                float durationRemaining = Mathf.Clamp01(shakes[i].currentShakeDuration);
                float shakeValue = Mathf.Lerp(shakes[i].shakeAmount, 0, durationRemaining);
                shakes[i].currentShakeAmount = curve.Evaluate(shakeValue);
                if (shakes[i].currentShakeAmount > mainAmount) {
                    mainAmount = shakes[i].currentShakeAmount;
                }
            } else if (shakes[i].currentShakeDuration != 0 || shakes[i].currentShakeAmount != 0) {
                toRemove.Add(shakes[i]);
            }
        }
        for (int i = 0; i < toRemove.Count; i++) {
            shakes.Remove(toRemove[i]);
        }
        camTransform.localPosition = originalPos + Random.insideUnitSphere * mainAmount;
    }

    public void NewCameraShake(float newDuration, float newAmount) {
        Shake newShake = new Shake();
        newShake.currentShakeAmount = newAmount;
        newShake.shakeAmount = newAmount;
        newShake.currentShakeDuration = newDuration;
        newShake.shakeDuration = newDuration;
        shakes.Add(newShake);
    }
}
