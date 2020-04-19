using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigCoupeScript : MonoBehaviour
{
    public Sprite spFlamme;

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
    /// <param name="collision"></param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        FlameScript flame = collision.gameObject.GetComponent<FlameScript>();
        if (flame != null)
        {
            GetComponent<SpriteRenderer>().sprite = spFlamme;

            // Script victoire

        }
    }

}
