using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    [SerializeField] Transform target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        Vector3 forwardDirection = transform.forward;

        float dot = Vector3.Dot(forwardDirection, directionToTarget);

        //Debug.Log(dot);

        if(dot > 0.5f)
        {
            Debug.Log("Target spotted");
        }

        if(dot < -0.5f)
        {
            Debug.Log("Target behind");
        }
    }
}
