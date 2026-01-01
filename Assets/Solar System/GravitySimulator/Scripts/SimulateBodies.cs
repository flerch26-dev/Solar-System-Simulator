using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulateBodies : MonoBehaviour
{
    CelestialBody[] bodies;
    public Transform viewer;
    public LOD[] lods;

    void Awake()
    {
        bodies = FindObjectsOfType<CelestialBody>();
    }

    void FixedUpdate()
    {
        for (int i = 0; i < bodies.Length; i++)
        {
            float dstToViewer = Vector3.Distance (bodies[i].transform.position, viewer.transform.position);
            int oldRes = bodies[i].transform.GetChild(0).GetComponent<Planet>().resolution;
            if (dstToViewer > lods[lods.Length - 1].lodDistanceThreshold)
            {
                bodies[i].transform.GetChild(0).gameObject.SetActive(false);
                continue;
            }
            for (int j = 0; j < lods.Length; j++)
            {
                bodies[i].transform.GetChild(0).gameObject.SetActive(true);
                if (dstToViewer < lods[j].lodDistanceThreshold)
                {
                    bodies[i].transform.GetChild(0).GetComponent<Planet>().resolution = lods[j].lodResolution;
                    if (bodies[i].transform.GetChild(0).GetComponent<Planet>().resolution != oldRes)
                    {
                        bodies[i].transform.GetChild(0).GetComponent<Planet>().GeneratePlanet();
                    }
                    break;
                }
            }
        }

        for (int i = 0; i < bodies.Length; i++)
        {
            bodies[i].UpdateVelocity(bodies, UniversalValues.physicsTimeStep);
        }

        for (int i = 0; i < bodies.Length; i++)
        {
            bodies[i].UpdatePosition(UniversalValues.physicsTimeStep);
        }

        for (int i = 0; i < bodies.Length; i++)
        {
            bodies[i].Rotate();
        }
    }

    [System.Serializable]
    public class LOD
    {
        public int lodResolution;
        public int lodDistanceThreshold;
    }
}
