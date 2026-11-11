using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private float damage = 1f;
    [SerializeField] private float knockbackX = 6f;
    [SerializeField] private float knockbackY = 8f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        Health health = collision.GetComponent<Health>();
        if (health == null) return;

        float before = health.currentHealth;
        health.TakeDamage(damage);

        // отбрасываем, только если урон реально нанесён и персонаж жив
        if (health.currentHealth < before && health.currentHealth > 0)
        {
            float dir = Mathf.Sign(collision.transform.position.x - transform.position.x);
            collision.GetComponent<PlayerMovement>()
                     .Knockback(new Vector2(dir * knockbackX, knockbackY));
        }
    }
}
