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

    // Start is called before the first frame update
    void Start()
    {
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
}
