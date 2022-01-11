using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorsManager : MonoBehaviour
{
    public float decorSpeed;
    public Transform trashParent;

    [Header("ROAD")]
    public Transform roadParent;
    public GameObject roadPrefab;
    public Transform roadSpawn;

    [Header("Props")]
    public Transform PropsSpawn;

    public List<Transform> prefabsSpawnInstance = new List<Transform>();
    public List<int> prefabsSpawnInstanceID = new List<int>();
    public List<Transform>[] prefabsSpawn;
    public LandScapeMovement[] prefabs = new LandScapeMovement[0];
    public Vector2[] timers = new Vector2[0];
    public List<LandScapeMovement>[] trash; 

    private void Start() {
        prefabsSpawn =  new List<Transform>[100];
        for (int i = 0; i < prefabsSpawn.Length; i++) {
            prefabsSpawn[i] = new List<Transform>();
        }
        trash = new List<LandScapeMovement>[100];
        for (int i = 0; i < trash.Length; i++) {
            trash[i] = new List<LandScapeMovement>();
        }
        for (int i = 0; i < prefabsSpawnInstance.Count; i++) {
            Debug.Log(prefabsSpawnInstanceID[i]);
            Debug.Log(prefabsSpawnInstance[i]);
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
            Vector3 newRotation = new Vector3(prefabsSpawn[listPosition][i].rotation.eulerAngles.x + decors.transform.rotation.eulerAngles.x, prefabsSpawn[listPosition][i].rotation.eulerAngles.y + decors.transform.rotation.eulerAngles.y, prefabsSpawn[listPosition][i].rotation.eulerAngles.z + decors.transform.rotation.eulerAngles.z); ;
            decors.transform.rotation = Quaternion.Euler(newRotation);
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
