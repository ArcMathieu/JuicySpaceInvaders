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
        //if (Input.GetKeyDown(KeyCode.Space))
        //    LaunchGame();
    }

    public void LaunchGame()
    {
        camManager.cam1.gameObject.GetComponent<Animator>().SetTrigger("Start");
        BarrageAnim.GetComponent<Animator>().SetTrigger("Start");
        StartCoroutine(waitToSpawnPlayer());
    }

    IEnumerator waitToSpawnPlayer()
    {
        yield return new WaitForSeconds(4.2f);
        PlayerPrefs = Instantiate(PlayerPrefs, Vector3.zero, new Quaternion(0,180,0,0), BarrageAnim.transform);
        isStart = true;
    }
    
    public void LaunchGameOver()
    {
        //arreter enemies + decor
        Gino.instance.decorsManager.decorSpeed = Mathf.Lerp(Gino.instance.decorsManager.decorSpeed, 0, 1);
        Gino.instance.entitiesManager.distJump = 0;
        gameOver.GetComponent<Animator>().SetBool("GO", true);
        Score.GetComponent<Animator>().SetTrigger("GameOver");
    }
    public void RestartGame()
    {
        
    }
}
