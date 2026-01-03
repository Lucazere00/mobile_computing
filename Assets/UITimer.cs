using UnityEngine;
using TMPro;

public class UITimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float startDelay = 0f;

    public Rigidbody carRb; // ⬅️ AGGIUNTO

    private float elapsedTime = 0f;
    private bool timerRunning = false;

    void Start()
    {
        // 🔒 Blocca la macchina (non si muove)
        if (carRb != null)
        {
            carRb.constraints = RigidbodyConstraints.FreezePositionZ |
                                RigidbodyConstraints.FreezePositionX |
                                RigidbodyConstraints.FreezeRotation;
        }

        if (startDelay > 0f)
            Invoke(nameof(StartTimer), startDelay);
        else
            StartTimer();
    }

    void Update()
    {
        if (!timerRunning) return;

        elapsedTime += Time.deltaTime;

        if (timerText != null)
            timerText.text = GetFormattedTime();
    }

    public void StartTimer()
    {
        // 🔓 Sblocca la macchina solo ora
        if (carRb != null)
        {
            carRb.constraints = RigidbodyConstraints.None;
        }

        elapsedTime = 0f;
        timerRunning = true;
    }

     public void StopTimer()
    {
        timerRunning = false;
    }
    public float GetTime()
{
    return elapsedTime;
}


    public void ResetTimer()
    {
        elapsedTime = 0f;

        if (timerText != null)
            timerText.text = "00:00:000";
    }

    public string GetFormattedTime()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 1000f) % 1000f);

        return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
}
