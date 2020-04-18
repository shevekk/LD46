using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindGeneratorScript : MonoBehaviour
{
    public float generationTime;
    public float windForce;
    public Vector2 windSpeed;
    public Quaternion windRotation;
    public Transform windPrefab;

    private float elapsedTimeOverLastGenetion = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        elapsedTimeOverLastGenetion++;

        if(elapsedTimeOverLastGenetion >= generationTime)
        {
            // Create wind
            elapsedTimeOverLastGenetion = 0;
        }
    }

}
