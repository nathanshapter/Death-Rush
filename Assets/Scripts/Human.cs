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

    HumanManager manager;

    bool attackingCar = true;
    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        driver = FindObjectOfType<Driver>();
        rb = GetComponent<Rigidbody2D>();
        manager = FindObjectOfType<HumanManager>();

        sprite = GetComponent<SpriteRenderer>();
     
    }

    // Update is called once per frame
    void Update()
    {
        if (attackingCar) 
        {
            FollowPlayer();
        }
        else
        {
            Vector2 currentPosition = transform.position; // The object's current position
            Vector2 targetPosition = driver.transform.position; // The position of the driver
            

            // Move the object away from the target
            Vector2 newPosition = MoveAway(currentPosition, targetPosition, moveSpeed * Time.deltaTime);

            transform.position = newPosition; // Update the position of the object
        }       
    }



    public static Vector2 MoveAway(Vector2 current, Vector2 target, float maxDistanceDelta)
    {
        // Calculate the direction vector pointing away from the target
        Vector2 directionAway = current - target;

        // Normalize the direction so that you move consistently
        directionAway.Normalize();

        // Move the current position away from the target by the maxDistanceDelta
        return current + directionAway * maxDistanceDelta;
    }


    private void FollowPlayer()
    {
        if (!isAlive) { return; }

        // Calculate the direction towards the driver
        Vector2 directionToDriver = (driver.transform.position - transform.position).normalized;

        // Move the human towards the driver
        Vector2 newPosition = (Vector2)transform.position + directionToDriver * moveSpeed * Time.deltaTime;

        transform.position = newPosition;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 hitDirection = (transform.position - collision.transform.position).normalized;
            rb.AddForce(hitDirection * forceMultiplier, ForceMode2D.Impulse);

            isAlive = false;
            manager.humansHit++;
            sprite.color = Color.red;

            StartCoroutine( Die());
        }
        else if (collision.gameObject.CompareTag("Human"))
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
        }
       
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(1);
        this.gameObject.SetActive(false);
    }
    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(60);
        this.gameObject.SetActive(false);
    }
}
