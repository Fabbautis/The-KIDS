using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnTimer : MonoBehaviour
{
    public float timer = 5f;
    void Start()
    {
        StartCoroutine(SelfDestruct());
    }

    public void StartStopCoroutine(bool state){ //true if you want it to self destruct. False if you want it to still exist
        if (!state){
            StopAllCoroutines();
        }
        if (state){
            StartCoroutine(SelfDestruct());
        }
    }
    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Hands")){
            StartStopCoroutine(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Hands")){
            StartStopCoroutine(true);
        }
    }

}
