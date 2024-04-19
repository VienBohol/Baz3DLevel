using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    // Public variables that can be customized in the Unity editor
    public GameObject projectilePrefab;
    public int amountOfProjectiles = 5;
    public float projectileSpeed = 10.0f;
    public float cooldown = 1.0f;
    public float range = 10.0f;
    public float turnSpeed;
    public Transform playerPosition;

    // Private variables to keep track of the shoot cooldown
    private float shootTimer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        // Increment the shoot timer every frame
        shootTimer += Time.deltaTime;

        // Check if the player is within range
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Player"))
            {
                // Check if the cooldown has been reached
                if (shootTimer >= cooldown)
                {
                    // Reset the shoot timer
                    shootTimer = 0.0f;

                    // Calculate the direction to the player
                    Vector3 direction = (hitCollider.transform.position - transform.position).normalized;

                    // Rotate the shooter object to face the player
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerPosition.position - transform.position, Vector3.up), turnSpeed * Time.deltaTime); 

                    // Shoot the specified amount of projectiles
                    for (int i = 0; i < amountOfProjectiles; i++)
                    {
                         // Instantiate a new projectile
                         GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

                         // Rotate the projectile to face the player
                         projectile.transform.LookAt(playerPosition);

                         // Set the projectile's velocity
                         projectile.GetComponent<Rigidbody>().velocity = projectile.transform.forward * projectileSpeed;

                         // Add a force to the projectile to make it shoot out
                         projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * projectileSpeed, ForceMode.VelocityChange);

                         // Destroy the projectile after a certain amount of time
                         Destroy(projectile, 2.0f);

                }
            }
        }
    }

  }
}