using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherComputer : MonoBehaviour
{
    public GameObject computerScreen;
    public RenderTexture[] renderTextures;
    private Renderer rend;
    public int currentCameraIndex;

    // Start is called before the first frame update
    void Start()
    {
        rend = computerScreen.GetComponent<Renderer>();
        //.material = renderTextures[Random.Range(0,renderTextures.Length)];
        currentCameraIndex = Random.Range(0,renderTextures.Length);
        rend.material.SetTexture("_MainTex", renderTextures[currentCameraIndex], default);
    }

    public void changeCameras()
    {

        currentCameraIndex++;
        if(currentCameraIndex > renderTextures.Length){
            currentCameraIndex = 0;
        }
        rend.material.SetTexture("_MainTex", renderTextures[currentCameraIndex], default);
        //put camera rotation to default position
        Camera currentCamera = GameObject.Find("Joystick").GetComponent<JoystickControl>().cameraArray[currentCameraIndex];
        Vector3 currentCameraRotation = GameObject.Find("Joystick").GetComponent<JoystickControl>().cameraRotationArray[currentCameraIndex];
        currentCamera.transform.localRotation = Quaternion.Euler(currentCameraRotation);
    }
}
