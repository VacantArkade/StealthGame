using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;

    [SerializeField] Material matPlayer;
    [SerializeField] Material matSneak;

    bool isSneaking = false;

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

    }
}
