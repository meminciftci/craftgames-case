using UnityEngine;

public class Goal : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Goal Triggered by: " + other.name);
        if (other.CompareTag("Player"))
        {
            // Check if the game has ended
            if (GameManager.singleton.GameEnded)
                return;

            // End the game with a win
            GameManager.singleton.EndGame(true);
        }
    }
}
