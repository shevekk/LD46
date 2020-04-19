using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextRoomScript : MonoBehaviour
{
    public Text text;
    public string labelUnit = "Vers des unités";
    public string labelHeal = "Vers une fontaine";

    public int type = 0;

    public string healScene;
    public string unitScene;

    // Start is called before the first frame update
    void Start()
    {
        type = Random.Range(0, 2);

        if (type == 0)
        {
            text.text = labelUnit;
        }
        else
        {
            text.text = labelHeal;
        }

        text.gameObject.SetActive(false);
    }

    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Flamme");

        if (Vector2.Distance(transform.position, player.transform.position) <= 5f)
        {
            text.gameObject.SetActive(true);
        }
        else
        {
            text.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Flamme")
        {
            string nextScene = (type == 0) ? unitScene : healScene;
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
            other.gameObject.transform.position = GameObject.Find("StartPosition").transform.position;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Flamme")
        {
            string nextScene = (type == 0) ? unitScene : healScene;
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
            //other.gameObject.transform.position = GameObject.Find("StartPosition").transform.position;
            
            foreach (Transform c in other.transform.parent.transform)
            {
                c.position = GameObject.Find("StartPosition").transform.position;
            }
        }
    }
}
