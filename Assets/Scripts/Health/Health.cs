using UnityEngine;
using UnityEngine.Rendering;

public class Health:MonoBehaviour {
    [SerializeField] private float startingHealth;
    public float currentHealth {get; private set;}
    private Animator anim;
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
            dead = true;
        }
    }

    private System.Collections.IEnumerator Invulnerability()
    {
        invulnerable = true;
        yield return new WaitForSeconds(iFramesDuration);
        invulnerable = false;
    }


    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }


    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
}
