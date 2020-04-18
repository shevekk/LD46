using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormationManagerScript : MonoBehaviour
{
    public static FormationManagerScript instance;

    public GameObject activeGroup;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeFormation(activeGroup);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeFormation(GameObject targetFormation)
    {
        activeGroup = targetFormation;

        List<Transform> tankPositions = new List<Transform>();
        List<Transform> warriorPositions = new List<Transform>();

        foreach (Transform child in activeGroup.transform.Find("Tank").transform)
        {
            tankPositions.Add(child);
        }

        foreach (Transform child in activeGroup.transform.Find("Warrior").transform)
        {
            warriorPositions.Add(child);
        }

        GroupUnitScript[] units = GameObject.FindObjectsOfType<GroupUnitScript>();

        foreach (GroupUnitScript unit in units)
        {
            if (unit.unitType == GroupUnitScript.Type.TANK && tankPositions.Count == 0)
                break;
            if (unit.unitType == GroupUnitScript.Type.WARRIOR && warriorPositions.Count == 0)
                break;
            
            Transform randomPoint = null;

            if (unit.unitType == GroupUnitScript.Type.TANK)
            {
                randomPoint = tankPositions[Random.Range(0, tankPositions.Count)];
                unit.SetGroupPoint(randomPoint);
                tankPositions.Remove(randomPoint);
            }
            else if (unit.unitType == GroupUnitScript.Type.WARRIOR)
            {
                randomPoint = warriorPositions[Random.Range(0, warriorPositions.Count)];
                unit.SetGroupPoint(randomPoint);
                warriorPositions.Remove(randomPoint);
            }
        }
    }
}
