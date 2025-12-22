using UnityEngine;

public class PlatformReseter : MonoBehaviour
{
   /// <summary>
   /// if platform exit form this point platform will return to pool
   /// </summary>
   /// <param name="collision"></param>
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.GetComponent<PoolableObject>())
        {
            collision.GetComponent<PoolableObject>().OnReleaseRequest();
        }
    }
}
