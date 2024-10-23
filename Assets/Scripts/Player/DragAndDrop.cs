using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] private Transform originPoint;
    [SerializeField] private TextMeshProUGUI pressButtonText;

    private Camera playerCamera;
    private GameObject draggedObject;
    private Rigidbody draggedObjectRigidbody;
    private bool canInteract = true;

    private void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        if (playerCamera == null) Debug.LogError("Player camera not found");
    }

    private void Update()
    {
        if (draggedObject != null && Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            StartCoroutine(DropObjectWithDelay());
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
                    pressButtonText.text = "Press E to pickup item";
                    if (Input.GetKeyDown(KeyCode.E) && canInteract)
                    {
                        StartCoroutine(PickUpObjectWithDelay(hit.collider.gameObject));
                        Debug.Log("PickUp obj");
                    }
                    return;
                }
            }
            pressButtonText.text = " ";
        }
    }

    private IEnumerator PickUpObjectWithDelay(GameObject draggedObj)
    {
        canInteract = false;
        PickUpObject(draggedObj);
        yield return new WaitForSeconds(0.5f);
        canInteract = true;
    }

    private IEnumerator DropObjectWithDelay()
    {
        canInteract = false;
        DropObject();
        yield return new WaitForSeconds(0.5f);
        canInteract = true;
    }

    private void PickUpObject(GameObject draggedObj)
    {
        draggedObject = draggedObj;
        draggedObjectRigidbody = draggedObject.GetComponent<Rigidbody>();
        if (draggedObjectRigidbody != null)
        {
            draggedObjectRigidbody.useGravity = false;
            draggedObjectRigidbody.isKinematic = true;
        }
        draggedObj.transform.position = originPoint.position;
        draggedObj.transform.SetParent(originPoint);
    }

    private void DropObject()
    {
        if (draggedObjectRigidbody != null)
        {
            draggedObjectRigidbody.useGravity = true;
            draggedObjectRigidbody.isKinematic = false;
        }
        draggedObject.transform.SetParent(null);
        draggedObject = null;
        draggedObjectRigidbody = null;
    }
}
