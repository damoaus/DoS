using UnityEngine;
using System.Collections;

public class Pathfollowert : MonoBehaviour
{

    
    Transform[] path;
  
    GameObject Manager;
    public float speed = 0.1f;
    public float reachDistance = 1.0f;

    public int currentPoint = 0;
   

    // used to add the health to an object
    //  Vector3 dir;

    void Start()
    {
       ;
        //find gamemanger
        Manager = GameObject.FindGameObjectWithTag("MrManager");
        // starting direction
        int pathlength = Manager.GetComponent<Gobal>().Pathway.Length;
        path = new Transform[pathlength];
       for (int i = 0; i < path.Length; i++){
          path[i] = Manager.GetComponent<Gobal>().Pathway[i].transform;
          
       }

        
         
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, path[currentPoint].position);
        //  Vector3 startPosition = path2[currentPoint].transform.position;
        //Vector3 endPosition = path2[currentPoint + 1].transform.position;

        if (distance < 1)
        {
            if (currentPoint == path.Length - 1)
            {
                Destroy(gameObject, 1f);   
            }
            else
            {
                currentPoint++;
                // rotates
                transform.LookAt(path[currentPoint], Vector3.forward);
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                // Vector3.MoveTowards(transform.position, path[currentPoint].position, speed);
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, path[currentPoint].position, speed*Time.deltaTime);

      

  

    }


}


