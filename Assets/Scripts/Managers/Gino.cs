using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gino : MonoBehaviour
{
    public static Gino instance;
    public EntitiesManager entitiesManager;
    public UIManager uiManager;
    public GameManager gameManager;
    public SoundManager soundsManager;
    public DecorsManager decorsManager;
    public JuicyManager juicyManager;
    public CameraManager cameraManager;
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frames
    void Update()
    {
        
    }
}
