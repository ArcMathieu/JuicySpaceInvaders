using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandScapeMovement : MonoBehaviour
{
    public int id;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + Gino.instance.decorsManager.decorSpeed * Time.deltaTime);
    }

    public void GoToTrash(int id) {
        gameObject.SetActive(false);
        transform.parent = Gino.instance.decorsManager.trashParent;
        Gino.instance.decorsManager.trash[id].Add(this);
    }

    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<Trasher>()) {
            GoToTrash(id);
        }
    }
}
