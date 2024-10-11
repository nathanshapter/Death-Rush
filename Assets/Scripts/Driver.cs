using Cinemachine;
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

   [SerializeField] CinemachineVirtualCamera cam;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam.m_Lens.OrthographicSize = 15.64f;
        print(cam.m_Lens.OrthographicSize);
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

        if (Input.GetKeyDown(KeyCode.UpArrow)) 
        {
            cam.m_Lens.OrthographicSize += 5;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            cam.m_Lens.OrthographicSize -= 5;
        }

        transform.Rotate(0, 0, -steerAmount);
        transform.Translate(0, forwardAmount, 0);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, 10);
    }

    private void OnApplicationQuit()
    {
        cam.m_Lens.OrthographicSize = 15.64f;
    }
}
