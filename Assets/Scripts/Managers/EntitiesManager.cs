using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitiesManager : MonoBehaviour {
    public int enemyDirection = 1;
    public float distBetweenLines = 4;
    public bool newDirection = false;

    public PlayerController player;
    public Bullet bullet;
    public EnemyBullet enemybullet;
    [SerializeField] private Enemy Enemy;


    [SerializeField] private float timeBetweenShoot = 1;
    private float timeBetweenShootTimer = 0;

    private float randomFactorX = 2f;
    private float randomFactorZ = 6f;

    public List<Enemy> enemies = new List<Enemy>();
    void Start() {
        NewWave(10,5);
    }

    // Update is called once per frame
    void LateUpdate() {
        NewDirection();
        ShootTimer();
    }

    void NewWave(int enemyEachLine, int lines) {
        int direction = Random.Range(0, 2) == 0 ? -1 : 1;
        enemyDirection = direction;
        int randomPosition = Random.Range(0, 2);
        float distBetweenBorders = Vector3.Distance(Gino.instance.gameManager.rightBorder.transform.position, Gino.instance.gameManager.leftBorder.transform.position);
        for (int i = 0; i < lines; i++) {
            for (int j = 0; j < enemyEachLine; j++) {
                float x = Random.Range(-distBetweenBorders / (enemyEachLine + 1f) / randomFactorX + transform.localScale.x / 2, distBetweenBorders / (enemyEachLine + 1f) / randomFactorX - transform.localScale.x / 2); 
                float z = Random.Range(-distBetweenLines / randomFactorZ + transform.localScale.z / 2, distBetweenLines / randomFactorZ - transform.localScale.z / 2);
                Enemy newEnemy = Instantiate(Enemy);
                newEnemy.transform.position = LerpPosition(Gino.instance.gameManager.rightBorder, Gino.instance.gameManager.leftBorder, (j + 1) / (enemyEachLine + 1f));
                newEnemy.transform.position = new Vector3(newEnemy.transform.position.x + x, newEnemy.transform.position.y, newEnemy.transform.position.z + i * distBetweenLines + z);
                newEnemy.transform.gameObject.name = j + "/" + i ;
                Debug.Log("Name :" + newEnemy.transform.gameObject.name + " V " + new Vector3(newEnemy.transform.position.x + x, newEnemy.transform.position.y, newEnemy.transform.position.z + i * distBetweenLines + z) + " x " + x + " z " + z);
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

    void ShootTimer() {
        if(timeBetweenShootTimer < 0) {
            timeBetweenShootTimer = timeBetweenShoot;
            Shoot();

        } else {
            timeBetweenShootTimer -= Time.deltaTime;
        }
    }

    void Shoot() {
        int Shooter = Random.Range(0,enemies.Count);
        enemies[Shooter].Shoot();
    }
}
