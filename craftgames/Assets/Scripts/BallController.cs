using UnityEngine;
using UnityEngine.InputSystem;  // ①

public class BallController : MonoBehaviour
{
    [SerializeField] private float thrust = 75f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float wallDistance = 5f;
    [SerializeField] private float minCameraDistance = 3f;
    [SerializeField] private float constantSpeed = 2f;

    private Vector3 moveDirection = Vector3.forward;
    private Vector2 lastPointerPos;
    private bool isPressed;
    private Vector2 currentPos;

    void Update()
    {
        // 1) Read pointer (touch or mouse) each frame:
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            isPressed   = true;
            currentPos  = Touchscreen.current.primaryTouch.position.ReadValue();
        }
        else if (Mouse.current != null && Mouse.current.leftButton.isPressed)
        {
            isPressed   = true;
            currentPos  = Mouse.current.position.ReadValue();
        }
        else
        {
            isPressed   = false;
            lastPointerPos = Vector2.zero;
        }

        // currentPos = Camera.main.ScreenToViewportPoint(currentPos);


        // 2) Start the game on first press
        if (!GameManager.singleton.GameStarted && isPressed)
        {
            GameManager.singleton.StartGame();
        }

        // 3) Skip input if game ended
        if (GameManager.singleton.GameEnded) return;

        // 4) Drag‐to‐move logic
        if (isPressed)
        {
            if (lastPointerPos == Vector2.zero)
                lastPointerPos = currentPos;

            Vector2 delta = currentPos - lastPointerPos;
            lastPointerPos = currentPos;

            Vector3 force = new Vector3(delta.x, 0, delta.y) * thrust;
            rb.AddForce(force);
        }
    }

    void LateUpdate()
    {
        // clamp to walls & camera back‐edge as before…
        Vector3 p = transform.position;
        p.x = Mathf.Clamp(p.x, -wallDistance, wallDistance);
        float minZ = Camera.main.transform.position.z + minCameraDistance;
        if (p.z < minZ) p.z = minZ;
        transform.position = p;
    }

    void FixedUpdate()
    {
        if (GameManager.singleton.GameEnded)
            return;

        if (GameManager.singleton.GameStarted)
            rb.MovePosition(transform.position + moveDirection * constantSpeed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (GameManager.singleton.GameEnded) return;
        if (collision.gameObject.CompareTag("Enemy"))
            GameManager.singleton.EndGame(false);
    }
}
