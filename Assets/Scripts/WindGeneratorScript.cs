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

    /// <summary>
    /// Différentiel de position lors de la génération (plus grand = plus eloigner du joueur)
    /// </summary>
    public float positionOffSet = 200;

    public Transform windPrefab;

    public GameObject leftIndicatorUI;
    public GameObject rightIndicatorUI;
    public GameObject topIndicatorUI;
    public GameObject bottomIndicatorUI;

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

        float windSpeed = Random.Range(windSpeedMin, windSpeedMax);
        float windSize = Random.Range(windSizeMin, windSizeMax);

        if (elapsedTimeOverLastGenetion >= generationTime)
        {
            // Create wind
            elapsedTimeOverLastGenetion = 0;

            int directionNum = Random.Range(0, 4);

            Vector3 windWidth = new Vector3(0, 0, 0);
            Vector3 position = GameObject.FindObjectsOfType<FlameScript>()[0].transform.position;
            Vector2 speed = new Vector2(0, 0);

            //
            var newWindPrefab = Instantiate(windPrefab) as Transform;

            GameObject indicatorUI = null;

            // Initialisation des paramètres selon la direction
            if (directionNum == 0)
            {
                position = new Vector3(position.x + (windSize/2) + positionOffSet, position.y, 0);
                speed = new Vector2(-windSpeed, 0);
                windWidth = new Vector3(windSize, 60, 1);

                foreach (Transform anim in newWindPrefab.transform)
                {
                    anim.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                    anim.localScale = new Vector3(0.01f, -0.02f, 1);
                }

                indicatorUI = rightIndicatorUI;
            }
            else if (directionNum == 1)
            {
                position = new Vector3(position.x - (windSize / 2) - positionOffSet, position.y, 0);
                speed = new Vector2(windSpeed, 0);
                windWidth = new Vector3(windSize, 60, 1);

                indicatorUI = leftIndicatorUI;
            }
            else if (directionNum == 2)
            {
                position = new Vector3(position.x, position.y + (windSize / 2) + positionOffSet, 0);
                speed = new Vector2(0, -windSpeed);
                windWidth = new Vector3(100, windSize, 1);

                foreach (Transform anim in newWindPrefab.transform)
                {
                    anim.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
                    anim.localScale = new Vector3(0.02f, 0.01f, 1);
                }

                indicatorUI = topIndicatorUI;
            }
            else if (directionNum == 3)
            {
                position = new Vector3(position.x, position.y - (windSize / 2) - positionOffSet, 0);
                speed = new Vector2(0, windSpeed);
                windWidth = new Vector3(100, windSize, 1);
                
                foreach (Transform anim in newWindPrefab.transform)
                {
                    anim.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                    anim.localScale = new Vector3(0.02f, 0.01f, 1);
                }

                indicatorUI = bottomIndicatorUI;
            }

            // Assign position and scale
            newWindPrefab.position = position;
            newWindPrefab.localScale = windWidth;

            // Création du vent
            WindScript wind = newWindPrefab.GetComponent<WindScript>();
            wind.speed = speed;
            wind.force = Random.Range(windForceMin, windForceMax);
            wind.indicatorUI = indicatorUI;

            indicatorUI.GetComponent<UnityEngine.UI.Image>().color = Color.white;

        }
    }

}
