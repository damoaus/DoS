using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class SoundManger : MonoBehaviour
{

    public AudioClip Sound;

    public AudioClip Notenough;

    private AudioSource Source { get { return GetComponent<AudioSource>(); } }



    public void PlaySound()
    {

        Source.PlayOneShot(Sound, .5f);

    }

    public void TowerBuy(int tower)
    {
        int TowerCost = gameObject.GetComponent<CreateTurret>().Towercost[tower];
        if (Gobal.Gold >= TowerCost)
        {
            Source.PlayOneShot(Sound, .5f);
        }
        else
        {
            Source.PlayOneShot(Notenough, .5f);
        }

    }


    public void FadeSound()
    {
        fadeoutSound();
        StartCoroutine(fadeoutSound());
    }

    IEnumerator fadeoutSound()
    {
        while (Source.volume > 0.01f)
        {
            Source.volume -= 0.8f * Time.deltaTime;
            yield return null;
        }
    }

}
