using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameScript : MonoBehaviour
{
    public float enrollDistance = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GroupUnitScript[] units = GameObject.FindObjectsOfType<GroupUnitScript>();

        foreach (GroupUnitScript unit in units)
        {
            if (unit.isEnrolled)
                continue;
            
            float distance = Vector3.Distance(transform.position, unit.transform.position);

            if (distance <= enrollDistance)
            {
                unit.isEnrolled = true;
                unit.transform.parent = transform.parent;

                Transform nextPosition = null;
                
                if (unit.unitType == GroupUnitScript.Type.TANK)
                {
                    int r = Random.Range(0, FormationManagerScript.instance.tankPositionsAvailables.Count);
                    unit.SetGroupPoint(FormationManagerScript.instance.tankPositionsAvailables[r]);
                    FormationManagerScript.instance.tankPositionsAvailables.RemoveAt(r);

                }
                else if (unit.unitType == GroupUnitScript.Type.WARRIOR)
                {
                    int r = Random.Range(0, FormationManagerScript.instance.tankPositionsAvailables.Count);
                    unit.SetGroupPoint(nextPosition = FormationManagerScript.instance.warriorPositionsAvailables[r]);
                    FormationManagerScript.instance.warriorPositionsAvailables.RemoveAt(r);
                }
            }
        }
    }
}
