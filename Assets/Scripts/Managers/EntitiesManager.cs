using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitiesManager : MonoBehaviour {
    public int enemyDirection = 1;
    public float distJump = 0;
    public bool newDirection = false;
    public int enemiesX;
    public int enemiesY;

    public PlayerController player;
    public Bullet bullet;
    public EnemyBullet enemybullet;
    [SerializeField] private Enemy Enemy;


    [SerializeField] private float timeBetweenShoot = 1;
    private float timeBetweenShootTimer = 0;

    private float randomFactorX = 2f;
    private float randomFactorZ = 6f;

    public Vector2 enemyHitCameraShake = Vector2.zero;

    public List<Enemy> enemies = new List<Enemy>();
    void Start() {
        NewWave(enemiesX,enemiesY);
    }

    // Update is called once per frame
    void LateUpdate() {
        NewDirection();
        ShootTimer();
        if (GameManager.instance.isStart && !alreadyStarted) StartCoroutine(moveFastForward());
    }

    void NewWave(int enemyEachLine, int lines) {
        int direction = Random.Range(0, 2) == 0 ? -1 : 1;
        enemyDirection = direction;
        int randomPosition = Random.Range(0, 2);
        float distBetweenLeftRightBorders = Vector3.Distance(Gino.instance.gameManager.rightBorder.transform.position, Gino.instance.gameManager.leftBorder.transform.position);
        float distBetweenFrontBackBorders = Vector3.Distance(Gino.instance.gameManager.frontBorder.transform.position, Gino.instance.gameManager.backBorder.transform.position);
        for (int i = 0; i < lines; i++) {
            for (int j = 0; j < enemyEachLine; j++) {
                float x = Random.Range(-distBetweenLeftRightBorders / (enemyEachLine + 1f) / randomFactorX + transform.localScale.x / 2, distBetweenLeftRightBorders / (enemyEachLine + 1f) / randomFactorX - transform.localScale.x / 2); 
                float z = Random.Range(-distBetweenFrontBackBorders / (enemyEachLine + 1f) / randomFactorZ + transform.localScale.x / 2, distBetweenFrontBackBorders / (enemyEachLine + 1f) / randomFactorZ - transform.localScale.x / 2);
                Enemy newEnemy = Instantiate(Enemy);
                newEnemy.transform.position = LerpPosition(Gino.instance.gameManager.rightBorder, Gino.instance.gameManager.leftBorder, (j + 1) / (enemyEachLine + 1f));
                newEnemy.transform.position = new Vector3(newEnemy.transform.position.x + x, newEnemy.transform.position.y, newEnemy.transform.position.z + i * distBetweenFrontBackBorders / lines + z);
                newEnemy.transform.gameObject.name = j + "/" + i ;
                enemies.Add(newEnemy);
            }
        }
    }

    Vector3 LerpPosition(Transform first, Transform second, float Between) {
        Vector3 final = Vector3.zero;
        final.x = Mathf.Lerp(first.position.x, second.position.x, Between);
        final.y = Mathf.Lerp(first.position.y, second.position.y, Between);
        final.z = Mathf.Lerp(first.position.z, second.position.z, Between);
        return final;
    }

    [SerializeField] private void NewDirection() {
        if (newDirection) {
            enemyDirection *= -1;
            newDirection = false;
        }
    }
    bool isGameStart;
    bool alreadyStarted;

    IEnumerator moveFastForward()
    {
        alreadyStarted = true;
        distJump = 20;
        yield return new WaitForSeconds(4);
        distJump = 2.3f;
        isGameStart = true;
    }
    void ShootTimer() {
        if (isGameStart)
        {
            if (timeBetweenShootTimer < 0)
            {
                timeBetweenShootTimer = timeBetweenShoot;
                Shoot();

            }
            else
            {
                timeBetweenShootTimer -= Time.deltaTime;
            }
        }
        
    }

    void Shoot() {
        int Shooter = Random.Range(0,enemies.Count);
        enemies[Shooter].Shoot();
    }
}
