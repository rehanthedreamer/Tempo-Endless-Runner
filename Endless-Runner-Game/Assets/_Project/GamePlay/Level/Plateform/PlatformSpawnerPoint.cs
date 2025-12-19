using UnityEngine;

public class PlatformSpawnerPoint : MonoBehaviour
{
     void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Platform"))
        {
            PlatformManager.Instance.SpawnPlatform(transform.position);
        }
    }
}
