using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour
{


    public GameObject Shell;
    public Transform TFireR;
    public Transform TFireL;
    public float rotaionTime;
    public float Shoot_delay = 1f;
    public bool CamShot = false;
    private float ShootTimer = 1;
    GameObject manager;
    Gobal MainScript;


    public GameObject enemy;
    GameObject shellInstanceR;
    GameObject shellInstanceL;

    Quaternion defaultpos;
    Vector3 rotationEnter;
    Vector3 rotationExit;
    Vector3 lookAtPoint;
    //bool Frist = true;

    Vector3 lastposition;


    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("MrManager");
        MainScript = manager.GetComponent<Gobal>();

        setDefaultpos();
        
    }
    void Update()
    {
        ShootTimer -= Time.deltaTime;
        // if Enemy isnt set rotates tower to default position and can not shoot. enemy is set in OnTriggerStay
        if (enemy == null)
        {
            rotateTower();

            CamShot = false;
        }
        //if CamShot is true Fire() is called on a delay
        if (CamShot == true)
        {
            
            if (ShootTimer <= 0)
            {
                ShootTimer = Shoot_delay;
                StartCoroutine(LookOnTime());
            }
        }



    }
    IEnumerator LookOnTime()
    {
        yield return new WaitForSeconds(0.3f);
        Fire();
    }
    /// <summary>
    ///  Creates bullets 
    /// </summary>
    private void Fire()
    {
        if (TFireR != null)
        {


            // create right bullet
            shellInstanceR = (GameObject)Instantiate(Shell, TFireR.position, TFireR.rotation);
            bulletfollow followerR = shellInstanceR.GetComponent<bulletfollow>();
            if (enemy != null)
            {
                followerR.target = enemy;
            }else
            {
                followerR.hitground = lastposition;
            }
            //DigitalRuby.LightningBolt.LightningBoltScript bolt = followerR.GetComponent<DigitalRuby.LightningBolt.LightningBoltScript>();
            //bolt.StartObject = TFireR.gameObject;
            //bolt.EndObject = enemy;
        }
        if (TFireL != null)
        {
            // create left bullet
            shellInstanceL = (GameObject)Instantiate(Shell, TFireL.position, TFireL.rotation);
            bulletfollow followerL = shellInstanceL.GetComponent<bulletfollow>();
            if (enemy != null)
            {
                followerL.target = enemy;
            }
            else
            {
                followerL.hitground = lastposition;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            // sets enemy
            enemy = other.gameObject;

            // makes tower look at enemy

            Vector3 targetPostition = other.transform.position - transform.position;
            targetPostition.y = 0;
            Quaternion targetpos = Quaternion.LookRotation(targetPostition);
            gameObject.transform.rotation = Quaternion.Lerp(transform.rotation, targetpos, rotaionTime * Time.deltaTime);

            // make the tower's bullet spawners look at the enemy
            if (TFireL != null)
            {
                TFireL.transform.LookAt(other.transform);
            }
            if (TFireR != null)
            {
                TFireR.transform.LookAt(other.transform);
            }
        
            

            //Allows tower to shoot
            CamShot = true;



        }
    }
    /// <summary>
    /// Sets the the tower to face the closest path
    /// </summary>
    private void setDefaultpos()
    {
        Vector3 turretPos = transform.position;

        lookAtPoint = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
        float nearestDistance = float.PositiveInfinity;


        for (int i = 0; i < MainScript.Pathway.Length - 1; ++i)
        {
            Vector3 point1 = MainScript.Pathway[i].transform.position;
            Vector3 point2 = MainScript.Pathway[i + 1].transform.position;

            Vector3 seg = point2 - point1;

            Vector3 toPoint = turretPos - point1;

            float toPointProjected = Vector3.Dot(toPoint, seg);
            if (toPointProjected <= 0.0f)
                continue;
            float segSqrMag = Vector3.Dot(seg, seg);
            if (segSqrMag <= toPointProjected)
                continue;

            float ratio = toPointProjected / segSqrMag;
            Vector3 projectedPoint = point1 + ratio * seg;

            float dist = Vector3.Distance(turretPos, projectedPoint);
            if (dist < nearestDistance)
            {
                nearestDistance = dist;
                lookAtPoint = projectedPoint;
            }
        }

        lookAtPoint.y = turretPos.y;
        transform.LookAt(lookAtPoint);

        defaultpos = transform.rotation;
    }



    private void OnTriggerExit(Collider other)
    {
        // if the gameobject leaving is the enemy 
        if (other.gameObject == enemy)
        {
            lastposition = other.gameObject.transform.position;

            // when enemy Exits the set the enemy to null
            enemy = null;
        }


        CamShot = false;
    }

    private void rotateTower()
    {
        gameObject.transform.rotation = Quaternion.Lerp(transform.rotation, defaultpos, rotaionTime * Time.deltaTime);
    }


}