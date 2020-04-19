using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public GameObject howToPlayPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HowToPlay()
    {
        howToPlayPanel.SetActive(!howToPlayPanel.activeInHierarchy);
    }

    public void PlayGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartRoom");
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
