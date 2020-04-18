using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneProtectFlameScript : MonoBehaviour
{
    public enum Direction
    {
        LEFT, RIGHT, TOP, BOTTOM
    }


    public int nbTanks = 0;
    public Direction direction;

    // Start is called before the first frame update
    void Start()
    {
        nbTanks = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collider)
    {
        GroupUnitScript unit = collider.gameObject.GetComponent<GroupUnitScript>();
        if (unit != null)
        {
            if(unit.unitType == GroupUnitScript.Type.TANK)
            {
                nbTanks++;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collider)
    {
        GroupUnitScript unit = collider.gameObject.GetComponent<GroupUnitScript>();
        if (unit != null)
        {
            if (unit.unitType == GroupUnitScript.Type.TANK)
            {
                nbTanks--;
            }
        }
    }
}
