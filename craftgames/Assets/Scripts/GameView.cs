using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    [SerializeField] private Image fillBarProgress;
    private float lastValue;
    void Update()
    {
        if (!GameManager.singleton.GameStarted)
            return;

        float travelledDistance = GameManager.singleton.EntireDistance - GameManager.singleton.DistanceLeft;
        float value = travelledDistance / GameManager.singleton.EntireDistance;
        // Debug.Log($"Travelled Distance: {travelledDistance}, Entire Distance: {GameManager.singleton.EntireDistance}, Value: {value}");

        if (GameManager.singleton.GameEnded && value < lastValue)
            return;

        fillBarProgress.fillAmount = Mathf.Lerp(fillBarProgress.fillAmount, value, Time.deltaTime * 5);

        lastValue = value;
    }
}
