using UnityEngine;
using System.Collections;

public class collider : MonoBehaviour {

    public Material red;
    public Material green;

    public bool canplace = true;
   

    // Is used when placing a Tower if tower collides with another object you can not place it and changes its color
    void OnTriggerStay(Collider other)
    {
        // Shooting tag is the Shooting range of the towers 
        if (other.gameObject.tag == "Shooting")
        {
            return;
        }
        if (gameObject.GetComponent<Renderer>() != null)
        {
            gameObject.GetComponent<Renderer>().material = red;
        }

        foreach (Transform child in gameObject.transform)
        {
            if (child.GetComponent<Renderer>() != null)
            {
                child.GetComponent<Renderer>().material = red;
            }
        }

        canplace = false;
    }
    // when tower  exits the collider it can be placed and changes its colour
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Shooting")
        {
            return;
        }

        foreach (Transform child in gameObject.transform)
        {
            if (child.GetComponent<Renderer>()!= null)
            {
                child.GetComponent<Renderer>().material = green;
            }
        }
        if (gameObject.GetComponent<Renderer>() != null)
        {
            gameObject.GetComponent<Renderer>().material = green;
        }


        canplace = true;

    }
}
    

