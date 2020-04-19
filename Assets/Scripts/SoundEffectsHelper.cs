using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsHelper : MonoBehaviour
{
    /// <summary>
    /// Singleton
    /// </summary>
    public static SoundEffectsHelper Instance;

    public AudioClip gainLifeSound;
    public AudioClip loseLifeSound;
    public AudioClip damageSound;

    public AudioSource audioData;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of SoundEffectsHelper!");
        }

        //audioData = GetComponent<AudioSource>();
        //audioData.Play(0);

        Instance = this;
    }

    public void MakeGainLifeSound()
    {
        MakeSound(gainLifeSound);
    }

    public void MakeLoseLifeSound()
    {
        MakeSound(gainLifeSound);
    }

    public void MakeDamageSound()
    {
        MakeSound(gainLifeSound);
    }

    /// <summary>
    /// Lance la lecture d'un son
    /// </summary>
    /// <param name="originalClip"></param>
    private void MakeSound(AudioClip originalClip)
    {
        AudioSource.PlayClipAtPoint(originalClip, transform.position);
    }
}
