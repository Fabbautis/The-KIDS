using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class AdaptiveAudioTrigger : MonoBehaviour {
    public int triggerLevel;
    private int snapshotToPlay;
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = GetGizmoColor();
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, GetComponent<BoxCollider>().size);        
    }

    private Color GetGizmoColor()
    {
        switch (triggerLevel)
        {
            case 1:
                return Color.blue;
            case 2:
                return Color.green;
            case 3:
                return Color.black;  //default is black

            case 4:
                return Color.yellow;

            case 5:
                return new Color(.4f, .1f, .6f); //purple

            case 6:
                return Color.red;
        }
        return Color.black;
    }

    void OnTriggerEnter(Collider collider)
    {   
        startAudioManager();
    }
    public void startAudioManager(){
        snapshotToPlay = triggerLevel;//We want the emo student to increase the snapshot to play and stay increased if the emo student exists.
        if (GameObject.Find("emo student").GetComponent<TargetMovementAndRespawn>().canBeKilled){
            AdaptiveAudioManager.Instance.AdjustAudioLevel(snapshotToPlay + 2);
        } else if (!GameObject.Find("emo student").GetComponent<TargetMovementAndRespawn>().canBeKilled){
            AdaptiveAudioManager.Instance.AdjustAudioLevel(snapshotToPlay);
        }       
        Debug.Log("New music thing selected");
    }
}
