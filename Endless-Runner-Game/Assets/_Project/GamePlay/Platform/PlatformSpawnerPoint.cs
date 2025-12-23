using UnityEngine;

public class PlatformSpawnerPoint : MonoBehaviour
{
    /// <summary>
    /// If currnt platform exit next platform will spawn 
    /// </summary>
    /// <param name="collision"></param>
     void OnTriggerExit2D(Collider2D collision)
    {
        if(GameManager.Instance != null)
        if(collision.CompareTag("Platform") && GameManager.Instance.CurrentState == GameState.inGame)
        {
            
            PlatformManager.Instance.SpawnPlatform(transform.position);
        }
    }
}
