using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Transform rightBorder;
    public Transform leftBorder;
    public Transform frontBorder;
    public Transform backBorder;
    public Transform gameOver;
    public ScoreManager ScoreManager;
    public CameraManager camManager;
    public GameObject BarrageAnim;
    public PlayerController PlayerPrefs;
    public bool isStart;
    public Animator bkack;
    public Animator Uiback;
    void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        camManager = Gino.instance.cameraManager;
        isPaused = false;
        bkack.Play("BlackFade");
        Uiback.Play("UIEmptyAnim");
        Time.timeScale = 1;
    }
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //    LaunchGame();
    }

    public void AddScoreToUI(int amount)
    {
        ScoreManager.AddScore(amount);
    }
    public bool PoliceMoveForward;
    public void LaunchGame()
    {
        camManager.cam1.gameObject.GetComponent<Animator>().SetTrigger("Start");
        BarrageAnim.GetComponent<Animator>().SetTrigger("Start");
        isPaused = false;
        Time.timeScale = 1;
        StartCoroutine(waitToSpawnPlayer());
        Gino.instance.uiManager.gameObject.GetComponent<BreakingNews>().enabled = true;
    }

    IEnumerator waitToSpawnPlayer()
    {
        yield return new WaitForSeconds(4.4f);
        PlayerPrefs = Instantiate(PlayerPrefs, Vector3.zero, new Quaternion(0,180,0,0), BarrageAnim.transform);
        isStart = true;
        PoliceMoveForward = true;
        StartCoroutine(AYA());
    }
    IEnumerator AYA() {
        yield return new WaitForSeconds(5f);
        Gino.instance.entitiesManager.canShoot = true;
    }
        bool isPaused;
    public void PauseGame()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Gino.instance.decorsManager.decorSpeed = Mathf.Lerp(Gino.instance.decorsManager.decorSpeed, 0, 1);
            Gino.instance.entitiesManager.distJump = 0;
            Gino.instance.entitiesManager.canShoot = false;
            PlayerPrefs.canMove = false;
        }
        else
        {
            StartCoroutine(WaitToUnPause());
        }
    }
    IEnumerator WaitToUnPause()
    {
        yield return new WaitForSeconds(0.5f);
        Gino.instance.decorsManager.decorSpeed = Mathf.Lerp(0, Gino.instance.decorsManager.decorMaxSpeed, 1);
        Gino.instance.entitiesManager.distJump = 2.3f;
        PlayerPrefs.canMove = true;
        Gino.instance.entitiesManager.canShoot = true;
    }
    public void LaunchGameOver()
    {
        //arreter enemies + decor
        Gino.instance.decorsManager.decorSpeed = Mathf.Lerp(Gino.instance.decorsManager.decorSpeed, 0, 1);
        Gino.instance.entitiesManager.distJump = 0;
        gameOver.GetComponent<Animator>().SetBool("GO", true);
        ScoreManager.transform.parent.GetComponent<Animator>().Play("GameOverScore");
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
