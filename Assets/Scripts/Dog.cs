using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Dog : Animal
{
    // --- Encapsulated movement stats (private + properties) ---
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float jumpForce = 4f;

    [SerializeField] private bool isGrounded = true; // kept private but shown in Inspector

    public float WalkSpeed
    {
        get => walkSpeed;
        set => walkSpeed = Mathf.Clamp(value, 0f, 10f);
    }

    public float RunSpeed
    {
        get => runSpeed;
        set => runSpeed = Mathf.Clamp(value, 0f, 15f);
    }

    public float JumpForce
    {
        get => jumpForce;
        set => jumpForce = Mathf.Clamp(value, 0f, 20f);
    }

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        // Optional: give the dog a default name
        if (string.IsNullOrWhiteSpace(AnimalName)) AnimalName = "Doggo";
    }

    // --- Polymorphism: dog-specific implementations ---
    public override void Walk(Vector3 input)
    {
        Vector3 move = input.normalized * WalkSpeed;
        MoveFlat(move);
    }

    public override void Run(Vector3 input)
    {
        Vector3 move = input.normalized * RunSpeed;
        MoveFlat(move);
    }

    public override void Jump()
    {
        if (!isGrounded) return;
        var v = rb.linearVelocity;         // Unity 6+; use rb.velocity on older versions
        v.y = JumpForce;
        rb.linearVelocity = v;
    }

    public override void Speak()
    {
        Debug.Log($"{AnimalName} says: Woof!");
    }

    // --- Helpers (Abstraction: hide movement math) ---
    private void MoveFlat(Vector3 flatVelocity)
    {
        var v = rb.linearVelocity;
        v.x = flatVelocity.x;
        v.z = flatVelocity.z;
        rb.linearVelocity = v;
    }

    // --- Simple grounding (feel free to upgrade to a sphere cast later) ---
    private void OnCollisionStay(Collision collision)
    {
        // If we’re touching anything roughly below us, consider grounded
        foreach (var contact in collision.contacts)
        {
            if (Vector3.Dot(contact.normal, Vector3.up) > 0.5f)
            {
                isGrounded = true;
                return;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // naive—but fine for a prototype
        isGrounded = false;
    }
}
