using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneratorScript : MonoBehaviour
{
    public Vector2 width;

    public int nbUnitMin;
    public int nbUnitMax;
    public List<Transform> units;

    public int nbEnnemisMin;
    public int nbEnnemisMax;
    public List<Transform> ennemis;


    // Start is called before the first frame update
    void Start()
    {
        initElement("UnitPosition", nbUnitMin, nbUnitMax, units);
        initElement("EnnemisPosition", nbEnnemisMin, nbEnnemisMax, ennemis);
    }

    public void initElement(string tag, int nbElementMin, int nbElementMax, List<Transform> prefabs)
    {
        // Generation Units
        GameObject[] obj = GameObject.FindGameObjectsWithTag(tag);
        int nbUnit = Random.Range(nbElementMin, nbElementMax);
        int nbPlacedUnit = 0;
        List<int> occupedPositions = new List<int>();

        // Check position valid
        while (nbPlacedUnit < nbUnit)
        {
            int numPosition = Random.Range(0, obj.Length);

            if (!occupedPositions.Contains(numPosition))
            {
                nbPlacedUnit++;

                occupedPositions.Add(numPosition);
            }
        }

        // Create Units
        for (int i = 0; i < occupedPositions.Count; i++)
        {
            int numTypePrefab = Random.Range(0, prefabs.Count);

            var newWindPrefab = Instantiate(prefabs[numTypePrefab]) as Transform;
            newWindPrefab.position = new Vector3(obj[occupedPositions[i]].transform.position.x, obj[occupedPositions[i]].transform.position.y, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
