using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float moveSpeed = 4f;
    private Vector3 moveDirection = Vector3.forward;

    void Update()
    {
        if(!GameManager.singleton.GameStarted || GameManager.singleton.GameEnded)
            return;
        transform.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime, Space.World);
    }
}
