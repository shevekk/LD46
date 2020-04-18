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

    public int nbHealsMin;
    public int nbHealsMax;
    public List<Transform> heal;


    // Start is called before the first frame update
    void Start()
    {
        initElement("UnitPosition", nbUnitMin, nbUnitMax, units);
        initElement("EnnemisPosition", nbEnnemisMin, nbEnnemisMax, ennemis);
        initElement("HealPosition", nbHealsMin, nbHealsMax, heal);
    }

    public void initElement(string tag, int nbElementMin, int nbElementMax, List<Transform> prefabs)
    {
        // Generation Units
        GameObject[] obj = GameObject.FindGameObjectsWithTag(tag);
        int nbElement = Random.Range(nbElementMin, nbElementMax);
        int nbPlacedElement = 0;
        List<int> occupedPositions = new List<int>();

        if(nbElement > obj.Length)
        {
            nbElement = obj.Length;
            Debug.Log("Problème de génération de l'élément " + tag + " nombre trop important d'éléments");
        }

        // Check position valid
        while (nbPlacedElement < nbElement)
        {
            int numPosition = Random.Range(0, obj.Length);

            if (!occupedPositions.Contains(numPosition))
            {
                nbPlacedElement++;

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
