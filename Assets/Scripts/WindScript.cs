using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindScript : MonoBehaviour
{
    public Vector2 speed;
    public float force;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    private void FixedUpdate()
    {
        Vector2 movement = Vector2.zero;
        movement.x = speed.x;
        movement.y = speed.y;

        transform.Translate(movement);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        GroupUnitScript group = collision.gameObject.GetComponent<GroupUnitScript>();
        if (group != null)
        {
            Debug.Log("CollisionEnter");

        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collider)
    {
        GroupUnitScript group = collider.gameObject.GetComponent<GroupUnitScript>();
        if (group != null)
        {
            Debug.Log("TriggerEnter");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay2D(Collider2D collider)
    {

    }

    

}
