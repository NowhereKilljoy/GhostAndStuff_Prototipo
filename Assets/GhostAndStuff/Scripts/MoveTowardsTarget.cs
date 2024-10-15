using UnityEngine;

public class MoveTowardsTarget : MonoBehaviour
{
    public string targetTag = "Player";
    public float speed = 5f;
    public float detectionRange = 10f;
    public float patrolSpeed = 2f;
    public float knockbackDistance = 1f;
    public float knockbackDuration = 0.5f;
    public float maxFollowHeightDifference = 3f;
    public float uprightSpeed = 2f; // Velocidad para corregir la orientación al estar "acostado"

    private Transform targetTransform;
    private Vector3 patrolDirection;
    private bool isChasingPlayer = false;
    private bool isKnockedBack = false;
    private Vector3 knockbackDirection;
    private float knockbackTimer;

    //  public int damage = 100;



    void Start()
    {
        GameObject target = GameObject.FindGameObjectWithTag(targetTag);
        if (target != null)
        {
            targetTransform = target.transform;
        }
        else
        {
            Debug.LogWarning("No se encontró un objeto con el tag " + targetTag);
        }

        SetRandomPatrolDirection();
    }

    void Update()
    {
        CorrectOrientation(); // Asegurar que esté "de pie"

        if (isKnockedBack)
        {
            HandleKnockback();
        }
        else if (isChasingPlayer && targetTransform != null)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }

        float distanceToTarget = Vector3.Distance(transform.position, targetTransform.position);
        float heightDifference = Mathf.Abs(transform.position.y - targetTransform.position.y);

        if (distanceToTarget <= detectionRange && heightDifference <= maxFollowHeightDifference)
        {
            isChasingPlayer = true;
        }
        else
        {
            isChasingPlayer = false;
        }
    }

    // Función para corregir la orientación y asegurar que el enemigo siempre esté de pie
    private void CorrectOrientation()
    {
        Vector3 currentRotation = transform.eulerAngles;

        // Corregir los ejes X y Z para que el enemigo no esté acostado
        currentRotation.x = Mathf.LerpAngle(currentRotation.x, 0f, Time.deltaTime * uprightSpeed);
        currentRotation.z = Mathf.LerpAngle(currentRotation.z, 0f, Time.deltaTime * uprightSpeed);

        transform.eulerAngles = currentRotation;
    }

    private void Patrol()
    {
        if (isKnockedBack) return;

        transform.position += patrolDirection * patrolSpeed * Time.deltaTime;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, patrolDirection, out hit, 1f))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                SetRandomPatrolDirection();
            }
        }

        Quaternion targetRotation = Quaternion.LookRotation(patrolDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * patrolSpeed);
    }

    private void SetRandomPatrolDirection()
    {
        patrolDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
    }

    private void ChasePlayer()
    {
        if (isKnockedBack) return;

        Vector3 direction = (targetTransform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed);
    }

    private void HandleKnockback()
    {
        transform.position += knockbackDirection * Time.deltaTime / knockbackDuration;
        knockbackTimer -= Time.deltaTime;
        if (knockbackTimer <= 0)
        {
            isKnockedBack = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {


        if (collision.gameObject.CompareTag(targetTag))
        {
            knockbackDirection = (transform.position - collision.transform.position).normalized * knockbackDistance;
            knockbackTimer = knockbackDuration;
            isKnockedBack = true;
        }
    }
}
