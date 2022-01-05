using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitiesManager : MonoBehaviour {
    public Enemy Enemy;
    public PlayerController player;
    public float distBetweenLines = 2;
    public int enemyDirection = 1;
    public int enemyDirectionNumber = 0;
    public int enemyNumber;
    void Start() {
        NewWave(5,3);
    }

    // Update is called once per frame
    void Update() {

    }

    void NewWave(int enemyEachLine, int lines) {
        int direction = Random.Range(0, 2) == 0 ? -1 : 1;
        enemyDirection = direction;
        int randomPosition = Random.Range(0, 2);
        for (int i = 0; i < lines; i++) {
            for (int j = 0; j < enemyEachLine; j++) {
                Enemy newEnemy = Instantiate(Enemy);
                newEnemy.transform.position = LerpPosition(Gino.instance.gameManager.rightBorder, Gino.instance.gameManager.leftBorder, (j + 1) / (enemyEachLine + 1f));
                newEnemy.transform.position = new Vector3(newEnemy.transform.position.x, newEnemy.transform.position.y, newEnemy.transform.position.z + i * distBetweenLines);
                enemyNumber++;
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

    public void NewDirection() {
        if (enemyDirectionNumber == 0) {
            enemyDirection *= -1;
            enemyDirectionNumber = enemyNumber;
        }
    }
}
