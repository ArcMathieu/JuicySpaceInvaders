using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class subEplode : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(waitToDestroy());
    }
    IEnumerator waitToDestroy()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
