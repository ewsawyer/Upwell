using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public GameObject bullet;
    public float launchForce;
    public float recoilForce;
    public Transform shotPoint;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    void Update()
    {
     
        Vector2 arrowPosition = transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - arrowPosition;
        transform.right = direction;

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
   
        GameObject newArrow = Instantiate(bullet, shotPoint.position, shotPoint.rotation);
        newArrow.GetComponent<Rigidbody2D>().linearVelocity = transform.right * launchForce;

 
        Vector2 recoilDirection = -transform.right; // Opposite direction of the shot
        rb.AddForce(recoilDirection * recoilForce, ForceMode2D.Impulse);
    }
}
