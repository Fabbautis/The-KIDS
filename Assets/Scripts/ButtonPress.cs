using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ButtonPress : MonoBehaviour
{
    [SerializeField] private float threshold = 0.1f; //how far does the button need to be pressed to be activated
    [SerializeField] private float deadzone = 0.025f; //Prevent jitteriness
    private bool isPressed;
    private Vector3 startPos;
    private ConfigurableJoint joint;

    public GameObject prefab;
    public UnityEvent onPress, onReleased;
    public AudioClip buttonSound;
    private AudioSource buttonAudio;

    void Start() {
        buttonAudio = GetComponent<AudioSource>();
        startPos = transform.localPosition;
        joint = GetComponent<ConfigurableJoint>();
    }
    
    void Update() {
        if(!isPressed && getValue() + threshold >= 1){
            Pressed();
        }
        if(isPressed && getValue() - threshold <= 0){
            Released();
        }
    }
    private void Pressed() {
        isPressed = true;
        onPress.Invoke();
        buttonAudio.PlayOneShot(buttonSound, 1.0f);
    }

    private void Released () {
        isPressed = false;
        onReleased.Invoke();
    }

    private float getValue() {
        var value = Vector3.Distance(startPos, transform.localPosition) / joint.linearLimit.limit;
        if (Math.Abs(value) < deadzone){
            value = 0;
        }
        return Mathf.Clamp(value, -1f, 1f);
    }
   
}
