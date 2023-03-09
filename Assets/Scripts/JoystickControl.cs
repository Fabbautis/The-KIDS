using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class JoystickControl : MonoBehaviour
{
    //joystick variables
    public Transform topOfJoystick;
    private Vector3 joystickDefaultPosition;

    //joystick movement variables
    private bool moveCamera = false;

    [SerializeField]
    private float ForwardBackward = 0;
    private bool ForwardBackwardBool = false; //if true then forward
    private float ForwardBackwardLimit = 10f;

    [SerializeField]
    private float LeftRight = 0;
    private bool LeftRightBool = false; //if true then right
    private float LeftRightLimit = 20f;

    //camera variables
    public Camera[] cameraArray;
    public List<Vector3> cameraRotationArray = new List<Vector3>();
    public Camera currentCamera;

    //sfx
    private AudioSource joystickAudio;
    
    void Start(){
        joystickDefaultPosition = new Vector3(transform.position.x,transform.position.y,transform.position.z);
        joystickAudio = GetComponent<AudioSource>();
        //Get the default rotation for cameras
        int index = 0;
        foreach (Camera cctvCamera in cameraArray){
            cameraRotationArray.Add(cctvCamera.transform.localRotation.eulerAngles);
            index++;
        }
    }

    void FixedUpdate()
    {
        JoystickMovement();
    }
    
    private void OnTriggerStay(Collider other){
        if (other.CompareTag("Hands")){
            transform.LookAt(other.transform.position, transform.up);
            joystickAudio.loop = true;
            joystickAudio.Play();
        }

    }

    private void OnTriggerExit(Collider other){
        if (other.CompareTag("Hands")){
            transform.rotation = Quaternion.Euler(270, 0, 270);
            moveCamera = false;
            joystickAudio.loop = false;
            joystickAudio.Stop();
        }
    }

    private void rotateCamera(){
        int currentCameraIndex = GameObject.Find("Screen").GetComponent<TeacherComputer>().currentCameraIndex;
        currentCamera = cameraArray[currentCameraIndex];
        
        if (moveCamera)
        {
            //up and down
            if (ForwardBackwardBool){
                currentCamera.transform.Rotate(Vector3.right*Time.deltaTime*3);
            }
            else if (!ForwardBackwardBool) {
                currentCamera.transform.Rotate(-Vector3.right*Time.deltaTime*3);           
            }
            
            
            if (currentCamera.transform.localRotation.eulerAngles.x > (cameraRotationArray[currentCameraIndex].x + ForwardBackwardLimit)){
                currentCamera.transform.localRotation = Quaternion.Euler(cameraRotationArray[currentCameraIndex].x + ForwardBackwardLimit, currentCamera.transform.localRotation.eulerAngles.y, currentCamera.transform.localRotation.eulerAngles.z);
            } 
            if (currentCamera.transform.localRotation.eulerAngles.x < (cameraRotationArray[currentCameraIndex].x)) {
                currentCamera.transform.localRotation = Quaternion.Euler(cameraRotationArray[currentCameraIndex].x, currentCamera.transform.localRotation.eulerAngles.y, currentCamera.transform.localRotation.eulerAngles.z);
            }

             //left and right
            if (LeftRightBool){
                currentCamera.transform.Rotate(Vector3.up*Time.deltaTime*5);
            }
            else if (!LeftRightBool) {
                currentCamera.transform.Rotate(-Vector3.up*Time.deltaTime*5);           
            }
            
            
            if (currentCamera.transform.localRotation.eulerAngles.y > (cameraRotationArray[currentCameraIndex].y + LeftRightLimit)){
                currentCamera.transform.localRotation = Quaternion.Euler(currentCamera.transform.localRotation.eulerAngles.x, cameraRotationArray[currentCameraIndex].y + LeftRightLimit, currentCamera.transform.localRotation.eulerAngles.z);
            } 
            if (currentCamera.transform.localRotation.eulerAngles.y < (cameraRotationArray[currentCameraIndex].y)) {
                currentCamera.transform.localRotation = Quaternion.Euler(currentCamera.transform.localRotation.eulerAngles.x, cameraRotationArray[currentCameraIndex].y, currentCamera.transform.localRotation.eulerAngles.z);
            }
        }   
    }

    void JoystickMovement()
    {
        ForwardBackward = topOfJoystick.rotation.eulerAngles.x;
            if(ForwardBackward < 355 && ForwardBackward > 290)
            {
                ForwardBackward = Math.Abs(ForwardBackward-360);
                ForwardBackwardBool = true;
                moveCamera = true;
            }
            else if (ForwardBackward > 5 && ForwardBackward < 74){
                ForwardBackwardBool = false;
                moveCamera = true;

            }

        LeftRight = topOfJoystick.rotation.eulerAngles.z;
            if(LeftRight < 355 && LeftRight > 290)
            {
                LeftRight = Math.Abs(LeftRight-360);
                LeftRightBool = false;
                moveCamera = true;

            }
            else if (LeftRight > 5 && LeftRight < 74){
                LeftRightBool = true;
                moveCamera = true;
            }
        rotateCamera();
    }

}
