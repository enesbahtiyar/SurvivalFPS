using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EquippableItem : MonoBehaviour
{

    public Animator animator;
    int damage = 3;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !InventorySystem.Instance.isOpen && !CraftingSystem.Instance.isOpen)
        {
            animator.SetTrigger("swing");
        }
    }

    public void GetHit()
    {
        GameObject choppableTree = SelectionManager.Instance.selectedTree;

        if (choppableTree != null && choppableTree.GetComponent<ChoppableTree>().canBeChopped)
        {
            choppableTree.GetComponent<ChoppableTree>().GetHit(damage);
        }
    }
}
