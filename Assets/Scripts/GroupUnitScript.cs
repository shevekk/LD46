using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupUnitScript : MonoBehaviour
{
    public enum Type
    {
        TANK, WARRIOR
    }

    private Transform groupPoint;

    private float speed = 0.12f;
    private Vector2 acceleration;

    public Type unitType;

    public float viewRange = 9f;

    private GameObject target;

    private bool forceReposition;
    private float forceRepositionTimer;

    private float attackInterval = 0.75f;
    private float attackCounter;
    public float attackRange = 0.5f;

    public bool isEnrolled = false;

    [HideInInspector]
    public float waitMove = 0f;

    private Rigidbody2D body2D;

    // Start is called before the first frame update
    void Start()
    {
        body2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
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
                GetComponent<Animator>().SetTrigger("attack");
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
            GetComponent<Animator>().SetBool("isMoving", false);
        }

        if (target)
        {
            if (Vector3.Distance(transform.position, target.transform.position) > attackRange)
            {
                GetComponent<Animator>().SetBool("isMoving", Vector2.Distance(transform.position, target.transform.position) > 0.35f);
                body2D.MovePosition(Vector3.Lerp(transform.position, target.transform.position, 0.025f));
            }

            // Vector3 targetDirection = (target.transform.position - transform.position).normalized;
            // Vector3 velocity = targetDirection * speed;

            // if (Vector3.Distance(transform.position, target.transform.position) > 0.35f)
            // {
            //     transform.Translate(velocity);
            // }
            // else
            // {
                
            // }
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
        
        if (waitMove == 0)
        {
            GetComponent<Animator>().SetBool("isMoving", Vector2.Distance(transform.position, groupPoint.position) > 0.35f);
            body2D.MovePosition(Vector3.Lerp(transform.position, groupPoint.position, 0.1f));
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
