using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour
{
    public Image endScreenImage;
    public Button exitButton;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.StartTeleportation(endScreenImage.gameObject, exitButton.gameObject);
            }
        }
    }
}
