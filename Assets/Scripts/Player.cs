using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float walkSpeed;
    [SerializeField] float sprintSpeed;
    float currentSpeed;

    bool isSneaking = false;
    bool isSprinting = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMove()
    {

    }

    private void OnSneak()
    {
        isSneaking = !isSneaking;

        if (!isSneaking)
            gameObject.layer = LayerMask.NameToLayer("Player");
        else
            gameObject.layer = LayerMask.NameToLayer("Sneaking");
    }

    private void OnSprint()
    {
        isSprinting = !isSprinting;

        if (isSprinting && !isSneaking)
            currentSpeed = sprintSpeed;
        else
            currentSpeed = walkSpeed;
    }
}
