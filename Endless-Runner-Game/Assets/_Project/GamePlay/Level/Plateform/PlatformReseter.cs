using UnityEngine;

public class PlatformReseter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.GetComponent<PoolableObject>())
        {
            collision.GetComponent<PoolableObject>().OnReleaseRequest();
        }
    }
}
