using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public enum GuardStates
{
    WANDER,
    INVESTIGATE,
    PURSUE
}

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

    //[SerializeField] bool isAlerted;
    [SerializeField] public float investigationDuration;
    public float investigationTime = 0;

    [SerializeField] public GuardStates state = GuardStates.WANDER;

    bool investigating = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentPatrolPoint = patrolPoints[0];
        /*agent.SetDestination(currentPatrolPoint.position);
        path = new NavMeshPath();

        agent.CalculatePath(currentPatrolPoint.position, path);*/
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position, transform.position + (transform.forward * 5), Color.red);

        switch (state)
        {
            case GuardStates.WANDER:
                UpdateWander();
                break;
            case GuardStates.INVESTIGATE:
                UpdateInvestigate();
                break;
            case GuardStates.PURSUE:
                UpdatePursue();
                break;
        }
    }

    private void HeardSomething(Collider thingWeHeard)
    {
        //isAlerted = true;
        if (thingWeHeard.GetComponent<Player>() != null)
        {
            Debug.Log("Did you hear that?");
            //Vector3 guardForward = transform.forward;
            state = GuardStates.INVESTIGATE;
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

            if (dot > visionRadius)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, lineToPlayer, out hit, 1000, environmentLayer))
                {
                    Debug.Log("Saw a wall");
                }
                else
                {
                    Debug.Log("Saw player");
                    state = GuardStates.PURSUE;
                    investigationTime = 0;
                }
            }

            else
            {
                Debug.Log("Did not see player");
                state = GuardStates.INVESTIGATE;
                investigationTime += 1 * Time.deltaTime;
                if (investigationTime > investigationDuration)
                {
                    investigationTime = 0;
                    state = GuardStates.WANDER;
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() == null)
        {
            investigationTime += 1 * Time.deltaTime;
            if (investigationTime > investigationDuration)
            {
                investigationTime = 0;
                state = GuardStates.WANDER;
            }
        }
    }


    void UpdateWander()
    {
        agent.SetDestination(currentPatrolPoint.position);
        path = new NavMeshPath();

        agent.CalculatePath(currentPatrolPoint.position, path);

        float distance = Vector3.Distance(transform.position, currentPatrolPoint.position);

        if (distance < 1)
        {
            patrolPointIndex++;

            patrolPointIndex %= patrolPoints.Length;

            currentPatrolPoint = patrolPoints[patrolPointIndex];
            agent.SetDestination(currentPatrolPoint.position);
        }
    }

    void UpdateInvestigate()
    {
        if(!investigating)
        {
            investigating = true;
            StartCoroutine(DoInvestigate());
        }

        transform.Rotate(transform.up, 20 * Time.deltaTime);
    }

    IEnumerator DoInvestigate()
    {
        yield return new WaitForSeconds(investigationDuration);

        state = GuardStates.WANDER;
        investigating = false;
        investigationTime = 0;
    }

    void UpdatePursue()
    {
        agent.SetDestination(player.transform.position);
    }
}
