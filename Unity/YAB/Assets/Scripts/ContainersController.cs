using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

public class ContainersController : MonoBehaviour
{
    public GameObject ContainerPrefab;
    public GameObject[] Things;

    private GameObject[] containers = new GameObject[5];
    private int removedIndex;

    private float fallStart;
    private const float FallTime = .2f;
    private const float BottomPos = -.2f;
    private const float BoxInterval = .3f;
    private const float XPos = 1.53f;
    private const float ZPos = .5f;

    // Start is called before the first frame update
    void Start()
    {
        for (var i = 0; i < 5; i++)
        {
            CreateContainer(i);
        }
    }

    private GameObject CreateContainer(int index)
    {
        containers[index] = Instantiate(ContainerPrefab, PosForIndex(index), Quaternion.identity);
        containers[index].name = $"Container {index}";
        Instantiate(Things[Random.Range(0, Things.Length)], containers[index].transform);
        containers[index].transform.GetChild(0).SetPositionAndRotation(containers[index].transform.position, Quaternion.Euler(0, 0, 0));
        return containers[index];
    }

    private static Vector3 PosForIndex(int index)
    {
        return new Vector3(XPos, YForIndex(index), ZPos);
    }

    private static float YForIndex(int index)
    {
        return BottomPos + BoxInterval * index;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Remove(GameObject thing)
    {
        var index = -1;
        for (var i = 0; i < 5; i++)
        {
            if (containers[i] == thing)
            {
                index = i;
                break;
            }
        }

        if (index == -1)
        {
            throw new Exception("Panic! Item is not a container.");
        }

        removedIndex = index;
        for (var i = index + 1; i < 5; i++)
        {
            containers[i - 1] = containers[i];
        }

        containers[4] = null;
        Destroy(thing);

        StartCoroutine(AddNewItem());
    }

    private IEnumerator AddNewItem()
    {
        fallStart = Time.time;
        float t;

        if (removedIndex < 4)
        { 
            do
            {
                t = (Time.time - fallStart) / FallTime;
                for (var x = removedIndex; x < 4; x++)
                {
                    containers[x].transform.position = Vector3.Lerp(PosForIndex(x + 1), PosForIndex(x), t);
                }
                if (t < fallStart + FallTime) 
                { 
                    yield return new WaitForSeconds(.007f);
                }
            } while (Time.time < fallStart + FallTime);
            for (var x = removedIndex; x < 4; x++)
            {
                containers[x].transform.position = PosForIndex(x);
            }
        }

        fallStart = Time.time;

        var newObj = CreateContainer(4);
        newObj.transform.position = PosForIndex(5);

        do
        {
            t = (Time.time - fallStart) / FallTime;
            newObj.transform.position = Vector3.Lerp(PosForIndex(5), PosForIndex(4), t);
            if (t < fallStart + FallTime)
            {
                yield return new WaitForSeconds(.007f);
            }
        } while (Time.time < fallStart + FallTime);
        newObj.transform.position = PosForIndex(4);

        GameController.Instance.GoodToGo();
    }
}
