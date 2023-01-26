using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWhiteboard : MonoBehaviour
{
    public GameObject whiteboard;
    public Material[] whiteboardDesigns;
   
    // Start is called before the first frame update
    void Start()
    {
        whiteboard.GetComponent<Renderer>().material = whiteboardDesigns[Random.Range(0,whiteboardDesigns.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
