﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public void Replay()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MapGenerator");
    }

}
