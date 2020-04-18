using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupUnitScript : MonoBehaviour
{
    public enum Type {
        TANK, WARRIOR
    }

    private Transform groupPoint;

    private float speed = 0.1f;

    private SpriteRenderer spriteRenderer;

    public Type unitType;

    public float viewRange = 9f;

    private GameObject target;

    private bool forceReposition;
    private float forceRepositionTimer;

    private float attackInterval = 0.75f;
    private float attackCounter;
    public float attackRange = 0.5f;

    [HideInInspector]
    public bool isEnrolled = false;

    [HideInInspector]
    public float waitMove = 0f;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.sortingOrder = (int)(transform.position.y * -1000);

        if (!isEnrolled)
            return;

        forceRepositionTimer -= Time.deltaTime;

        if (forceRepositionTimer <= 0)
        {
            forceRepositionTimer = 0;
            forceReposition = false;
        }

        // Mouvement
        waitMove -= Time.deltaTime;

        if (waitMove < 0)
        {
            waitMove = 0;
        }

        // Attaque
        attackCounter -= Time.deltaTime;

        if (attackCounter < 0)
            attackCounter = 0;
        
        if (attackCounter == 0 && target && Vector3.Distance(transform.position, target.transform.position) <= attackRange)
        {
            HealthScript health = target.GetComponent<HealthScript>();

            if (health)
            {
                health.Hurt(1);
                attackCounter = attackInterval;
            }
        }
    }
    
    void FixedUpdate()
    {
        if (!isEnrolled)
            return;
        
        // Target
        if (target && Vector3.Distance(transform.position, target.transform.position) > viewRange)
        {
            target = null;
        }

        if (target)
        {
            Vector3 targetDirection = (target.transform.position - transform.position).normalized;
            Vector3 velocity = targetDirection * speed;

            if (Vector3.Distance(transform.position, target.transform.position) > 0.35f)
            {
                transform.Translate(velocity);
            }
            else
            {
                
            }
        }

        if (!target && unitType == Type.WARRIOR && !forceReposition)
        {
            MobScript[] mobs = GameObject.FindObjectsOfType<MobScript>();

            foreach (MobScript mob in mobs)
            {
                if (Vector3.Distance(transform.position, mob.transform.position) <= viewRange)
                {
                    target = mob.gameObject;
                }
            }
        }   

        // Formation
        if (groupPoint == null || target)
            return;
        
        Vector3 direction = (groupPoint.position - transform.position).normalized;
        Vector2 movement = direction * speed;

        float lastDist = Vector3.Distance(transform.position, groupPoint.position);

        if (waitMove == 0)
        {
            transform.Translate(movement);
        }

        if (Vector3.Distance(transform.position, groupPoint.position) >= lastDist && waitMove == 0)
        {
            transform.position = groupPoint.position;
        }
    }

    public void SetGroupPoint(Transform point)
    {
        if (!isEnrolled)
            return;
        
        groupPoint = point;

        forceReposition = true;
        forceRepositionTimer = 3f;
        target = null;
    }
}
