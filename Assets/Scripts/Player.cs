using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    PlayerInput input;
    InputAction action;

    [SerializeField] Rigidbody rb;
    [SerializeField] float walkSpeed;
    [SerializeField] float sprintSpeed;
    float currentSpeed;

    [SerializeField] Material matPlayer;
    [SerializeField] Material matSneak;
    Renderer currentMat;

    bool isSneaking = false;
    bool isSprinting = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        input = GetComponent<PlayerInput>();
        action = input.actions.FindAction("WASD");

        currentSpeed = walkSpeed;

        rb = GetComponent<Rigidbody>();
        currentMat = GetComponent<Renderer>();
        currentMat.material = matPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        OnWASD();
    }

    private void FixedUpdate()
    {

    }

    public void OnWASD()
    {
        Vector2 direction = action.ReadValue<Vector2>();
        transform.position += new Vector3(direction.x, 0, direction.y) * currentSpeed * Time.deltaTime;
    }

    public void OnSneak()
    {
        if (isSprinting) return;

        isSneaking = !isSneaking;

        if (!isSneaking)
        {
            currentMat.material = matPlayer;
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
        else
        {
            currentMat.material = matSneak;
            gameObject.layer = LayerMask.NameToLayer("Sneaking");
        }
    }

    public void OnSprint()
    {
        if (isSneaking) return;

        isSprinting = !isSprinting;

        if (isSprinting && !isSneaking)
            currentSpeed = sprintSpeed;
        else
            currentSpeed = walkSpeed;
    }
}
