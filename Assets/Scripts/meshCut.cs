using UnityEngine;
using EzySlice;
using VRTK;
using Packages.Rider.Editor.UnitTesting;
using UnityEngine.UIElements;

public class meshCut : MonoBehaviour
{
    public Transform cutPlane;
    public LayerMask layerMask;

    // Start is called before the first frame update

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        
    }
    */
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9) {
            Collider[] hits = Physics.OverlapBox(cutPlane.position, new Vector3(5, 0.1f, 5), cutPlane.rotation, layerMask);

            if (hits.Length <= 0)
            {
                return;
            }

            for (int i = 0; i < hits.Length; i++)
            {
                Material crossMaterial = other.gameObject.GetComponent<Renderer>().material;
                SlicedHull hull = SliceObject(hits[i].gameObject, crossMaterial);
                if (hull != null)
                {
                    GameObject bottom = hull.CreateLowerHull(hits[i].gameObject, crossMaterial);
                    GameObject top = hull.CreateUpperHull(hits[i].gameObject, crossMaterial);
                    try
                    {
                        AddHullComponents(bottom, hits[i].gameObject);
                        AddHullComponents(top, hits[i].gameObject);
                        Destroy(hits[i].gameObject);
                    }
                    catch
                    {
                        break;
                    }
                }
            }
        }
    }

    public void AddHullComponents(GameObject go, GameObject parent)
    { 
        go.layer = 9;
        go.tag = parent.tag;
        
        //add rigid body
        Rigidbody rb = go.AddComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.AddExplosionForce(30, go.transform.position, 10);
        
        //add mesh
        MeshCollider collider = go.AddComponent<MeshCollider>();
        collider.convex = true;

        //add script
        float volume = goVolume(go, parent);
        steakScript child = go.AddComponent<steakScript>();
        child.volume = volume;
        Debug.Log("Volume" + volume);


        //add VRTK
        VRTK_InteractableObject vRTK_InteractableObject = go.AddComponent<VRTK_InteractableObject>();
        vRTK_InteractableObject.isGrabbable = true;
    }

    public float goVolume(GameObject go, GameObject parent) {
        Bounds parentMeshBound = parent.GetComponent<MeshFilter>().sharedMesh.bounds;
        Bounds goMeshBound = go.GetComponent<MeshFilter>().sharedMesh.bounds;
        float parentMeshVolume = parentMeshBound.size.x * parentMeshBound.size.y * parentMeshBound.size.z;
        float goMeshVolume = goMeshBound.size.x * goMeshBound.size.y * goMeshBound.size.z;
        float parentVolume = parent.GetComponent<steakScript>().volume;
        float volume = (parentVolume * goMeshVolume) / parentMeshVolume;
        return volume;
    }

    public SlicedHull SliceObject(GameObject obj, Material crossSectionMaterial = null)
    {
        // slice the provided object using the transforms of this object
        if (obj.GetComponent<MeshFilter>() == null)
            return null;

        return obj.Slice(cutPlane.position, cutPlane.up, crossSectionMaterial);
    }


}
