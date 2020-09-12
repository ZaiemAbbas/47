using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static SoundManagerScript SoundInstance;
    public List<AudioClip> Sounds;
    public static AudioClip sword,boom;
    static AudioSource audiosrc;
    // Start is called before the first frame update
    void Start()
    {
        audiosrc = GetComponent<AudioSource>();
        audiosrc.PlayOneShot(Sounds[0]);
        boom = Sounds[1];
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void PlaySword()
    {
        audiosrc.PlayOneShot(Sounds[0]);
    }
    public void PlayBoom()
    {
        print("I am Here in PlayBoom");
        //audiosrc.PlayOneShot(Sounds[1]);
        audiosrc.PlayOneShot(boom);
    }
}
