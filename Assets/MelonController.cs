using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelonController : MonoBehaviour
{
    public float moveForce = 20f;
    public float maxHealth = 100f;
    public float deathHeight = -10f;


    private float currentHealth;
    private Rigidbody rb;
    private Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
        currentHealth = maxHealth;
        UpdateColor(); 
    }

    void Update()
    {
        if (transform.position.y < deathHeight)
        {
            Die();
        }
    }

    // FixedUpdate is a function that is called at specific times to help with physics calculations, here it is used for the movement system
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 force = new Vector3(h, 0 , v);
        rb.AddForce(force * moveForce);

        // Speed Clamp
        float maxSpeed = 10f;

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    // Collision function used to calculate the damage that should be taken based on the impact force
    void OnCollisionEnter(Collision collision)
    {
        float impactForce = collision.relativeVelocity.magnitude;
        float damageMultiplier = 2f;

        if (collision.gameObject.CompareTag("Sharp"))
        {
            damageMultiplier = 5f;
            TakeDamage(impactForce * damageMultiplier);
        }

        else if (collision.gameObject.CompareTag("Trap"))
        {
            TakeDamage(100f);
        }

        else if (impactForce > 8f)
        {
            TakeDamage(impactForce * damageMultiplier);
        }
    }

    // Function TakeDamage is called in OnCollisionEnter to apply damage and then call UpdateColor to change the color, if the player health is 0 then it calls Die instead
    void TakeDamage(float amount)
    {
        currentHealth -= amount;
        UpdateColor();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // UpdateColor simply changes the color of the Melon object with evey time it is called, adding a more browing tone to the melon to show bruising
    void UpdateColor()
    {
        float healthPercent = currentHealth / maxHealth;

        Color fresh = new Color(0.6f, 1f, 0.6f);
        Color bruised = new Color(0.25f, 0f, 0.2f);

        rend.material.color = Color.Lerp(bruised, fresh, healthPercent);
    }

    // A Die Function to simple Destroy the melon object, when the Die function is called it calls the MelonDied function in GameManager
    void Die()
    {
        GameManager.Instance.MelonDied();
        Destroy(gameObject);
    }
}
