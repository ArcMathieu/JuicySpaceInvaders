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
        transitionDone = true;
        cam1.m_Lens.FieldOfView = 40;
        cam1.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 0;

        //cam1.gameObject.SetActive(true);
        //cam2.gameObject.SetActive(false);

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
        if(cam1.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition >= 1 && cam1.m_Lens.FieldOfView < 50)
        {
            cam1.m_Lens.FieldOfView += Time.deltaTime * 3;
        }
        if (cam1.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition >= 1 && cam1.m_Lens.FieldOfView >= 50)
        {
            cam1.gameObject.SetActive(false);
            cam2.gameObject.SetActive(true);
            transitionDone = true;
        }
    }

    public void Cam2Movement()
    {
        if (InverseCam)
        {
            pathPos = (player.position.x / -25) + 1;
        }
        else
        {
            pathPos = (player.position.x / 25) + 1;
        }
            
        cam2.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = pathPos;
    }
}
