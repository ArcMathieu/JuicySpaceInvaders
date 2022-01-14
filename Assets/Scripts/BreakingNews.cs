using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BreakingNews : MonoBehaviour {
    public List<string> titles = new List<string>();

    public TMP_Text titleRef;
    public string titleText;
    public TMP_Text newsRef;
    public string newsText;
    public Vector2 changeNewsTimer;
    public int currentNews = 0;

    private void OnEnable() {

    }
    private void Start() {

    }
    private void Update() {
        if (changeNewsTimer.x >= changeNewsTimer.y) {
            changeNewsTimer.x = 0;

            NewTitle();
        } else {
            changeNewsTimer.x += Time.deltaTime;
        }
        UpdateWhite();
    }

    void NewTitle() {
        currentNews++;
        if (currentNews >= titles.Count) {
            currentNews = 0;
        }
        titleText = titles[currentNews];
        newsRef.text = titleText;
    }

    void UpdateWhite() {
        titleText = "There is " + Gino.instance.entitiesManager.enemies.Count + " cars behind the killer";
        titleRef.text = titleText;
    }
}
