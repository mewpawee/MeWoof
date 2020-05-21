using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class moveCamera : MonoBehaviour
{
    float time = 0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(movePlayer());
    }

    // Update is called once per frame
    IEnumerator movePlayer()
    {
        while (time < 1.5f)
        {
            this.transform.position += new Vector3(0f, 0f, -0.01f);
            time += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }
}
