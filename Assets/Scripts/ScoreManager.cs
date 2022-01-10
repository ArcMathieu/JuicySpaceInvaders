using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int currentScore;
    private TMP_Text scoreText;
    public Animator ScoreT;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TMP_Text>();
        ScoreT = GetComponent<Animator>();
        Reset();
    }

    public void Reset()
    {
        currentScore = 0;
    }

    private void HandleScore()
    {
        scoreText.text = "Score: " + currentScore;
    }

    // Update is called once per frame
    void Update()
    {
        HandleScore();
        //if (Input.GetKeyDown(KeyCode.Space))
        //    AddScore(100);
    }

    private void AddScore(int amount)
    {
        ScoreT.SetTrigger("Add");
        currentScore += amount;
    }
}
