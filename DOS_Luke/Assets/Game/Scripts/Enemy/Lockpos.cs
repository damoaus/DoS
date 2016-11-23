using UnityEngine;
using System.Collections;

public class Lockpos : MonoBehaviour {


    float lockPos = 0;
    void Update()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, lockPos, lockPos);
    }
}