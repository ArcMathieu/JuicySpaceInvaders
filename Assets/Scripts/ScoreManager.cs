using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int currentScore;
    private TMP_Text scoreText;
    public MenuUIAnimController ScoreT;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TMP_Text>();
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
    float ComboTime;
    int trueCombo;
    bool inCombo;
    // Update is called once per frame
    void Update()
    {
        HandleScore();
        ComboTime -= Time.deltaTime;
        if (ComboTime > 0) inCombo = true;
        else
        {
            trueCombo = 0;
            inCombo = false;
        }

    }

    public void AddScore(int amount)
    {
        ScoreT.AddScoreAnim();
        if(inCombo)
            currentScore += amount * 2 + 50*trueCombo;
        else
            currentScore += amount;

        trueCombo++;
        ComboTime = 2;
    }
}
