using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlameScript : MonoBehaviour
{
    public static FlameScript instance;

    public float enrollDistance = 0.5f;
    public float power = 100f;
    
    public int minUnitProtect = 5;
    [HideInInspector]
    public WindScript wind;

    public Slider powerSlider;
    public Text powerText;

    public GameObject[] dontDestroy;

    public GameObject windGenerator;

    public int maxWarrior = 10;
    public int maxTanks = 10;

    private int nbWarriors;
    private int nbTanks;

    public int endLevelIn = 5;

    void Awake()
    {
        instance = this;

        foreach (GameObject g in dontDestroy)
        {
            DontDestroyOnLoad(g);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        power = 100f;
        powerText.text = ((int) power) + "%";
    }

    // Update is called once per frame
    void Update()
    {
        if (power <= 0)
        {
            foreach (GameObject o in FlameScript.instance.dontDestroy)
            {
                Destroy(o);
            }
            
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
            
            return;
        }

        GroupUnitScript[] unitsEnrolled = GameObject.FindObjectsOfType<GroupUnitScript>();

        nbTanks = 0;
        nbWarriors = 0;
        
        foreach (GroupUnitScript c in unitsEnrolled)
        {
            if (c.isEnrolled)
            {
                if (c.unitType == GroupUnitScript.Type.TANK)
                {
                    nbTanks ++;
                }
                else if (c.unitType == GroupUnitScript.Type.WARRIOR)
                {
                    nbWarriors ++;
                }
            }
        }

        powerSlider.value = power / 100f;
        powerText.text = ((int) power) + "%";

        GroupUnitScript[] units = GameObject.FindObjectsOfType<GroupUnitScript>();

        foreach (GroupUnitScript unit in units)
        {
            if (unit.isEnrolled)
                continue;

            float distance = Vector3.Distance(transform.position, unit.transform.position);

            if (distance <= enrollDistance)
            {
                if ((unit.unitType == GroupUnitScript.Type.TANK && nbTanks < maxTanks) ||
                (unit.unitType == GroupUnitScript.Type.WARRIOR && nbWarriors < maxWarrior))
                {
                    unit.isEnrolled = true;
                    unit.transform.parent = transform.parent;

                    Transform nextPosition = null;

                    if (unit.unitType == GroupUnitScript.Type.TANK)
                    {
                        int r = Random.Range(0, FormationManagerScript.instance.tankPositionsAvailables.Count);
                        unit.SetGroupPoint(FormationManagerScript.instance.tankPositionsAvailables[r]);
                        FormationManagerScript.instance.tankPositionsAvailables.RemoveAt(r);
                        nbTanks ++;

                    }
                    else if (unit.unitType == GroupUnitScript.Type.WARRIOR)
                    {
                        int r = Random.Range(0, FormationManagerScript.instance.tankPositionsAvailables.Count);
                        unit.SetGroupPoint(nextPosition = FormationManagerScript.instance.warriorPositionsAvailables[r]);
                        FormationManagerScript.instance.warriorPositionsAvailables.RemoveAt(r);
                        nbWarriors ++;
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(wind != null)
        {
            // Check If the Flame is protect
            int nbTanks = 0;
            ZoneProtectFlameScript.Direction wildDirection = ZoneProtectFlameScript.Direction.LEFT;

            if (wind.speed.x < 0)
            {
                wildDirection = ZoneProtectFlameScript.Direction.RIGHT;
            }
            else if (wind.speed.x > 0)
            {
                wildDirection = ZoneProtectFlameScript.Direction.LEFT;
            }
            else if (wind.speed.y < 0)
            {
                wildDirection = ZoneProtectFlameScript.Direction.TOP;
            }
            else if (wind.speed.y > 0)
            {
                wildDirection = ZoneProtectFlameScript.Direction.BOTTOM;
            }

            // Verifie si des tanks sont présent dans la zone de protection
            ZoneProtectFlameScript[] zoneProtect = GetComponentsInChildren<ZoneProtectFlameScript>();
            for(int i = 0; i < zoneProtect.Length; i++)
            {
                if(zoneProtect[i].direction == wildDirection)
                {
                    nbTanks = zoneProtect[i].nbTanks;
                }
            }

            // Si pas asser de tank, perte de vie
            if (nbTanks < minUnitProtect)
            {
                power -= wind.force;
                SoundEffectsHelper.Instance.MakeLoseLifeSound();
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
        if (this.wind != null)
        {
            if (this.wind.indicatorUI != null)
            {
                this.wind.indicatorUI.GetComponent<UnityEngine.UI.Image>().color = new Color(0, 0, 0, 0);
            }
            this.wind = null;
        }
    }



}
