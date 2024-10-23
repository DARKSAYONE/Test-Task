using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Picker : MonoBehaviour
{
    [SerializeField] private List<GameObject> neededObjects;
    [SerializeField] private int objectsInsideTriggerCount = 0;
    private bool allObjectsCollected = false;

    [SerializeField] TextMeshProUGUI TextCount;


    private void OnTriggerEnter(Collider other)
    {
        if (neededObjects.Contains(other.gameObject))
        {
            objectsInsideTriggerCount++;
            TextCount.text = "Collected items: " + objectsInsideTriggerCount;
            if (objectsInsideTriggerCount == neededObjects.Count)
            {
                allObjectsCollected = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (neededObjects.Contains(other.gameObject))
        {
            objectsInsideTriggerCount--;
        }
    }
}
