using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using EzySlice;
using VRTK;
public class meshCut : MonoBehaviour
{
    public TimeManager tm1;
    public Transform cutPlane;
    public LayerMask layerMask;
    private bool isGrabed = true;
    // Start is called before the first frame update

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
      
    }
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9) {
            tm1.DoSlowMotion();
            Collider[] hits = Physics.OverlapBox(cutPlane.position, new Vector3(5, 0.1f, 5), cutPlane.rotation, layerMask);

            if (hits.Length <= 0)
                return;

            for (int i = 0; i < hits.Length; i++)
            {
                Material crossMaterial = other.gameObject.GetComponent<Renderer>().material;
                SlicedHull hull = SliceObject(hits[i].gameObject, crossMaterial);
                if (hull != null)
                {
                    GameObject bottom = hull.CreateLowerHull(hits[i].gameObject, crossMaterial);
                    GameObject top = hull.CreateUpperHull(hits[i].gameObject, crossMaterial);
                    AddHullComponents(bottom, hits[i].gameObject);
                    AddHullComponents(top, hits[i].gameObject);
                    Destroy(hits[i].gameObject);
                }
            }
        }
    }

    public void AddHullComponents(GameObject go, GameObject parent)
    { 
        go.layer = 9;
        Rigidbody rb = go.AddComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        MeshCollider collider = go.AddComponent<MeshCollider>();
        collider.convex = true;
        VRTK_InteractableObject vRTK_InteractableObject = go.AddComponent<VRTK_InteractableObject>();
        vRTK_InteractableObject.isGrabbable = true;
        go.gameObject.tag = parent.tag;
        go.AddComponent<steakScript>();

        rb.AddExplosionForce(100, go.transform.position, 20);
    }

    public SlicedHull SliceObject(GameObject obj, Material crossSectionMaterial = null)
    {
        // slice the provided object using the transforms of this object
        if (obj.GetComponent<MeshFilter>() == null)
            return null;

        return obj.Slice(cutPlane.position, cutPlane.up, crossSectionMaterial);
    }


}
