using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using UnityEngine;

public class DragDropController : MonoBehaviour
{
    public GameObject Blob;
    public GameObject Face;
    public Texture IdleFace;
    public Texture OpenFace;

    private GameObject draggedObject;
    private Vector3 originalPoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.Instance.Halted)
        {
            return;
        }

        var mousePos = Input.mousePosition;
        var ray = Camera.main.ScreenPointToRay(mousePos);
        var hits = Physics.RaycastAll(ray, 100f);
        var isOverFace = false;
        if (hits.Length > 0)
        {
            if (hits.Any(x => x.collider.gameObject.name == "blob"))
            {
                isOverFace = true;
            }
            else
            {
                var hit = hits[0];
                if (hit.collider.gameObject.name.StartsWith("Container")
                 && Input.GetMouseButtonDown(0)
                 && draggedObject == null
                 )
                {
                    draggedObject = hit.collider.gameObject;
                    originalPoint = hit.transform.position;
                }
            }
        }

        if (draggedObject != null)
        {
            var distance = Camera.main.transform.position.z - draggedObject.transform.position.z;
            var worldPoint = Camera.main.ScreenToWorldPoint(mousePos + new Vector3(0, 0, distance));
            if (Input.GetMouseButton(0))
            {
                draggedObject.transform.position = new Vector3(worldPoint.x, worldPoint.y, originalPoint.z);
                if (isOverFace)
                {
                    Face.GetComponent<MeshRenderer>().material.mainTexture = OpenFace;
                }
                else
                {
                    Face.GetComponent<MeshRenderer>().material.mainTexture = IdleFace;
                }
            }
            else
            {
                if (isOverFace)
                {
                    GameController.Instance.Ate(draggedObject);
                }
                else
                {
                    draggedObject.transform.position = originalPoint;
                }
                draggedObject = null;
                Face.GetComponent<MeshRenderer>().material.mainTexture = IdleFace;
            }
        }
    }
}
