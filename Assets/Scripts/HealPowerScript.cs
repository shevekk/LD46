using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealPowerScript : MonoBehaviour
{
    private float distanceAction = 2f;

    public KeyCode shortcutKey = KeyCode.Keypad0;

    private bool isConsumed;

    public int healAmount = 10;

    public Text label;

    public string labelBeforeKey = "Press";
    public string labelAfterKey = "for use";
    public string labelConsumed = "Consumed";

    // Start is called before the first frame update
    void Start()
    {
        label.text = labelBeforeKey + " " + shortcutKey + " " + labelAfterKey;
        label.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Flamme");

        if (Vector2.Distance(transform.position, player.transform.position) <= distanceAction)
        {
            label.gameObject.SetActive(true);
        }
        else
        {
            label.gameObject.SetActive(false);
        }

        if (isConsumed)
            return;

        if (Vector2.Distance(transform.position, player.transform.position) <= distanceAction && Input.GetKeyDown(shortcutKey))
        {
            FlameScript.instance.power += healAmount;

            if (FlameScript.instance.power > 100)
            {
                FlameScript.instance.power = 100;
            }
            
            isConsumed = true;

            label.text = "Consumed";

            Animator animator = GetComponent<Animator>();

            if (animator)
            {
                animator.SetTrigger("off");
            }

            SoundEffectsHelper.Instance.MakeGainLifeSound();
        }
    }
}
