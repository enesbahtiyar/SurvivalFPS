using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private string objectName;

    public bool canBePicked;

    public bool onTarget;
    private bool isPlayerNear;
    float counter;
    public string ObjectName
    {
        get
        {
            return objectName;
        }
    }


    private void Update()
    {
        onTarget = SelectionManager.Instance.onTarget;
        if (Input.GetMouseButtonDown(0) && canBePicked && onTarget && isPlayerNear && !InventorySystem.Instance.CheckIfFull())
        {
            InventorySystem.Instance.AddToInventory(ObjectName);
            Destroy(this.gameObject);
        }

        CanBePickedCounter();
    }

    private void CanBePickedCounter()
    {
        if (canBePicked)
        {
            counter += Time.deltaTime;

            if (counter > 1)
            {
                canBePicked = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}
