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

}
