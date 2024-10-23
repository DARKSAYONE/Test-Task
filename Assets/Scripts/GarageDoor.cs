using UnityEngine;
using DG.Tweening;

public class GarageDoorController : MonoBehaviour
{
    [SerializeField] private Transform openDoorPosition;  
    [SerializeField] private float openDuration = 5.0f;  

    private Vector3 closedDoorPosition;  
    private Quaternion closedDoorRotation;


    private void Awake()
    {
        closedDoorPosition = transform.position;
        closedDoorRotation = transform.rotation;
    }
    private void Start()
    {
        OpenDoor();
    }
    public void OpenDoor()
    {
        transform.DOMove(openDoorPosition.position, openDuration).SetEase(Ease.InOutQuad);
        transform.DORotateQuaternion(openDoorPosition.rotation, openDuration).SetEase(Ease.InOutQuad);
    }
}
