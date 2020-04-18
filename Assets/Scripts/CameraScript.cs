using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject group;
    public float zPosition = -10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = new Vector3(group.transform.position.x, group.transform.position.y, zPosition);
        // Vector2 l = Vector2.Lerp(Camera.main.transform.position, GameObject.FindGameObjectWithTag("Flamme").transform.position, 0.02f);
        // Vector3 camPos = new Vector3(
        //     l.x,
        //     l.y,
        //     zPosition
        // );
        // Camera.main.transform.position = camPos;
        transform.position = new Vector3(GameObject.FindGameObjectWithTag("Flamme").transform.position.x, GameObject.FindGameObjectWithTag("Flamme").transform.position.y, zPosition);
    }
}
