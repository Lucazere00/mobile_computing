using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarPathFollower : MonoBehaviour
{
    [Header("Percorso")]
    public Transform[] waypoints;

    [Header("Parametri Movimento")]
    public float speed = 10f;
    public float rotationSpeed = 5f;
    public float reachDistance = 1f;
    public bool loop = true;

    private int currentPoint = 0;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false; // Usa la fisica
    }

    void FixedUpdate()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        Transform target = waypoints[currentPoint];
        Vector3 direction = (target.position - transform.position);
        direction.y = 0f;
        Vector3 moveDir = direction.normalized;

        // Ruota verso il target
        if (moveDir.sqrMagnitude > 0.001f)
        {
            Quaternion lookRot = Quaternion.LookRotation(moveDir);
            Quaternion smoothRot = Quaternion.Slerp(rb.rotation, lookRot, rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(smoothRot);
        }

        // Muove con la fisica
        Vector3 move = moveDir * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);

        // Se è vicino al waypoint → passa al successivo
        if (Vector3.Distance(transform.position, target.position) <= reachDistance)
        {
            currentPoint++;
            if (currentPoint >= waypoints.Length)
            {
                if (loop)
                    currentPoint = 0;
                else
                    enabled = false;
            }
        }
    }

    void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Length < 2) return;

        Gizmos.color = Color.yellow;
        for (int i = 0; i < waypoints.Length - 1; i++)
            Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);

        if (loop)
            Gizmos.DrawLine(waypoints[waypoints.Length - 1].position, waypoints[0].position);
    }
}
