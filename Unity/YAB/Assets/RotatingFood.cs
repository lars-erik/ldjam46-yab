using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RotatingFood : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.transform.GetChild(0).Rotate(0, Random.Range(0, 360), 0);
    }

    // Update is called once per frame
    void Update()
    {
        var rotation = Time.deltaTime * -180f;
        gameObject.transform.GetChild(0).Rotate(0, rotation, 0);
    }
}
