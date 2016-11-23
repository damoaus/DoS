using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class How_to_play : MonoBehaviour
{


    public Text MovingText;
    //Story
    // string[] Dialogue;

    [System.Serializable]
    public struct Dialogue
    {
        public string[] Nsphere;

    }
     public Dialogue TalkingPoint;
    
    int currentlyDisplayingText = 0;

    void Awake()
    {
        StartCoroutine(AnimateText());
    }

    //This is a function for a button you press to skip to the next text
    public void SkipToNextText()
    {
        StopAllCoroutines();
       

        if (currentlyDisplayingText > TalkingPoint.Nsphere.Length)
        {
            //stop writing things
        }
        StartCoroutine(AnimateText());
        currentlyDisplayingText++;
    }
    //Waitforsecconds is speed of writting
    IEnumerator AnimateText()
    {

        for (int i = 0; i < (TalkingPoint.Nsphere[currentlyDisplayingText].Length + 1); i++)
        {
            MovingText.text = TalkingPoint.Nsphere[currentlyDisplayingText].Substring(0, i);
            yield return new WaitForSeconds(.03f);
        }
    }








    // Use this for initialization
    void WriteText()
    {
        StartCoroutine(AnimateText());
        
    }
    
    // Update is called once per frame
}
