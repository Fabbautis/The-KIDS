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

    void Start() {
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


 /*/public void SpawnObjectRandomSize(int numbers) {
        for (int i = 0; i < numbers; i++)
        {
            GameObject go = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            Vector3 newScale = go.transform.localScale;
            Vector3 randomDeform = Random.insideUnitCircle * 10;
            Debug.Log(randomDeform);
            go.transform.localScale += randomDeform;
            go.GetComponent<Renderer>().material.color = Random.ColorHSV();
        }
    }/*/