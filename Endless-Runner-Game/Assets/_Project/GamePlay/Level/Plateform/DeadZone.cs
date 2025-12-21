using System.Collections;
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
            StartCoroutine(GameOverAfterADealy());
        }
    }
    IEnumerator GameOverAfterADealy()
    {
        yield return new WaitForSeconds(.5f);
        GameManager.Instance.SetState(GameState.GameOver);
    }
}
