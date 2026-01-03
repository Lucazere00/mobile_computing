using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI Fine Gara")]
    public GameObject finishPanel;       // pannello UI (FinishPanel)
    public TextMeshProUGUI finishTitle;  // FinishTitle
    public TextMeshProUGUI finishTime;   // FinishTime

    [Header("Riferimenti Gioco")]
    public Rigidbody carRb;              // rigidbody della macchina (drag from inspector)
    public UITimer timer;                // lo script UITimer

    // Chiamare questa funzione quando la gara termina
    public void FinishRace()
    {
        // 1) Ferma timer
        if (timer != null)
        {
            timer.StopTimer();
        }

        // 2) Blocca la macchina: azzera velocità e applica constraints
        if (carRb != null)
        {
            carRb.linearVelocity = Vector3.zero;
            carRb.angularVelocity = Vector3.zero;

            // blocca posizioni X/Z e rotazioni (modifica se vuoi anche Y)
            carRb.constraints = RigidbodyConstraints.FreezePositionX |
                                RigidbodyConstraints.FreezePositionZ |
                                RigidbodyConstraints.FreezeRotation;
        }

        // 3) Mostra pannello di fine gara
        if (finishPanel != null)
            finishPanel.SetActive(true);

        // 4) Imposta testi UI
        if (finishTitle != null)
            finishTitle.text = "FINE";

        if (finishTime != null)
        {
            if (timer != null)
                finishTime.text = timer.GetFormattedTime();
            else
                finishTime.text = "00:00:000";
        }

        Debug.Log("GARA TERMINATA - tempo: " + (timer != null ? timer.GetFormattedTime() : "n/a"));
    }
}
