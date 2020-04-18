using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormationButtonScript : MonoBehaviour
{
    public GameObject targetFormation;

    public KeyCode shortcutKey;

    private string defaultText;

    // Start is called before the first frame update
    void Start()
    {
        defaultText = transform.Find("Text").GetComponent<Text>().text;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(shortcutKey))
        {
            ChangeFormation();
        }

        transform.Find("Text").GetComponent<Text>().text = defaultText + " (" + shortcutKey + ")";
    }

    public void ChangeFormation()
    {
        FormationManagerScript.instance.ChangeFormation(targetFormation);
    }
}
