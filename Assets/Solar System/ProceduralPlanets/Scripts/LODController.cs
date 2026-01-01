using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LODController : MonoBehaviour
{
    public Transform viewer;
    public LOD[] lods;

    void Update()
    {
        float dstToViewer = Vector3.Distance (transform.position, viewer.transform.position);
    }

    public struct LOD
    {
        public int lod;
        public int lodResolution;
    }
}
