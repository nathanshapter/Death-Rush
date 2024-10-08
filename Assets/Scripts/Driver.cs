using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    [SerializeField] float steerSpeed = 1f;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float decelerationRate = 2f;
   [SerializeField] private float currentSpeed = 0f;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float steerAmount = Input.GetAxis("Horizontal") * Time.deltaTime * steerSpeed;
        float forwardAmount = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        if (forwardAmount != 0)
        {
            currentSpeed = forwardAmount * moveSpeed;
            print(currentSpeed);
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, decelerationRate);
        }



        transform.Rotate(0, 0, -steerAmount);
        transform.Translate(0, forwardAmount, 0);
    }
}
