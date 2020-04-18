using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingFood : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var rotation = Time.deltaTime * 180f;
        gameObject.transform.Rotate(rotation, 0, 0);
    }
}
