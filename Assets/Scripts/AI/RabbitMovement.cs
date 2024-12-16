using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitMovement : MonoBehaviour
{
    Animator animator;

    public float moveSpeed;

    Vector3 stopPosition;

    float walkTime;
    public float walkCounter;
    float waittime;
    public float waitCounter;

    float walkRotation;

    public bool isWalking;

    private void Start()
    {
        animator = GetComponent<Animator>();

        walkTime = Random.Range(3, 9);
        waittime = Random.Range(2, 6);

        waitCounter = waittime;
        walkCounter = walkTime;

        ChooseDirection();
    }

    private void Update()
    {
        if(isWalking)
        {
            animator.SetBool("isRunning", true);
            walkCounter -= Time.deltaTime;

            transform.localRotation = Quaternion.Euler(0f, walkRotation, 0f);
            transform.position += transform.forward * moveSpeed * Time.deltaTime;



            if (walkCounter <= 0)
            {
                stopPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                isWalking = false;

                transform.position = stopPosition;
                animator.SetBool("isRunning", false);

                waitCounter = waittime;
            }
        }
        else
        {
            waitCounter -= Time.deltaTime;

            if(waitCounter <= 0)
            {
                ChooseDirection();
            }
        }
    }

    private void ChooseDirection()
    {
        walkRotation = Random.Range(-90, 181);

        isWalking = true;
        walkCounter = walkTime;
    }
}
