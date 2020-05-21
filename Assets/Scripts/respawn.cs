using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class respawn : MonoBehaviour
{
    bool grabbed = false;
    Vector3 pos;
    Vector3 scale;
    // Start is called before the first frame update
    void Start()
    {
        pos = gameObject.transform.position;
        scale = gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<VRTK_InteractableObject>().IsGrabbed() && !grabbed)
        {
            grabbed = true;
            StartCoroutine(genPrefab());
        }
    }

    IEnumerator genPrefab()
    {
        yield return new WaitForSeconds(3);
        GameObject gen = Instantiate(gameObject);
        gen.transform.position = pos;
        gen.transform.localScale = scale;
        gen.GetComponent<Rigidbody>().isKinematic = false;
    }
}
