using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public bool loadSpecific = false;
    public string sceneName = " ";
    // Start is called before the first frame update
    public void NextScene() {

        if (!loadSpecific){
            float random = Random.Range(0,2);
            float room = Mathf.Round(random);
            if (room == 0){
                SceneManager.LoadSceneAsync("TeacherRoomButton");
            }
            if (room == 1){
                SceneManager.LoadSceneAsync("TeacherRoomRespawn");
            }

        }
        else {
            SceneManager.LoadSceneAsync(sceneName);
        }
        
    }
}
