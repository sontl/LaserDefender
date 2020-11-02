using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Player Movement")]
    [SerializeField] int moveSpeed;
    [SerializeField] float padding;
    [SerializeField] int health = 200;

    [Header("Projecttile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projecttileFiringPeriod;

    Coroutine firingCoroutine;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    void Start()
    {
        SetUpMoveBoundaries();
        //StartCoroutine(NameOfCoroutine());
    }



    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuosly());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuosly()
    {
        while (true)
        {
            GameObject laser = Instantiate(
                laserPrefab, transform.position,
                Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            yield return new WaitForSeconds(projecttileFiringPeriod); 
        }
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed ;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin , xMax );

        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

    IEnumerator NameOfCoroutine()
    {
        Debug.Log("Start wait for 3 seconds");
        yield return new WaitForSeconds(3);
        Debug.Log("End 3 seconds");
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0f, 1f, 0f)).y - padding;
    }
    private void OnTriggerEnter2D(Collider2D other)
    { 
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if(!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
