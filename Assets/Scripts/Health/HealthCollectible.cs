using UnityEngine;

public class HealthCollectible:MonoBehaviour
{
    [SerializeField] private float healthValue;

    [SerializeField] private AudioClip heartClip;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().AddHealth(healthValue);

            SoundFX.Play(heartClip, "collectible");

            gameObject.SetActive(false);
        }
    }

}
