using UnityEngine;
using UnityEngine.AI; // Required for NavMesh Agent

public class MonsterAI : MonoBehaviour
{
    // Public variable to assign Ellen in the Inspector
    public Transform playerTarget;
    
    // Agent component reference
    private NavMeshAgent agent;

    // --- Chase Logic Variables ---
    public float chaseRange = 15f; // Distance before monster starts chasing
    public float attackRange = 2f; // Distance at which monster attacks
    
    void Start()
    {
        // Get the NavMeshAgent component attached to the monster
        agent = GetComponent<NavMeshAgent>();

        // Optional: Find Ellen automatically if playerTarget is null
        if (playerTarget == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                // We use the player's root Transform for NavMesh destination
                playerTarget = playerObject.transform;
            }
        }
    }

    void Update()
    {
        if (playerTarget != null)
        {
            // 1. Calculate distance to the player
            float distanceToPlayer = Vector3.Distance(transform.position, playerTarget.position);

            // 2. Check for Chase
            if (distanceToPlayer <= chaseRange)
            {
                // Set the player's position as the destination
                agent.SetDestination(playerTarget.position);

                // 3. Check for Attack
                if (distanceToPlayer <= attackRange)
                {
                    // Stop movement to "attack"
                    agent.isStopped = true;
                    AttackPlayer(); 
                }
                else
                {
                    // Continue chasing
                    agent.isStopped = false;
                }
            }
            else
            {
                // Out of range, remain idle at spawn or patrol (optional)
                agent.isStopped = true;
            }
        }
    }
    
    // --- Attack Function (Death Condition) ---
    void AttackPlayer()
    {
        // **IMPORTANT:** This is where you trigger the death mechanic
        Debug.Log("Monster attacked Ellen! Game Over."); 
        
        // Find Ellen's health/death script (usually on the Player object)
        // Since you are using 3D Game Kit Lite, you might need to find the 
        // appropriate script, such as 'PlayerCharacter.cs' or 'Health.cs'.
        
        // For a simple instant death:
        // You would typically call a function like:
        // FindObjectOfType<GameManager>().HandleGameOver();
        // OR
        // playerTarget.GetComponent<HealthScript>().Die();
    }
}