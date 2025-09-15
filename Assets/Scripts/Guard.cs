using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    NavMeshPath path;
    //[SerializeField] Rigidbody rb;

    [SerializeField] float speed;

    [SerializeField] Transform[] patrolPoints;
    Transform currentPatrolPoint;
    int patrolPointIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentPatrolPoint = patrolPoints[0];
        agent.SetDestination(currentPatrolPoint.position);
        //rb = GetComponent<Rigidbody>();
        path = new NavMeshPath();

        agent.CalculatePath(currentPatrolPoint.position, path);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, currentPatrolPoint.position);


        if(distance < 1)
        {
            patrolPointIndex++;

            patrolPointIndex %= patrolPoints.Length;

            currentPatrolPoint = patrolPoints[patrolPointIndex];
            agent.SetDestination(currentPatrolPoint.position);
        }
    }

    private void FixedUpdate()
    {
        
    }
}
