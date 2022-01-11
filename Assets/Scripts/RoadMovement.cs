using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

public class RoadMovement : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + DecorsManager.decorSpeed * Time.deltaTime);
        if (transform.position.z > 880f)
        {
            Gino.instance.decorsManager.SpawnRoad();
            Destroy(this.gameObject);
        }
    }
}
