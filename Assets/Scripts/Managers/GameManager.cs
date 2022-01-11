using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform rightBorder;
    public Transform leftBorder;
    public Transform frontBorder;
    public Transform backBorder;
    public Transform gameOver;
    public Transform Score;

    //// Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //        LaunchGameOver();
    //}

    //public void LaunchGameOver()
    //{
    //    gameOver.GetComponent<Animator>().SetBool("GO", true);
    //    Score.GetComponent<Animator>().SetTrigger("GameOver");
    //}
}
