using UnityEngine;

public class SafeZone : MonoBehaviour
{
    // Other scripts can read this to know if we are safe
    public static bool isPlayerSafe = true; 

    // Adjust this number in the Inspector to match your visual circle size
    public float detectionRadius = 5.0f;    

    void Update()
    {
        // 1. Find Ellen
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        if (player != null)
        {
            // 2. Calculate distance using Math (failsafe)
            float distance = Vector3.Distance(transform.position, player.transform.position);

            // 3. Decide safety
            if (distance < detectionRadius)
            {
                isPlayerSafe = true; // Inside
            }
            else
            {
                isPlayerSafe = false; // Outside
            }
        }
    }

    // This draws a visible Green Circle in your Scene View so you can tune the size
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}