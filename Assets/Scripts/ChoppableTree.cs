using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoppableTree : MonoBehaviour
{
    public bool playerInRange;
    public bool canBeChopped;

    public int maxHealth;
    public int currentHealth;

    public Animator animator;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    public void GetHit(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("hit");

        if(currentHealth <= 0)
        {
            Vector3 treePosition = transform.parent.transform.position;
            Destroy(transform.parent.gameObject);
            SelectionManager.Instance.selectedTree = null;


            GameObject cuttedTree = Instantiate(Resources.Load<GameObject>("CuttedTree"), treePosition, Quaternion.Euler(0, 0, 0));
        }
    }
}
