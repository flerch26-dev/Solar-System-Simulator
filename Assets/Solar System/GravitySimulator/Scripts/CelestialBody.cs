using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class CelestialBody : MonoBehaviour
{
    public float radius;
    public float surfaceGravity;
    public Vector3Int initialVelocity;
    public string bodyName = "Unnamed";
    Transform meshHolder;

    public Vector3 velocity { get; private set; }
    public float mass { get; private set; }
    public Vector3 rotationSpeed;
    Rigidbody rb;

    CelestialBody[] bodies;

    void Awake () {
        rb = GetComponent<Rigidbody> ();
        rb.mass = mass;
        velocity = initialVelocity;
        bodies = FindObjectsOfType<CelestialBody> ();
        transform.GetChild(0).GetComponent<Planet>().GeneratePlanet();
    }

    public void UpdateVelocity (CelestialBody[] allBodies, float timeStep) {
        foreach (var otherBody in allBodies) {
            if (otherBody != this) {
                float sqrDst = (otherBody.rb.position - rb.position).sqrMagnitude;
                Vector3 forceDir = (otherBody.rb.position - rb.position).normalized;

                Vector3 acceleration = forceDir * UniversalValues.gravitationalConstant * otherBody.mass / sqrDst;
                velocity += acceleration * timeStep;
            }
        }
    }

    public void UpdatePosition (float timeStep) {
        rb.MovePosition (rb.position + velocity * timeStep);
    }

    public void Rotate()
    {
        transform.Rotate(rotationSpeed);
    }

    void OnValidate () {
        mass = surfaceGravity * radius * radius / UniversalValues.gravitationalConstant;
        meshHolder = transform.GetChild(0);
        //meshHolder.localScale = Vector3.one * radius;
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
