using UnityEngine;

public class PlatformSpawnerPoint : MonoBehaviour
{
     void OnTriggerExit2D(Collider2D collision)
    {

        if(collision.CompareTag("Platform") && GameManager.Instance.CurrentState == GameState.inGame)
        {
            PlatformManager.Instance.SpawnPlatform(transform.position);
        }
    }
}
