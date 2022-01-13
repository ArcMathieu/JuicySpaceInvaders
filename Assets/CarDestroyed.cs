using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDestroyed : MonoBehaviour
{
    public GameObject explosion;
    private void OnStart() {
        explosion = Instantiate(explosion, transform.parent.position, Quaternion.identity);
    }
}
