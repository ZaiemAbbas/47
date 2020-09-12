using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedDisable : MonoBehaviour
{
    public float duration = 0.5f;
    void OnEnable()
    {
        Invoke("DisableIt", duration);
    }

    // Update is called once per frame
    void DisableIt()
    {
        gameObject.SetActive(false);
    }
}
