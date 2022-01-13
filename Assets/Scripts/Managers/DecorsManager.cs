using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorsManager : MonoBehaviour
{
    [HideInInspector] public float decorSpeed;
    public float decorMaxSpeed;
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
            prefabsSpawn[prefabsSpawnInstanceID[i]].Add(prefabsSpawnInstance[i]);
        }
        decorSpeed = decorMaxSpeed;
    }
    public void SpawnRoad()
    {
        GameObject road = Instantiate(roadPrefab, roadSpawn.position, roadSpawn.rotation, roadParent.transform);
        road.AddComponent<RoadMovement>();
    }

    private void FixedUpdate() {
        for (int i = 0; i < timers.Length; i++) {
            timers[i].x += Time.fixedDeltaTime;
            if (timers[i].x >= timers[i].y * (100/ decorSpeed)) {
                timers[i].x = 0;
                Spawn(i);
            }
        }
    }

    void Spawn(int listPosition) {
        for (int i = 0; i < prefabsSpawn[listPosition].Count; i++) {
            LandScapeMovement decors = NewOrTrash(listPosition);
            Vector3 newPosition = new Vector3(prefabsSpawn[listPosition][i].position.x, prefabsSpawn[listPosition][i].position.y, PropsSpawn.position.z);
            decors.transform.position = newPosition;
            Vector3 newRotation = new Vector3(prefabsSpawn[listPosition][i].rotation.eulerAngles.x + prefabs[listPosition].transform.rotation.eulerAngles.x, prefabsSpawn[listPosition][i].rotation.eulerAngles.y + prefabs[listPosition].transform.rotation.eulerAngles.y, prefabsSpawn[listPosition][i].rotation.eulerAngles.z + prefabs[listPosition].transform.rotation.eulerAngles.z); ;
            decors.transform.rotation = Quaternion.Euler(newRotation);
            decors.id = listPosition;
        }
    }

    LandScapeMovement NewOrTrash(int listPosition) {
        if (trash[listPosition].Count == 0) {
            return Instantiate(prefabs[listPosition]);
        } else {
            LandScapeMovement lastTrash = trash[listPosition][0];
            trash[listPosition].RemoveAt(0);
            lastTrash.gameObject.SetActive(true);
            return lastTrash;
        }
    }

}
