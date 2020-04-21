using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager instance = null;
    public static Material meatCooked;
    public Material thisCooked;
    public Material thisOverCooked;
    public static Material cooked;
    public static Material overCooked;


    private void Awake()
    {
        cooked = thisCooked;
        overCooked = thisOverCooked;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
