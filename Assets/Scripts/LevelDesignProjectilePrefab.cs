using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelDesignProjectilePrefab : MonoBehaviour
{
    public float projectileSpeed;
    public float projectileImpactRadius;
    public float projectileLifetime;

    public GameObject explosionVisuals;
    // Start is called before the first frame update
    void Start()
    {
        //destroys the projectile after projectileLifetime amount of time
       // Destroy(gameObject, projectileLifetime);
    }

    // Update is called once per frame
    void Update()
    {
        //moves the projectile forward
        transform.position += transform.forward * projectileSpeed * Time.deltaTime;
        
        
    }
    //checks if the projectile collides with an object
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag != "Boss")
        {
            print("hit");
            //if the projectile collides with an object, it will explode
            Explode();
        }

      
    }
    
void Explode()
{
    print("exploded");

    // Check if the gameObject is null
    if (gameObject == null)
    {
        return;
    }

    // Create a sphere at the projectile's position with a radius of projectileImpactRadius
    Collider[] colliders = Physics.OverlapSphere(transform.position, projectileImpactRadius);

    // Check for collisions with other objects
    foreach (Collider nearbyObject in colliders)
    {
        if (nearbyObject.tag == "Player")
        {
            // Deal damage to the player
            Debug.Log("Player Hit!");
        }
    }

    // Instantiate explosion visuals if available
    if (explosionVisuals != null)
    {
        Instantiate(explosionVisuals, transform.position, transform.rotation);
    }

    // Destroy the gameObject at the end
    Destroy(gameObject);
 }
}
