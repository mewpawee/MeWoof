using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    private GameObject triggerNPC;
    private bool triggering;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (triggering)
        {
            Debug.Log("OOF" + triggerNPC);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject)
        {
            triggering = true;
            triggerNPC = collision.gameObject;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject)
        {
            triggering = false;
            triggerNPC = null;
        }
    }
}
