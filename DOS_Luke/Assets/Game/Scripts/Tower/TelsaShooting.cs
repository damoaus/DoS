using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TelsaShooting : MonoBehaviour
{


    GameObject[] enemy;
    GameObject shellInstance;
    public Transform TFire;
    public GameObject bolts;
    public bool CamShot = false;
    List<GameObject> Cubes = new List<GameObject>();
    List<GameObject> Bolts = new List<GameObject>();
    public float power;
    


    void Update()
    {
        
        if (Cubes != null)
        {
            for (int i = 0; i < Cubes.Count; i++)
            {
                if (Cubes[i] == null)
                {
                    Cubes.RemoveAt(0);
                    Destroy(Bolts[0]);
                    Bolts.RemoveAt(0);
                   
                }
                Cubes[i].gameObject.GetComponent<Health>().hit(power);
                i++;
            }
        }
       




    }

    private void OnTriggerEnter(Collider other)
    {
      
        if (other.gameObject.tag == "Enemy")
        {
            // sets enemy
           
                Cubes.Add(other.gameObject);

            //Allows tower to shoot
            CamShot = true;

            if (CamShot == true)
            {
                Fire();
               // CamShot = false;
            }
            else
            {
                CamShot = false;
            }


        }
    }
    private void OnTriggerExit(Collider other)
    {
        // if the gameobject leaving is the enemy 
        if (other.gameObject.tag == "Enemy")
        {
            Cubes.RemoveAt(0);
            Destroy(Bolts[0]);
            Bolts.RemoveAt(0);

          

        }
        CamShot = false;
    }

    private void Fire()
    {
        
        shellInstance = (GameObject)Instantiate(bolts, TFire.position, TFire.rotation);
       
        Bolts.Add(shellInstance);
        

        DigitalRuby.LightningBolt.LightningBoltScript bolt = shellInstance.GetComponent<DigitalRuby.LightningBolt.LightningBoltScript>();
       

        bolt.StartObject = TFire.gameObject;
        
        for (int i = 0; i < Cubes.Count; i++)
        {
            bolt.EndObject = Cubes[i];
           
        }
        }
        
}
