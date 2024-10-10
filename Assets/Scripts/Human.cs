using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{

    Driver driver;
    [SerializeField] float moveSpeed = 15f;
    [SerializeField] float forceMultiplier = 10f;
    bool isAlive = true;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        driver = FindObjectOfType<Driver>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();

    }

    private void FollowPlayer()
    {
        if (!isAlive) { return; }

        Vector2 newPosition = Vector2.MoveTowards(this.transform.position, driver.transform.position, moveSpeed * Time.deltaTime);

        this.transform.position = newPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 hitDirection = (transform.position - collision.transform.position).normalized;
            rb.AddForce(hitDirection * forceMultiplier, ForceMode2D.Impulse);

            isAlive = false;
        }
    }
}
