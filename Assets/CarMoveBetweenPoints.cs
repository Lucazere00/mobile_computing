using UnityEngine;

public class CarMoveBetweenPoints : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 10f;
    private bool goingToB = true;

    void Update()
    {
        Transform target = goingToB ? pointB : pointA;

        // Muove la macchina verso il target
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        
        // Fa guardare la macchina verso la direzione di movimento
        transform.LookAt(target);

        // Quando è vicino al target, inverte direzione
        if (Vector3.Distance(transform.position, target.position) < 0.5f)
        {
            //goingToB = !goingToB;
            enabled = false; // disattiva lo script
        }
    }
}
