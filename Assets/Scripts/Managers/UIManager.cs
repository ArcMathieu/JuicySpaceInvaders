using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image uloose;
    public int score = 0;
    public CameraController camera;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver() {
        uloose.gameObject.SetActive(true);
    }

    public void AddScore(int addScore) {
        score += addScore;
    }
}
