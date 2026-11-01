using UnityEngine;

public class Fruits_Collectible:MonoBehaviour
{
    [SerializeField] private int value = 1;

    public int Value => value;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Level_Management.Instance.AddFruit(value);
            gameObject.SetActive(false);

            //Debug.Log("Собрала фрукт");
            //gameObject.SetActive(false);
        }
    }
}
    

