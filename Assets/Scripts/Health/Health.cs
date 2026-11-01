using UnityEngine;
using UnityEngine.Rendering;

public class Health:MonoBehaviour {
    [SerializeField] private float startingHealth;
    public float currentHealth {get; private set;}
    private Animator anim;
    private SpriteRenderer spriteRend;
    [SerializeField, Range(0f, 1f)] private float invulnerableAlpha = 0.5f;
    private bool dead;
    [SerializeField] private float iFramesDuration = 1f;
    private bool invulnerable;

    public void TakeDamage(float _damage)
    {
        if (invulnerable || dead) return;

        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
        }
        else
        {
            anim.SetTrigger("die");
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            GetComponent<Rigidbody2D>().gravityScale = 0f;
            dead = true;
        }
    }

    private System.Collections.IEnumerator Invulnerability()
    {
        invulnerable = true;
        spriteRend.color = new Color(1f, 1f, 1f, invulnerableAlpha);

        yield return new WaitForSeconds(iFramesDuration);
        spriteRend.color = Color.white;

        invulnerable = false;
    }



    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }


    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    public void OnDeathAnimationEnd()
    {
        Level_Management.Instance.GameOver();
    }

}
