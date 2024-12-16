using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerCrouch : SingletonMonoBehaviour<PlayerCrouch>
{
    [SerializeField] float crouchHeight = 0.9f;
    [SerializeField] float crouchTransitionSpeed;
    [SerializeField] float croucSpeedMultipler = 0.5f;

    Vector3 initialCameraPosition;
    float currentHeight;
    float standingHeight;
    PlayerMovement player;

    bool isCrouching => standingHeight - currentHeight > .1f;

    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        standingHeight = currentHeight = player.Height;

        initialCameraPosition = player.cameraTransform.localPosition;
    }

    private void OnEnable()
    {
        EventsHandler.OnBeforeMove += OnBeforeMove;
    }

    private void OnDisable()
    {
        EventsHandler.OnBeforeMove -= OnBeforeMove;
    }

    void OnBeforeMove()
    {
        bool isTryingToCrouch = false;
        if (Input.GetKey(KeyCode.LeftControl))
        {
            isTryingToCrouch = true;
            Debug.Log("eğilme");
        }
        else
        {
            isTryingToCrouch = false;
        }

        var heightTarget = isTryingToCrouch ? crouchHeight : standingHeight;

        if(isCrouching && !isTryingToCrouch)
        {
            var castOrigin = transform.position + new Vector3(0, currentHeight / 2, 0);
            if(Physics.Raycast(castOrigin, Vector3.up, out RaycastHit hit, 0.2f))
            {
                var distanceToCeiling = hit.point.y - castOrigin.y;
                heightTarget = Mathf.Max
                    (currentHeight + distanceToCeiling - 0.1f, crouchHeight);
            }
        }


        if(!Mathf.Approximately(heightTarget, currentHeight))
        {
            float crouchDelta = Time.deltaTime * crouchTransitionSpeed;
            currentHeight = Mathf.Lerp(currentHeight, heightTarget, crouchDelta);

            Vector3 halfHeightDifference = new Vector3(0, (standingHeight - currentHeight) / 2, 0);
            Vector3 newCameraPosition = initialCameraPosition - halfHeightDifference;

            player.cameraTransform.localPosition = newCameraPosition;

            player.Height = currentHeight;

            Debug.Log(isCrouching);
        }


        if(isCrouching)
        {
            player.movementSpeedMultiplier *= croucSpeedMultipler;
        }
          
    }

}
