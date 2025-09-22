using UnityEngine;
using UnityEngine.Events;

public class Sensor : MonoBehaviour
{
    [SerializeField] private Guard guard;
    [SerializeField] UnityEvent<Collider> OnHeard;

    private void OnTriggerStay(Collider other)
    {
        OnHeard.Invoke(other);

        //if(other.gameObject.GetComponent<Player>() == null)
        //{
            guard.investigationTime = 1 * Time.deltaTime;
            if(guard.investigationTime > guard.investigationDuration)
            {
                guard.investigationTime = 0;
                guard.state = GuardStates.WANDER;
            }
        //}
    }
}
