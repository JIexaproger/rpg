using UnityEngine;

public class Fireball : MonoBehaviour
{
    [Header("Параметры полёта")]
    [SerializeField] private float speed = 20f;
    
    [Header("Параметры взрыва")]
    [SerializeField] private float explosionRadius = 3f;
    [SerializeField] private float damage = 50f;
    [SerializeField] private GameObject explosionVFX; // Префаб эффекта взрыва (опционально)
    
    [Header("Время жизни")]
    [SerializeField] private float lifetime = 5f;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
    }

    public void Launch(Vector3 direction)
    {
        direction = direction.normalized;
        transform.rotation = Quaternion.LookRotation(direction);
        _rb.linearVelocity = direction * speed;

        // Автоуничтожение через время жизни
        Destroy(gameObject, lifetime);
    }
    
    public void Launch()
    {
        _rb.linearVelocity = transform.forward * speed;
        
        // Автоуничтожение через время жизни
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    private void Explode()
    {
        // Эффект взрыва
        if (explosionVFX != null)
        {
            Instantiate(explosionVFX, transform.position, Quaternion.identity);
        }

        // Нанесение урона по области
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius, LayerMask.GetMask("Enemy"));
        foreach (Collider hit in hits)
        {
            Enemy enemyHealth = hit.GetComponentInParent<Enemy>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }

        // Уничтожение фаербола
        Destroy(gameObject);
    }

    // Визуализация радиуса в редакторе (опционально)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}