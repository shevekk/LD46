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

    private float speed = 0.1f;

    private SpriteRenderer spriteRenderer;

    public Type unitType;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.sortingOrder = (int)(transform.position.y * -1000);
    }

    void FixedUpdate()
    {
        if (groupPoint == null)
            return;

        Vector3 direction = (groupPoint.position - transform.position).normalized;
        Vector2 movement = direction * speed;

        transform.Translate(movement);

        if (Vector3.Distance(transform.position, groupPoint.position) <= 0.1f)
        {
            transform.position = groupPoint.position;
        }
    }

    public void SetGroupPoint(Transform point)
    {
        groupPoint = point;
    }


}
