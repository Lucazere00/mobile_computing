using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [Header("UI Menu")]
    public GameObject mainMenuUI;      // menu principale
    public GameObject[] otherUI;       // altri canvas/pannelli da nascondere

    [Header("Oggetti Gioco")]
    public GameObject car;             // macchina giocatore
    public GameObject track;           // percorso / waypoint
    public UITimer timer;              // cronometro
    public GameObject timerUI;         // GameObject UI del cronometro
public Rigidbody carRb;
    public void PlayGame()
    {
        // Nasconde il menu principale
        if (mainMenuUI != null)
            mainMenuUI.SetActive(false);

        // Nasconde tutti gli altri canvas/pannelli
        if (otherUI != null)
        {
            foreach (GameObject ui in otherUI)
            {
                if (ui != null)
                    ui.SetActive(false);
            }
        }

        if (carRb != null)
    {
        carRb.constraints = RigidbodyConstraints.FreezePositionX |
                            RigidbodyConstraints.FreezePositionZ |
                            RigidbodyConstraints.FreezeRotation;
    }

        // Mostra e avvia cronometro
        if (timerUI != null)
            timerUI.SetActive(true);

        if (timer != null)
            timer.StartTimer();

        // Attiva macchina e percorso
        if (car != null) car.SetActive(true);
        if (track != null) track.SetActive(true);

        Debug.Log("GIOCO INIZIATO!");
    }
}
