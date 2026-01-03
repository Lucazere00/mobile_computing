using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
 
public class FinishLine : MonoBehaviour
{
    [Header("Riferimenti Gara")]
    public Rigidbody carRigidbody;       // La macchina (rigidbody)
    public Transform finishPoint;        // Punto del traguardo
    public float reachDistance = 2f;     // Distanza di rilevamento
    public UITimer timer;                // Riferimento al cronometro
 
    [Header("UI Fine Gara")]
    public GameObject finishPanel;       // pannello finale
    public TextMeshProUGUI finishText;   // testo "FINE"
    public TextMeshProUGUI timeText;     // testo con il tempo finale
 
    public TextMeshProUGUI topScoreText;
 
   
    private bool firstPass = true;
     private bool finished = false;
     private bool insideFinish = false;
 
     string FormatTime(float t)
{
    int minutes = Mathf.FloorToInt(t / 60f);
    int seconds = Mathf.FloorToInt(t % 60f);
    int milliseconds = Mathf.FloorToInt((t * 1000f) % 1000f);
    return $"{minutes:00}:{seconds:00}:{milliseconds:000}";
}
 
float GetBestTime()
{
    if (PlayerPrefs.HasKey("BestTime"))
        return PlayerPrefs.GetFloat("BestTime");
    else
        return 0f; // prima volta
}
 
void SaveBestTime(float t)
{
    float best = GetBestTime();
 
    // Se è la prima volta o il nuovo tempo è migliore
    if (best == 0f || t < best)
    {
        PlayerPrefs.SetFloat("BestTime", t);
        PlayerPrefs.Save(); // <-- fondamentale per salvare sul disco
    }
}

void Start()
{
    if (topScoreText != null)
        topScoreText.text = "BEST: " + FormatTime(GetBestTime());
}
 
 
 void Update()
{
    if (finished) return;
 
    BoxCollider box = GetComponent<BoxCollider>();
    bool isInside = box.bounds.Contains(carRigidbody.position);
 
    if (isInside && !insideFinish)
    {
        // La macchina entra nel cubo
        insideFinish = true;
 
        if (firstPass)
        {
            firstPass = false; // ignora la prima volta
            return;
        }
 
        // La seconda volta o oltre termina
        finished = true;
        FinishRace();
    }
    else if (!isInside && insideFinish)
    {
        // La macchina esce dal cubo
        insideFinish = false;
    }
}
 
 
    void FinishRace()
    {
        Debug.Log("GARA FINITA! La macchina ha attraversato il traguardo.");
 
        // 1️ Ferma il cronometro
        if (timer != null)
            timer.StopTimer();
           
float finalTime = timer.GetTime();
SaveBestTime(finalTime);
 
if (topScoreText != null)
    topScoreText.text = "BEST: " + FormatTime(GetBestTime());
 
        // 2️ Blocca la macchina
        if (carRigidbody != null)
        {
            carRigidbody.linearVelocity = Vector3.zero;
            carRigidbody.angularVelocity = Vector3.zero;
 
            carRigidbody.constraints =
                RigidbodyConstraints.FreezePositionX |
                RigidbodyConstraints.FreezePositionZ |
                RigidbodyConstraints.FreezeRotation;
        }
 
        // 3️ Mostra UI fine gara
        if (finishPanel != null)
            finishPanel.SetActive(true);
 
        if (finishText != null)
            finishText.text = "FINE";
 
        if (timeText != null && timer != null)
            timeText.text = timer.GetFormattedTime();
    }
    public void RestartGame()
{
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
 
}
 