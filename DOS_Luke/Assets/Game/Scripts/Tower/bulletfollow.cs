using UnityEngine;
using System.Collections;

public class bulletfollow : MonoBehaviour
{
    public int caseType;
    public int speed;
    public GameObject target;
    bool targetFound = true;
    Vector3 Endposition;
    Vector3 Nextpostion;
    float distance;
    public Vector3 hitground;


    void Start()
    {
        
        hitground.y = 20;
        gameObject.transform.LookAt(hitground);
    }
    void Update()
    {
        if (target != null)
        {
            hitground = target.transform.position;
            hitground.y = 20;
            hitground += transform.forward;
            gameObject.transform.LookAt(hitground);
        }

        Rigidbody bullet = gameObject.GetComponent<Rigidbody>();
        switch (caseType)
        {
            case 0:
                // Moves the bullet to target. Destorys the object if target cant be found

                if (target == null)
                {
                    Destroy(gameObject);
                }
                else
                {

                    gameObject.transform.LookAt(target.transform);
                    bullet.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);

                }

                break;

            case 1:
                if (target != null)
                {
                    if (targetFound)
                    {


                        Endposition = target.transform.position;
                        Endposition.y = 20;
                        Endposition += transform.forward;
                        Nextpostion = new Vector3(transform.position.x + (Endposition.x - transform.position.x) / 2, 25, transform.position.z + (Endposition.z - transform.position.z) / 2);

                        targetFound = false;
                    }

                    distance = Vector3.Distance(transform.position, Nextpostion);
                    gameObject.transform.LookAt(Nextpostion);
                    bullet.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);

                    if (distance < 1)
                    {
                        Nextpostion = Endposition;

                    }

                }
                else
                {
                    gameObject.transform.LookAt(hitground);
                    bullet.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);

                }



                break;
        }

    }

}