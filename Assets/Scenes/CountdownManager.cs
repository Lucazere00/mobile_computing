using UnityEngine;
using TMPro;
using System.Collections;

public class CountdownManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject menuPanel;            // pannello menu principale
    public TextMeshProUGUI countdownText;   // testo countdown
    public GameObject timerUI;              // UI cronometro

    [Header("Gioco")]
    public GameObject car;                  // macchina giocatore
    public MonoBehaviour carController;     // script dei controlli macchina
    public GameObject track;                // percorso / waypoint
    public UITimer timer;                 // cronometro script

    [Header("Countdown")]
    public float countdownTime = 5f;        // secondi countdown

    // Funzione da collegare al pulsante Play
    public void StartCountdown()
    {
        // Nascondi il menu
        if (menuPanel != null)
            menuPanel.SetActive(false);

        // Mostra il testo countdown
        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(true);
        }

        // Blocca la macchina
        if (carController != null)
            carController.enabled = false;

        if (car != null)
            car.SetActive(true);

        // Disattiva cronometro
        if (timerUI != null)
            timerUI.SetActive(false);

        // Disattiva percorso se presente
        if (track != null)
            track.SetActive(false);

        // Avvia coroutine countdown
        StartCoroutine(CountdownCoroutine());

   
    }

    private IEnumerator CountdownCoroutine()
    {
        float time = countdownTime;

        while (time > 0)
        {
            if (countdownText != null)
                countdownText.text = Mathf.Ceil(time).ToString();

            yield return new WaitForSeconds(1f);
            time -= 1f;
        }

        if (countdownText != null)
            countdownText.text = "VIA!";

        yield return new WaitForSeconds(1f);

        // Nascondi il countdown
        if (countdownText != null)
            countdownText.gameObject.SetActive(false);

        // Attiva macchina e percorso
        if (carController != null)
            carController.enabled = true;

        if (track != null)
            track.SetActive(true);

        // Attiva e avvia cronometro
        if (timerUI != null)
            timerUI.SetActive(true);

        if (timer != null)
            timer.StartTimer();

        Debug.Log("GIOCO INIZIATO!");
    }
}
