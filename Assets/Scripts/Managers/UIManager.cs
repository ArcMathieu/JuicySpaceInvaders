using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image gameOver;
    public int score = 0;
    public CameraController camera;
    public Image[] lifes = new Image[0];
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver() {
        gameOver.gameObject.SetActive(true);
    }

    public void AddScore(int addScore) {
        score += addScore;
    }

    public void LoseLife(int life) {
        for (int i = lifes.Length - 1; i > life - 1; i--) {
            lifes[i].gameObject.SetActive(false);
        }
    }
}
