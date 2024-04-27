using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource Sound;

    public AudioClip flip, fail, match, win;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
 
    }

    public void OnPlaySound(AudioClip clip)
    {
        Sound.clip = clip;
        Sound.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
