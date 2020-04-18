using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupScript : MonoBehaviour
{
    public float speed = 0.1f;

    private float inputX;
    private float inputY;

    private Vector2 lastDirection;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
    }

    /// <summary>
    /// 
    /// </summary>
    private void FixedUpdate()
    {
        // Vector2 movement = Vector2.zero;
        // movement.x = inputX * speed;
        // movement.y = inputY * speed;

        // transform.Translate(movement);

        Vector2 direction = new Vector2(
            inputX,
            inputY
        ).normalized;

        if (lastDirection != direction && lastDirection == Vector2.zero)
        {
            GroupUnitScript[] units = GameObject.FindObjectsOfType<GroupUnitScript>();

            foreach (GroupUnitScript unit in units)
            {
                if (!unit.isEnrolled)
                    continue;
                
                unit.waitMove = Random.Range(0.05f, 0.55f);
            }
        }

        GameObject.FindGameObjectWithTag("Flamme").transform.Translate(direction * speed);
        GameObject.FindGameObjectWithTag("Group").transform.Find("Group Positions").position = Vector2.Lerp(transform.position, GameObject.FindGameObjectWithTag("Flamme").transform.position, 1f);

        lastDirection = direction;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter2D(Collision2D collision)
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collider)
    {

    }
}
