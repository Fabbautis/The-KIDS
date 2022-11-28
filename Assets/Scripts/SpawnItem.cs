using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    public GameObject[] throwableObjects;
    public void SpawnRandomItem() {
        int index = Random.Range(0,throwableObjects.Length);
        GameObject go = Instantiate(throwableObjects[index], transform.position,Quaternion.identity);
    }
}
