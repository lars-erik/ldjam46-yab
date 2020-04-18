using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainersController : MonoBehaviour
{
    public GameObject ContainerPrefab;
    public GameObject[] Things;

    private GameObject[] containers = new GameObject[5];

    // Start is called before the first frame update
    void Start()
    {
        for (var i = 0; i < 5; i++)
        {
            containers[i] = Instantiate(ContainerPrefab, new Vector3(1.53f, -.2f+.3f*i, .5f), Quaternion.identity);
            Instantiate(Things[0], containers[i].transform);
            containers[i].transform.GetChild(0).SetPositionAndRotation(containers[i].transform.position, Quaternion.Euler(0,0,0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
