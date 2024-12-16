using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectionManager : MonoBehaviour
{
    public GameObject uiCanvas;
    public TMP_Text objectNameDisplayerText;


    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            hit.transform.TryGetComponent(out InteractableObject interactableObject);

            if(interactableObject != null)
            {
                uiCanvas.SetActive(true);
                objectNameDisplayerText.text = interactableObject.ObjectName;
            }
            else
            {
                objectNameDisplayerText.text = string.Empty;
            }
        }
    }
}
