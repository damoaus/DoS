using UnityEngine;
using System.Collections;

public class RTSCam : MonoBehaviour
{
    public Transform target;
    float speed;
    Vector3 direction;

    // Boundary variables 
    float BoundaryT = 200;
    float BoundaryB = 0;
    float BoundaryL = 45;
    float BoundaryR = 210;
    float Bdifferance = 15;



    float mPosX;
    float mPosY;
    float movementRange;

    void Start()
    {
           
        // Sets the Boundary on the carmera mount 
        Terrain terrain = Terrain.activeTerrain;
        Vector3 terrianSize = terrain.terrainData.size;

        movementRange = Screen.height / 20;

        BoundaryB = terrain.transform.position.z;
        BoundaryL = terrain.transform.position.y + Bdifferance;
        BoundaryT = terrain.transform.position.z + terrianSize.z - Bdifferance;
        BoundaryR = terrain.transform.position.y + terrianSize.x - Bdifferance;
    }



    void Update()
    {
        speed = 30 * Time.deltaTime;
        mPosX = Input.mousePosition.x;
        mPosY = Input.mousePosition.y;

        // checks to see if carmera mount has reached the boarder if not allows the camera to move 
        if (target.position.x < BoundaryR)
        {
            if (mPosX >= Screen.width - movementRange)
            {
                direction = -target.right;
                direction *= speed;
                target.position -= direction;

                
            }
        }
        if (target.position.x > BoundaryL)
        {
            if (mPosX <= movementRange)
            {
                direction = target.right;
                direction *= speed;
                target.position -= direction;

            }
        }
        if (target.position.z < BoundaryT)
        {
            if (mPosY >= Screen.height - movementRange)
            {
                direction = -target.forward;
                direction *= speed;
                target.position -= direction;

            }
        }
        if (target.position.z > BoundaryB)
            if (mPosY <= movementRange)
            {
                direction = target.forward;
                direction *= speed;
                target.position -= direction;

            }


        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (target.position.y < 60)
            {
                target.position += new Vector3(0, 2);

            }
        }
        // On scroll wheel up, lowers the carmera mount
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (target.position.y > 30)
            {
                target.position -= new Vector3(0, 2);
            }
        }


    }
    
}
