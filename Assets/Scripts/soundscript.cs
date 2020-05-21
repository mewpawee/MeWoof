using System.Collections;
using System.Collections.Generic;
using System.Runtime.Versioning;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Soundscript : MonoBehaviour
{
    public static AudioClip steaksound, startsound, submitsound, chopsound, timeoutsound;
    static AudioSource audiosrc;
    // Start is called before the first frame update
    void Start()
    {
        steaksound = Resources.Load<AudioClip>("steaksound");
        startsound = Resources.Load<AudioClip>("start");
        submitsound = Resources.Load<AudioClip>("submit");
        chopsound = Resources.Load<AudioClip>("chopping");
        timeoutsound = Resources.Load<AudioClip>("timeouts");

        audiosrc = GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void Playsound(string clip)
        {
            switch (clip)
            {
                case "steak":
                    audiosrc.PlayOneShot(steaksound);
                    break;
                case "start":
                    audiosrc.PlayOneShot(startsound);
                    break;
                case "submit":
                    audiosrc.PlayOneShot(submitsound);
                    break;
                case "chopping":
                    audiosrc.PlayOneShot(chopsound);
                    break;
                case "timeouts":
                    audiosrc.PlayOneShot(timeoutsound);
                    break;
            }
        }
}
