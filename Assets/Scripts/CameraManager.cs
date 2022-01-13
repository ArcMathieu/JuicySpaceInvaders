using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;

    public Transform player;
    public Transform targetCam;
    
    public bool InverseCam;

    public bool transitionDone;
    public bool transitionZoom;
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

        NewRDZoom = true;
        canZoomCorout = true;

    }
    public float RandomZoomCamera = 0;
    public bool NewRDZoom;
    public bool canZoomCorout;
    // Update is called once per frame
    void Update()
    {   
        if (Gino.instance.juicyManager.isCamera)
        CameraShake();

        if (Input.GetKeyDown(KeyCode.E))
            ZoomEffect();

        //if(RandomZoomCamera < 0 && !NewRDZoom)
        //{
        //    ZoomEffect();
        //}
        //else
        //{
        //    if (NewRDZoom)
        //    {
        //        RandomZoomCamera = Random.Range(1, 3);
        //        NewRDZoom = false;
        //    }
        //    RandomZoomCamera -= Time.deltaTime;
        //}

        if (GameManager.instance.isStart)
        {
            player = FindObjectOfType<PlayerController>().transform;
            cam2.Follow = player;
        }

        switch (transitionDone)
        {
            case true:
                Cam2Movement();
                break;
            case false:
                UnZoomToCam2();
                break;
        }
        switch (transitionZoom)
        {
            case true:
                HitZoomCamera();
                break;
            case false:
                UnZoomCamera();
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
    public void ZoomEffect()
    {
        //StartCoroutine(ZoomCoroutine());
        HitZoomCamera();
    }
    IEnumerator ZoomCoroutine()
    {
        transitionZoom = true;
        yield return new WaitForSeconds(2);
        transitionZoom = false;
    }

    public void ZoomCamera()
    {
        if (cam2.m_Lens.FieldOfView > 3)
        {
            cam2.m_Lens.FieldOfView -= 10 * Time.deltaTime;
        }
        else cam2.m_Lens.FieldOfView = 3;
        cam2.LookAt = player;
        cam2.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0.3f;
        cam2.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0.3f;
    }
    public void HitZoomCamera()
    {
        if (cam2.m_Lens.FieldOfView > 15)
        {
            cam2.m_Lens.FieldOfView -= 10 * Time.deltaTime;
        }
        else UnZoomCamera();
        cam2.LookAt = player;
        cam2.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0.5f;
        cam2.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0.5f;
    }
    public void UnZoomCamera()
    {
        if (cam2.m_Lens.FieldOfView < 20)
        {
            cam2.m_Lens.FieldOfView += 10 * Time.deltaTime;
        }
        else
        {
            cam2.m_Lens.FieldOfView = 20;
            //NewRDZoom = true;
            //canZoomCorout = true;
        }
        cam2.LookAt = targetCam;
        cam2.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0.7f;
        cam2.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 1f;
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
                float shakeValue = Mathf.Lerp(shakes[i].shakeDuration, 0, shakes[i].currentShakeDuration);
                shakes[i].currentShakeAmount = shakes[i].shakeAmount * curve.Evaluate(shakeValue);
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
        camTransform.localPosition = originalPos + Random.insideUnitSphere * mainAmount * Gino.instance.soundsManager.ShakeMultiplier;
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
