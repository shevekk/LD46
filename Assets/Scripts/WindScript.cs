using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindScript : MonoBehaviour
{
    public Vector2 speed;
    public float force;
    public float duration; // Temps avant supression
    public GameObject indicatorUI;

    private AudioSource windSound;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, duration);

        windSound = GetComponent<AudioSource>();
        windSound.Play();
        windSound.spatialize = true;
        windSound.volume = 0.5f;
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

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collider)
    {
        //indicatorUI.GetComponent<UnityEngine.UI.Image>().color = new Color(0, 0, 0, 0);
        FlameScript flame = collider.gameObject.GetComponent<FlameScript>();
        if (flame != null)
        {
            windSound.volume = 1f;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay2D(Collider2D collider)
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collider)
    {
        FlameScript flame = collider.gameObject.GetComponent<FlameScript>();
        if (flame != null)
        {
            //windSound.velocityUpdateMode = AudioVelocityUpdateMode.
            windSound.volume = 0.3f;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void OnDestroy()
    {
        windSound.Stop();
    }


}