using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindGeneratorScript : MonoBehaviour
{
    public float generationTime = 300;

    public float windForceMax = 0.1f;
    public float windForceMin = 0.05f;

    public float windSpeedMax = 0.8f;
    public float windSpeedMin= 0.4f;

    public float windSizeMax = 60f;
    public float windSizeMin = 20f;
  
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
        Vector2 positionOffSet = new Vector2(50, 40);

        elapsedTimeOverLastGenetion++;

        float windSpeed = Random.Range(windSpeedMin, windSpeedMax);
        float windSize = Random.Range(windSizeMin, windSizeMax);

        if (elapsedTimeOverLastGenetion >= generationTime)
        {
            // Create wind
            elapsedTimeOverLastGenetion = 0;

            int directionNum = Random.Range(0, 4);

            Vector3 windWidth = new Vector3(0, 0, 0);
            Vector3 position = GameObject.FindObjectsOfType<GroupScript>()[0].transform.position;
            Vector2 speed = new Vector2(0, 0);

            //
            var newWindPrefab = Instantiate(windPrefab) as Transform;

            // Initialisation des paramètres selon la direction
            if (directionNum == 0)
            {
                position = new Vector3(position.x + (newWindPrefab.transform.localScale.x/2) + positionOffSet.x, position.y, 0);
                speed = new Vector2(-windSpeed, 0);
                windWidth = new Vector3(windSize, 60, 1);
            }
            else if (directionNum == 1)
            {
                position = new Vector3(position.x - (newWindPrefab.transform.localScale.x / 2) - positionOffSet.x, position.y, 0);
                speed = new Vector2(windSpeed, 0);
                windWidth = new Vector3(windSize, 60, 1);
            }
            else if (directionNum == 2)
            {
                position = new Vector3(position.x, position.y + (newWindPrefab.transform.localScale.y / 2) + positionOffSet.y, 0);
                speed = new Vector2(0, -windSpeed);
                windWidth = new Vector3(100, windSize, 1);
            }
            else if (directionNum == 3)
            {
                position = new Vector3(position.x, position.y - (newWindPrefab.transform.localScale.y / 2) - positionOffSet.y, 0);
                speed = new Vector2(0, windSpeed);
                windWidth = new Vector3(100, windSize, 1);
            }

            // Assign position and scale
            newWindPrefab.position = position;
            newWindPrefab.localScale = windWidth;

            // Création du vent
            WindScript wind = newWindPrefab.GetComponent<WindScript>();
            wind.speed = speed;
            wind.force = Random.Range(windForceMin, windForceMax);
        }
    }

}
