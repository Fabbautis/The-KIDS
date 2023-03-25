using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingSound : MonoBehaviour
{
    public AudioClip clip;
    private AudioSource source;
    private bool canPlayAudio = false;
    private float volume = 0.5f;
    private float minVelocity = 0;
    private float maxVelocity = 4;
    VelocityEstimator estimator;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hands")){
            canPlayAudio = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hands")){
            estimator = other.GetComponent<VelocityEstimator>(); 

            float v = estimator.GetVelocityEstimate().magnitude;
            volume = Mathf.InverseLerp(minVelocity,maxVelocity,v);
            canPlayAudio = false; //make it so the velociry of the hand can determine how loud the throwing sfx sounds later
        }
    }
    public void playThrowingSound(){
        if (canPlayAudio){
          source.PlayOneShot(clip, volume);
        }
    }
}
