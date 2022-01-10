using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorsManager : MonoBehaviour
{
    public float decorSpeed;

    [Header("ROAD")]
    public Transform roadParent;
    public GameObject roadPrefab;
    public Transform roadSpawn;

    //public float spawnDelay;
    //public GameObject[] decors;
    //public Transform[] spawnPoint;
    //public float rangeX;
    //public float rangeY;

    //public float lampSpawnDelay;
    //public GameObject lampadaire;
    //public Transform[] lampSpawns;

    //public GameObject roadBGPrefab;
    //public Transform roadBGSpawn;

    //private float currentTime = 0f;
    //private float lampTime = 0f;

    //// Update is called once per frame
    //void Update()
    //{
    //    SpawnObjectTimer();
    //    //SpawnLamp();
    //}

    //void SpawnObject()
    //{
    //    int randomSpawnPoint = Random.Range(0, 2);
    //    GameObject obj = Instantiate(decors[Random.Range(0, decors.Length)], spawnPoint[randomSpawnPoint], false);
    //    //obj.AddComponent<DecoMovement>();
    //    obj.transform.position = new Vector3(Random.Range(spawnPoint[randomSpawnPoint].position.x - rangeX, spawnPoint[randomSpawnPoint].position.x + rangeX), Random.Range(spawnPoint[randomSpawnPoint].position.y - rangeY, 0f), spawnPoint[randomSpawnPoint].position.z);
    //    currentTime = 0f;
    //}

    //void SpawnObjectTimer()
    //{
    //    currentTime += Time.deltaTime;

    //    if (currentTime > spawnDelay)
    //    {
    //        SpawnObject();
    //    }
    //}

    //void SpawnLamp()
    //{
    //    if (lampTime < lampSpawnDelay)
    //    {
    //        lampTime += Time.deltaTime;
    //    }
    //    else
    //    {
    //        //Debug.Log("instantiate");
    //        GameObject obj = Instantiate(lampadaire, lampSpawns[0], false);
    //        GameObject obj2 = Instantiate(lampadaire, lampSpawns[1], false);
    //        obj.AddComponent<LampMovement>();
    //        obj2.AddComponent<LampMovement>();
    //        obj.transform.Rotate(new Vector3(0, 0, 180));
    //        lampTime = 0f;
    //    }
    //}

    public void SpawnRoad()
    {
        GameObject road = Instantiate(roadPrefab, roadSpawn.position, roadSpawn.rotation, roadParent.transform);
        road.AddComponent<RoadMovement>();
    }

}
