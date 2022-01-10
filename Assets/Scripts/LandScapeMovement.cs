using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandScapeMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - Gino.instance.decorManager.decorSpeed * Time.deltaTime);
        if (transform.position.z < -20f)
        {
            Gino.instance.decorManager.SpawnRoad();
            Destroy(this.gameObject);
        }
    }
}
