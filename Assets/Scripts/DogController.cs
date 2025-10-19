using UnityEngine;

[RequireComponent(typeof(Dog))]
public class DogController : MonoBehaviour
{
    private Dog dog;

    private void Awake()
    {
        dog = GetComponent<Dog>();

        // Lock cursor to the screen center for better camera control
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // --- Input ---
        float h = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right
        float v = Input.GetAxisRaw("Vertical");   // W/S or Up/Down
        bool run = Input.GetKey(KeyCode.LeftShift);
        bool jump = Input.GetKeyDown(KeyCode.Space);

        // --- Camera-relative movement ---
        Transform cam = Camera.main.transform;
        Vector3 camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 camRight = cam.right;

        Vector3 moveInput = (camForward * v + camRight * h);

        // --- Movement calls ---
        if (run) dog.Run(moveInput);
        else dog.Walk(moveInput);

        if (jump) dog.Jump();

        // --- Facing direction ---
        Vector3 lookDir = new Vector3(moveInput.x, 0, moveInput.z);
        if (lookDir.sqrMagnitude > 0.01f)
        {
            Quaternion targetRot = Quaternion.LookRotation(lookDir);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * 10f);
        }

        // --- Bark ---
        if (Input.GetMouseButtonDown(0)) dog.Speak();
    }
}
