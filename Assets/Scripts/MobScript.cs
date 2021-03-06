﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobScript : MonoBehaviour
{
    public enum Type {
        SKELETON,
        ZOMBIE,
        GHOST
    }

    public Type mobType;

    public float viewRange = 7;

    public float speed = 2f;

    private GameObject target;

    private Rigidbody2D body2D;

    public float attackInterval = 1f;
    private float attackCounter;
    public float attackRange = 0.5f;
    public int strength = 1;

    // Start is called before the first frame update
    void Start()
    {
        body2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        attackCounter -= Time.deltaTime;

        if (attackCounter < 0)
            attackCounter = 0;
        
        if (attackCounter == 0 && target && Vector3.Distance(transform.position, target.transform.position) <= attackRange)
        {
            HealthScript health = target.GetComponent<HealthScript>();

            if (health)
            {
                health.Hurt(strength);
                attackCounter = attackInterval;
            }

            FlameScript flameScript = target.GetComponent<FlameScript>();

            if (flameScript)
            {
                flameScript.power -= strength;

                if (flameScript.power < 0)
                {
                    flameScript.power = 0;
                }

                Destroy(gameObject);
            }
        }
    }

    void FixedUpdate()
    {
        if (target && Vector3.Distance(transform.position, target.transform.position) > viewRange)
        {
            target = null;
            body2D.velocity = Vector3.zero;
        }

        if (target)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            Vector3 velocity = direction * speed;

            if (Vector3.Distance(transform.position, target.transform.position) > attackRange)
            {
                body2D.MovePosition(Vector3.Lerp(transform.position, target.transform.position, 0.025f));

                // body2D.velocity = velocity;
            }
            // else
            // {
            //     body2D.velocity = Vector3.zero;
            // }
        }

        if (target)
            return;
        
        GroupUnitScript[] units = GameObject.FindObjectsOfType<GroupUnitScript>();

        foreach (GroupUnitScript unit in units)
        {
            if (!unit.isEnrolled)
                continue;
            
            if (mobType == Type.SKELETON && Vector3.Distance(transform.position, unit.transform.position) <= viewRange)
            {
                target = unit.gameObject;
                break;
            }

            if (mobType == Type.ZOMBIE && Vector3.Distance(transform.position, unit.transform.position) <= viewRange && unit.unitType == GroupUnitScript.Type.WARRIOR)
            {
                target = unit.gameObject;
                break;
            }

            if (mobType == Type.GHOST)
            {
                GameObject flammeInstance = GameObject.FindGameObjectWithTag("Flamme");

                if (mobType == Type.GHOST && Vector3.Distance(transform.position, flammeInstance.transform.position) <= viewRange)
                {
                    target = flammeInstance;
                    break;
                }
            }
        }
    }
}
