using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent (typeof (Rigidbody))]
public class CelestialBody :  MonoBehaviour {

    public float radius;
    public float surfaceGravity;
    public Vector3 initialVelocity;
    public string bodyName = "Unnamed";
    Transform meshHolder;

    public Vector3 velocity { get; private set; }
    public float mass { get; private set; }
    Rigidbody rb;

    void Awake ()
    {
        CheckValue();
        rb = GetComponent<Rigidbody> ();
        rb.mass = mass;
        velocity = initialVelocity;
    }

    public void UpdateVelocity (Vector3 acceleration, float timeStep) {
        velocity += acceleration * timeStep;
    }

    public void UpdatePosition (float timeStep) {
        rb.MovePosition (rb.position + velocity * timeStep);

    }

    void OnValidate ()
    {
        CheckValue();
    }

    void CheckValue()
    {
        mass = surfaceGravity * radius * radius / Universe.gravitationalConstant;
        meshHolder = transform;
        meshHolder.localScale = Vector3.one * radius;
        gameObject.name = bodyName;
    }
    

    public Rigidbody Rigidbody {
        get {
            return rb;
        }
    }

    public Vector3 Position {
        get {
            return rb.position;
        }
    }

}