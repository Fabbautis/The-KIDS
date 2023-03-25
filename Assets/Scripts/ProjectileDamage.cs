using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProjectileDamage : MonoBehaviour
{
    public double health = 1.0;
    private bool isDead = false;
    private float timer;
    private float timeSinceDead =0;
    private TargetMovementAndRespawn respawner;
    private DespawnTimer despawnTimer;
    private bool isInRespawnRoom = false;
    public AudioClip spawnSFX;
    public AudioClip deathSFX;
    private AudioSource playerSourcePlayer;

    private void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "TeacherRoomRespawn"){
            isInRespawnRoom = true;
        }
        respawner = GetComponent<TargetMovementAndRespawn>();
        playerSourcePlayer = GetComponent<AudioSource>();
    }   
    private void Update()
    {
        timer += Time.deltaTime;
        if (isDead){
        }
        if (isDead && timer> (timeSinceDead +5.0f)){
            respawner.respawnTarget();
            playerSourcePlayer.PlayOneShot(spawnSFX, 0.6f);
            isDead =false;
        }

    }
     private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Teacher Projectile")){
            takeDamage(other.gameObject);
            if (isInRespawnRoom){
                despawnTimer = other.gameObject.GetComponent<DespawnTimer>();
                despawnTimer.SelfDestruct();
            }
            else if (!isInRespawnRoom){
                Destroy(other.gameObject);
            }
        }
    }

    private void takeDamage(GameObject item){
        switch (item.name){
            case "cactus":
                health -= 40;
            break; 
            case "red book":  case "blue book":  case "purple book":
                health -= 10;
            break;
            case "lunchbox": 
                health -= 24;
            break;
            case "apple":
                health -= 6;
            break;
            case "Candy Peaches":
                health -= 16;
            break;
            case "juicebox":
                health -= 10;    
            break;
            case "topplewear":
                health -= 20;
            break;
            default:
                health -= 15;
            break;
        }
        if (health <= 0){
            gameObject.transform.position += new Vector3(0.0f, 100.0f, 0.0f);
            isDead = true;
            playerSourcePlayer.PlayOneShot(deathSFX, 1.0f);
            timeSinceDead = timer;
        }
    }
}
