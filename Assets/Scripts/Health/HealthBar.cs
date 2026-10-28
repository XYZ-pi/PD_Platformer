using UnityEngine;
using UnityEngine.UI;   

public class HealthBar: MonoBehaviour{
    [SerializeField] Health playerHealth;
    [SerializeField] Image totalHealthBar;
    [SerializeField] Image currentHealthBar;

    private void Start()
    {
       totalHealthBar.fillAmount = playerHealth.currentHealth / 10;
    }

    private void Update()
    {
        currentHealthBar.fillAmount = playerHealth.currentHealth / 10;
    }

}
