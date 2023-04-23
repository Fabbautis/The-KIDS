using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovementAndRespawn : MonoBehaviour
{
    //.5 to -6 x
    //30 to 20z

    private float speed = 0.25f;
    public bool canBeKilled = true;
    private AdaptiveAudioTrigger adaptiveAudRespawn;
    void Start(){
        adaptiveAudRespawn = GameObject.Find("Computer Area").GetComponent<AdaptiveAudioTrigger>();
        respawnTarget();
        InvokeRepeating("moveTarget", 0.0f, 0.1f);
    }
    void Update()
    {
        gameObject.transform.LookAt(GameObject.Find("XR Origin").transform, Vector3.up);
    }
    public void respawnTarget(){
        Vector3 newSpot = new Vector3(Random.Range(-6.0f, 0.5f),5.6f,Random.Range(21.5f, 31.0f));
        gameObject.transform.position = newSpot;
        adaptiveAudRespawn.startAudioManager();
    }

    private void moveTarget(){
        var position = gameObject.transform.position;
        gameObject.transform.position = new Vector3((1 * speed) + position.x, position.y, position.z);
        if(gameObject.transform.position.x > 4.25 || gameObject.transform.position.x < -8){
            speed *=-1;
        }  
    }
    private void OnTriggerStay(Collider other)
    {
        canBeKilled = true;
    }
    private void OnTriggerExit(Collider other){
        canBeKilled = false;
        adaptiveAudRespawn.startAudioManager();
        Debug.Log("exited a collider");
    }
}
