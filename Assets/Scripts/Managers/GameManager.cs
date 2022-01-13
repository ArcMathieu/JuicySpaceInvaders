using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Transform rightBorder;
    public Transform leftBorder;
    public Transform frontBorder;
    public Transform backBorder;
    public Transform gameOver;
    public Transform Score;
    public CameraManager camManager;
    public GameObject BarrageAnim;
    public PlayerController PlayerPrefs;
    public bool isStart;
    void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        camManager = Gino.instance.cameraManager;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            LaunchGame();
    }

    public void LaunchGame()
    {
        camManager.cam1.gameObject.GetComponent<Animator>().SetTrigger("Start");
        BarrageAnim.GetComponent<Animator>().SetTrigger("Start");
        StartCoroutine(waitToSpawnPlayer());
    }

    IEnumerator waitToSpawnPlayer()
    {
        yield return new WaitForSeconds(4);
        Instantiate(PlayerPrefs, Vector3.zero, Quaternion.identity, BarrageAnim.transform);
    }
    
    public void LaunchGameOver()
    {
        //arreter enemies + decor
        gameOver.GetComponent<Animator>().SetBool("GO", true);
        Score.GetComponent<Animator>().SetTrigger("GameOver");
    }
    public void RestartGame()
    {
        
    }
}
