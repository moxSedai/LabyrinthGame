﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 6f;
    private Rigidbody2D rb;
    Camera viewCamera;

    [SerializeField] private FieldOfView fieldOfView;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        viewCamera = Camera.main;


        if (rb == null)
        {
            Debug.LogError("Player::Start cant find RigidBody2D </sadface>");
        }
        rb.drag = 5;
        rb.gravityScale = 0;
    }

    void Update()
    {
        Vector3 targetPosition = GetMouseWorldPosition();
        Vector3 aimDir = (targetPosition - transform.position).normalized;
        transform.LookAt(targetPosition);
        fieldOfView.SetAimDirection(aimDir);
        fieldOfView.SetOrigin(transform.position);

    }
        
    // this is called at a fixed interval for use with physics objects like the RigidBody2D
    void FixedUpdate()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {

            // convert user input into world movement
            float horizontalMovement = Input.GetAxisRaw("Horizontal") * moveSpeed;
            float verticalMovement = Input.GetAxisRaw("Vertical") * moveSpeed;

            //assign world movements to a Veoctor2
            Vector2 directionOfMovement = new Vector2(horizontalMovement, verticalMovement);

            // apply movement to player's transform
            rb.AddForce(directionOfMovement);
        }
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    public static Vector3 GetMouseWorldPositionWithZ()
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}