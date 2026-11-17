using UnityEngine;

public class HealthCollectible:MonoBehaviour
{
    [SerializeField] private float healthValue;

    [SerializeField] private AudioClip heartClip;
    [SerializeField, Range(0f, 1f)] private float volume = 0.4f;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().AddHealth(healthValue);

            SoundFX.Play(heartClip);

            gameObject.SetActive(false);
        }
    }

}
