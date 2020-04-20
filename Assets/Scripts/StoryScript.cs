using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryScript : MonoBehaviour
{
    public Text[] textes;
    public int currentTextID;

    private bool makeVisible = true;
    public float fadeCounter = 2f;
    public float visibleTimer = 6f;
    public float invisibleTimer = 2f;
    public float alphaSpeed = 0.35f;

    public string nextScene;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Text text in textes)
        {
            text.color = new Color(255, 255, 255, 0);
            text.gameObject.SetActive(true);
        }

        currentTextID = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Text text = textes[currentTextID];
        
        fadeCounter -= Time.deltaTime;

        if (fadeCounter <= 0)
        {
            float alpha = text.color.a;

            if (makeVisible)
            {
                alpha += alphaSpeed * Time.deltaTime;

                if (alpha >= 1)
                {
                    alpha = 1;
                    makeVisible = !makeVisible;
                    fadeCounter = visibleTimer;
                }

                text.color = new Color(1, 1, 1, alpha);
            }
            else
            {
                alpha -= alphaSpeed * Time.deltaTime;

                if (alpha <= 0)
                {
                    alpha = 0;
                    makeVisible = !makeVisible;
                    fadeCounter = invisibleTimer;

                    currentTextID ++;

                    if (currentTextID >= textes.Length)
                    {
                        UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
                        return;
                    }
                }

                text.color = new Color(1, 1, 1, alpha);
            }
        }
    }
}
