using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameScript : MonoBehaviour
{
    public float enrollDistance = 0.5f;

    public float power;

    private WindScript wind;

    // Start is called before the first frame update
    void Start()
    {
        power = 100f;
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

    // Update is called once per frame
    void FixedUpdate()
    {
        if(wind != null)
        {
            // Check Formation
            int minTanks = 5;
            int nbTanks = 0;
            ZoneProtectFlameScript.Direction wildDirection = ZoneProtectFlameScript.Direction.LEFT;

            if (wind.speed.x < 0)
            {
                wildDirection = ZoneProtectFlameScript.Direction.LEFT;
            }
            else if (wind.speed.x > 0)
            {
                wildDirection = ZoneProtectFlameScript.Direction.RIGHT;
            }
            else if (wind.speed.y < 0)
            {
                wildDirection = ZoneProtectFlameScript.Direction.LEFT;
            }
            else if (wind.speed.y > 0)
            {
                wildDirection = ZoneProtectFlameScript.Direction.RIGHT;
            }

            //
            ZoneProtectFlameScript[] zoneProtect = GetComponentsInParent<ZoneProtectFlameScript>();
            for(int i = 0; i < zoneProtect.Length; i++)
            {
                if(zoneProtect[i].direction == wildDirection)
                {
                    nbTanks = zoneProtect[i].nbTanks;
                }
            }

            // 
            if(nbTanks < minTanks)
            {
                power -= wind.force;
            }
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collider)
    {
        WindScript wind = collider.gameObject.GetComponent<WindScript>();
        if (wind != null)
        {
            this.wind = wind;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collider)
    {
        WindScript wind = collider.gameObject.GetComponent<WindScript>();
        if (wind != null)
        {
            this.wind = null;
        }
    }



}
