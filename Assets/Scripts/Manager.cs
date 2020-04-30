using UnityEngine;
public class ingredient {
    public int cookState = 0;
    public float cookingTime = 0.0f;
    public bool isCooking = false;
    public float cookFactor;
    public float volume;
    public Material cooked;
    public Material overcooked;

    public ingredient(float thisCookFactor, float thisVolume, Material thisCooked, Material thisOverCooked) {
        cookFactor = thisCookFactor;
        volume = thisVolume;
        cooked = thisCooked;
        overcooked = thisOverCooked;
    }
}

public class Manager : MonoBehaviour
{
    public Material meatCooked;
    public Material meatOverCooked;
    public Material onionCooked;
    public Material onionOverCooked;

    public static ingredient newIngrediant(string name) {
        Manager manager = GameObject.Find("Manager").GetComponent<Manager>();
        if (name == "meat")
            return new ingredient(1.1f, 200f, manager.meatCooked, manager.meatOverCooked);
        else if (name == "onion") {
            return new ingredient(2.0f, 50f, manager.onionCooked, manager.onionOverCooked);
        }
        else
            return null;
    }
}
