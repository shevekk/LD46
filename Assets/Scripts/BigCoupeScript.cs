using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigCoupeScript : MonoBehaviour
{
    public GameObject offObj;
    public GameObject onObj;

    private float distanceAction = 3f;

    public KeyCode shortcutKey = KeyCode.Keypad0;

    public Text txtLabel;

    public string labelBeforeKey = "Press";
    public string labelAfterKey = "";

    private bool startFadeToEnd;
    private float fadeCounter;

    public GameObject fadePanel;

    // Start is called before the first frame update
    void Start()
    {
        offObj.SetActive(true);
        onObj.SetActive(false);
        fadePanel.SetActive(false);

        txtLabel.text = labelBeforeKey + " " + shortcutKey + " " + labelAfterKey;
        txtLabel.gameObject.SetActive(false);

        GameObject.FindGameObjectWithTag("WindGenerator").SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (startFadeToEnd)
        {
            fadeCounter += Time.deltaTime * 0.25f;

            if (fadeCounter >= 1)
            {
                foreach (GameObject o in FlameScript.instance.dontDestroy)
                {
                    Destroy(o);
                }
                
                UnityEngine.SceneManagement.SceneManager.LoadScene("StoryEnd");
                return;
            }

            fadePanel.GetComponent<Image>().color = new Color(0, 0, 0, fadeCounter);
        }

        if (onObj.activeInHierarchy)
        {
            txtLabel.gameObject.SetActive(false);
            return;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Flamme");

        if (Vector2.Distance(transform.position, player.transform.position) <= distanceAction)
        {
            txtLabel.gameObject.SetActive(true);
        }
        else
        {
            txtLabel.gameObject.SetActive(false);
        }

        if (Vector2.Distance(transform.position, player.transform.position) <= distanceAction && Input.GetKeyDown(shortcutKey))
        {
            offObj.SetActive(false);
            onObj.SetActive(true);
            txtLabel.gameObject.SetActive(false);

            SoundEffectsHelper.Instance.MakeGainLifeSound();

            GroupScript.instance.canInteract = false;

            startFadeToEnd = true;
            fadePanel.SetActive(true);
        }
    }
}
