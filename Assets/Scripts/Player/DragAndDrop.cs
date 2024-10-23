using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] private Transform originPoint;

    private Camera playerCamera;
    private GameObject draggedObject;
    private Rigidbody draggedObjectRigidbody;

    private void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        if (playerCamera == null) Debug.LogError("Player camera not found");
    }
    private void Update()
    {

        if (draggedObject != null && Input.GetKeyDown(KeyCode.E))
        {
            DropObject();
            Debug.Log("Drop Obj");
        }


        if (draggedObject == null)
        {
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            RaycastHit hit;

            Debug.DrawRay(ray.origin, ray.direction * 5.0f, Color.green);
            if (Physics.Raycast(ray, out hit, 5.0f))
            {
                if (hit.collider.CompareTag("Interacted"))
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        PickUpObject(hit.collider.gameObject);
                        Debug.Log("PickUp obj");
                    }
                }
            }
        }
    }

    private void PickUpObject(GameObject draggedObj)
    {
        draggedObject = draggedObj;
        draggedObjectRigidbody = draggedObject.GetComponent<Rigidbody>();

        if (draggedObjectRigidbody != null)
        {
            draggedObjectRigidbody.useGravity = false;
            //draggedObjectRigidbody.isKinematic = true;
        }
        draggedObj.transform.position = originPoint.position;
        draggedObj.transform.SetParent(originPoint);
    }

    private void DropObject()
    {
        if (draggedObjectRigidbody != null)
        {
            draggedObjectRigidbody.useGravity = true;
            //draggedObjectRigidbody.isKinematic = false;
        }
        draggedObject.transform.SetParent(null);
        draggedObject = null;
        draggedObjectRigidbody = null;
    }

}
