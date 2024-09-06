using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbits : MonoBehaviour
{
    public Transform[] bodies;
    public float orbitSpeed = 10f;

    void Update()
    {
        foreach (Transform body in bodies)
        {
            transform.RotateAround(body.position, Vector3.up, orbitSpeed * Time.deltaTime);
        }
    }
}
