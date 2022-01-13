using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject uiJuicyParent;
    public Image gameOver;
    public int score = 0;
    //public CameraController camera;
    public Image[] lifes = new Image[0];

    public void GameOver() {
        gameOver.gameObject.SetActive(true);
    }

    public void AddScore(int addScore) {
        score += addScore;
    }

    public void LoseLife(int life) {
        for (int i = lifes.Length - 1; i > life - 1; i--) {
            lifes[i].gameObject.GetComponent<Animator>().SetTrigger("hit");
        }
    }
}
