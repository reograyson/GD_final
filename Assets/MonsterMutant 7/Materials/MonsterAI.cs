using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class MonsterAI : MonoBehaviour
{
    public Transform playerTarget;
    private NavMeshAgent agent;
    private Vector3 startPosition; // To remember where he started

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        // 1. Remember where the monster spawned
        startPosition = transform.position;

        // 2. Find the player automatically
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) playerTarget = p.transform;
    }

    void Update()
    {
        if (playerTarget == null) return;

        // --- LOGIC A: PLAYER IS SAFE ---
        if (SafeZone.isPlayerSafe)
        {
            // Don't stop moving! Instead, walk back to the start position.
            agent.isStopped = false;
            agent.SetDestination(startPosition);
            return;
        }

        // --- LOGIC B: PLAYER IS OUTSIDE (CHASE) ---
        agent.isStopped = false;
        agent.SetDestination(playerTarget.position);

        // --- LOGIC C: CATCH PLAYER ---
        // Only kill if the player is NOT safe
        float distance = Vector3.Distance(transform.position, playerTarget.position);
        if (distance < 1.5f && !SafeZone.isPlayerSafe)
        {
            Debug.Log("CAUGHT!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}