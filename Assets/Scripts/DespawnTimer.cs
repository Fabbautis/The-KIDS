using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DespawnTimer : MonoBehaviour
{
    private bool isInRespawnRoom = false;
    public float timer = 5f;

    private Vector3 position;
    private Quaternion rotation;
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "TeacherRoomRespawn"){
            isInRespawnRoom = true;
        }
        if (isInRespawnRoom){ //get the position and rotation and all of that of the object
            position = gameObject.transform.position;
            rotation = gameObject.transform.rotation;
        }
        else if (!isInRespawnRoom){ //The items should not be disappearing when you load into the place for this room. They should disappear after you drop them
            StartCoroutine(SelfDestruct());
        }
        
    }

    public void StartStopCoroutine(bool state){ //true if you want it to self destruct. False if you want it to still exist
        if (!state){
            StopAllCoroutines();
        }
        if (state){
            StartCoroutine(SelfDestruct());
        }
    }


    public IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(timer);
        if (isInRespawnRoom){
            gameObject.transform.position = position;
            gameObject.transform.rotation = rotation;
        }
        else if (!isInRespawnRoom){
        Destroy(gameObject);
        }
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
