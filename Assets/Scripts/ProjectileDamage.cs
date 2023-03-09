using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    public double health = 100.0;
    private bool isDead = false;
    private float timer;
    private float timeSinceDead =0;
    private TargetMovementAndRespawn respawner;

    private void Start()
    {
        respawner = GetComponent<TargetMovementAndRespawn>();
    }   
    private void Update()
    {
        timer = Time.deltaTime;
        if (isDead){
            Debug.Log("" + timer + " " + timeSinceDead + 5.0f);
        }
        if (isDead && timer< timeSinceDead +5.0f){
            respawner.respawnTarget();
        }

    }
     private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Teacher Projectile")){
            Debug.Log(other.name);
            takeDamage(other.gameObject);
            other.gameObject.SetActive(false);
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
        }
        isDead = true;
        timeSinceDead = timer;
        Debug.Log(health);
        if (health <= 0){
            Debug.Log("Just died");
            gameObject.transform.position += new Vector3(0.0f, 100.0f, 0.0f);
        }
    }
}
