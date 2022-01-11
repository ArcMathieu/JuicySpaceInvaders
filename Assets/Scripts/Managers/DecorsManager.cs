using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorsManager : MonoBehaviour
{
    public static float decorSpeed;
    public Transform trashParent;

    [Header("ROAD")]
    public Transform roadParent;
    public GameObject roadPrefab;
    public Transform roadSpawn;

    [Header("Props")]
    public Transform PropsSpawn;

    public List<Transform> prefabsSpawnInstance = new List<Transform>();
    public List<int> prefabsSpawnInstanceID = new List<int>();
    public List<Transform>[] prefabsSpawn = new List<Transform>[0];
    public LandScapeMovement[] prefabs = new LandScapeMovement[0];
    public Vector2[] timers = new Vector2[0];
    public List<LandScapeMovement>[] trash = new List<LandScapeMovement>[0];

    private void Start() {
        for (int i = 0; i < prefabsSpawnInstance.Count; i++) {
            prefabsSpawn[prefabsSpawnInstanceID[i]].Add(prefabsSpawnInstance[i]);
        }
    }
    public void SpawnRoad()
    {
        GameObject road = Instantiate(roadPrefab, roadSpawn.position, roadSpawn.rotation, roadParent.transform);
        road.AddComponent<RoadMovement>();
    }

    private void Update() {
        for (int i = 0; i < timers.Length; i++) {
            timers[i].x += Time.deltaTime;
            if (timers[i].x >= timers[i].y) {
                timers[i].x = 0;
                Spawn(i);
            }
        }
    }

    void Spawn(int listPosition) {
        for (int i = 0; i < prefabsSpawn[listPosition].Count; i++) {
            GameObject decors = NewOrTrash(listPosition);
            Vector3 newPosition = new Vector3(prefabsSpawn[listPosition][i].position.x, prefabsSpawn[listPosition][i].position.y, PropsSpawn.position.z);
            decors.transform.position = newPosition;
            decors.transform.rotation = PropsSpawn.rotation;
        }
    }

    GameObject NewOrTrash(int listPosition) {
        if (trash[listPosition].Count == 0) {
            return Instantiate(prefabs[listPosition].gameObject);
        } else {
            GameObject lastTrash = trash[listPosition][0].gameObject;
            trash[listPosition].RemoveAt(0);
            return lastTrash;
        }
    }

}
