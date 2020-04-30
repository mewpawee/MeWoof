using UnityEngine;
using EzySlice;
using VRTK;
using Packages.Rider.Editor.UnitTesting;
using UnityEngine.UIElements;
using System.Collections;

public class meshCut : MonoBehaviour
{
    public Transform cutPlane;
    public LayerMask layerMask;
    private bool isCut = true;

    // Start is called before the first frame update

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

   public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == 9 && isCut) {

            Collider[] hits = Physics.OverlapBox(cutPlane.position, new Vector3(5, 0.1f, 5), cutPlane.rotation, layerMask);
            isCut = false;
            if (hits.Length <= 0)
            {
                isCut = true;
                return;
            }

            for (int i = 0; i < hits.Length; i++)
            {
                Material crossMaterial = other.gameObject.GetComponent<Renderer>().material;
                SlicedHull hull = SliceObject(hits[i].gameObject, crossMaterial);
                if (hull != null)
                {
                    hits[i].gameObject.SetActive(false);
                    GameObject bottom = hull.CreateLowerHull(hits[i].gameObject, crossMaterial);
                    GameObject top = hull.CreateUpperHull(hits[i].gameObject, crossMaterial);
                    
                        AddHullComponents(bottom, hits[i].gameObject);
                        AddHullComponents(top, hits[i].gameObject);
                        Destroy(hits[i].gameObject);
                }
            }
            isCut = true;
            return;
        }
    }
    public void AddHullComponents(GameObject go, GameObject parent)
    { 
        go.layer = 9;
        go.tag = parent.tag;
        
        //add rigid body
        Rigidbody rb = go.AddComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        MeshCollider collider = go.AddComponent<MeshCollider>();
        collider.convex = true;
        rb.AddExplosionForce(50, go.transform.position,20);
        
        //add mesh
        //collider.convex = true;
        /*
        Mesh mesh = go.GetComponent<MeshCollider>().sharedMesh;
        Vector3[] verts = mesh.vertices;
        Vector3 v = verts[0];//save vertex
        verts[0] += Vector3.one * 0.001f;//add epsilon to workaround physics problem wrt coplanar vertices in simple meshes - usually quads
        mesh.vertices = verts;

        go.GetComponent<MeshCollider>().convex = true;
        verts[0] = v;//restore vertex
        mesh.vertices = verts;
        */
        //add script
        ingredientScript child = go.AddComponent<ingredientScript>();
        child.ing = Manager.newIngrediant(go.tag);
        float volume = goVolume(go, parent);
        child.ing.volume = volume;
       
        //add VRTK
        VRTK_InteractableObject vRTK_InteractableObject = go.AddComponent<VRTK_InteractableObject>();
        vRTK_InteractableObject.isGrabbable = true;
    }

    public  float goVolume(GameObject go, GameObject parent) {
        Bounds parentMeshBound = parent.GetComponent<MeshFilter>().sharedMesh.bounds;
        Bounds goMeshBound = go.GetComponent<MeshFilter>().sharedMesh.bounds;
        float parentMeshVolume = parentMeshBound.size.x * parentMeshBound.size.y * parentMeshBound.size.z;
        float goMeshVolume = goMeshBound.size.x * goMeshBound.size.y * goMeshBound.size.z;
        float parentVolume = parent.GetComponent<ingredientScript>().ing.volume;
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
