using UnityEngine;

public class DeadZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Game end
            Debug.Log("Player Dead");
            SoundManager.Instance.PlaySFX(SoundManager.Instance.deadSound);
            SoundManager.Instance.StopBGMusic();
            GameManager.Instance.SetState(GameState.GameOver);
        }
    }
}
