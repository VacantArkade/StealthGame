using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    NavMeshPath path;
    //[SerializeField] Rigidbody rb;
    [SerializeField] GameObject player;

    [SerializeField] float speed;

    [SerializeField] Transform[] patrolPoints;
    Transform currentPatrolPoint;
    int patrolPointIndex = 0;

    [SerializeField] float visionRadius;
    [SerializeField] LayerMask environmentLayer;

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

        Debug.DrawLine(transform.position, transform.position + (transform.forward * 5), Color.red);
    }

    private void FixedUpdate()
    {
        
    }

    private void HeardSomething(Collider thingWeHeard)
    {
        if (thingWeHeard.GetComponent<Player>() != null)
        {
            Debug.Log("Did you hear that?");
            Vector3 guardForward = transform.forward;
        }
    }

    public void SawSomething(Collider thingWeSaw)
    {
        if(thingWeSaw.GetComponent<Player>() != null)
        {
            Vector3 guardForward = transform.forward;
            guardForward.y = 0;
            guardForward.Normalize();
            Vector3 lineToPlayer = (thingWeSaw.transform.position - transform.position).normalized;
            lineToPlayer.y = 0;
            lineToPlayer.Normalize();
            float dot = Vector3.Dot(guardForward, lineToPlayer);

            if(dot > visionRadius)
            {
                RaycastHit hit;
                if(Physics.Raycast(transform.position, lineToPlayer, out hit, 1000, environmentLayer))
                {
                    Debug.Log("Saw a wall");
                }
                else
                    Debug.Log("Saw player");
            }

            else
            {
                Debug.Log("Did not see player");
            }
        }
    }
}
