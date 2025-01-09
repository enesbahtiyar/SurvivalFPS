using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JetBrains.Annotations;

public class SelectionManager : SingletonMonoBehaviour<SelectionManager>
{
    public GameObject uiCanvas;
    public TMP_Text objectNameDisplayerText;
    InteractableObject interactable;

    public bool onTarget;


    public GameObject selectedTree;

    protected override void Awake()
    {
        base.Awake();
    }


    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 7.5f))
        {
            hit.transform.TryGetComponent(out InteractableObject interactableObject);

            if(interactableObject != null)
            {
                interactable = interactableObject;
                uiCanvas.SetActive(true);
                objectNameDisplayerText.text = interactable.ObjectName;
                onTarget = true;
                interactable.canBePicked = true;
            }
            else
            {
                objectNameDisplayerText.text = string.Empty;
                onTarget = false;
                interactable = null;
            }


            hit.transform.TryGetComponent(out ChoppableTree choppableTree);

            if (choppableTree && choppableTree.playerInRange)
            {
                choppableTree.canBeChopped = true;
                selectedTree = choppableTree.gameObject;
                    
            }
            else
            {
                if(selectedTree != null)
                {
                    selectedTree.gameObject.GetComponent<ChoppableTree>().canBeChopped = false;
                    selectedTree = null;
                }
            }
            
        }
        else
        {
            interactable = null;
            onTarget = false;
            objectNameDisplayerText.text = string.Empty;
        }
    }
}
