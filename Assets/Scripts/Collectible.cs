using UnityEngine;

public class Collectible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided");
        if (collision.tag == "Player")
        {
            Debug.Log("Collided with player");
            CollectibleCounter.instance.IncrementCollected();
            Destroy(gameObject);
        }
    }
}
