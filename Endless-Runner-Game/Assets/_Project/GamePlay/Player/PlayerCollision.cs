using System.Collections;
using ERG;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
Animator animator;
    void Start() 
    {
    animator = GetComponent<Animator>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.GetComponent<Obstacle>())
        {
            var obstacle = collision.collider.GetComponent<Obstacle>();
           StartCoroutine(OnPlayerHitBy(obstacle.GetObstacleType()));
        }
    }


    IEnumerator OnPlayerHitBy(ObstacleType obstacleType)
    {
        if(obstacleType == ObstacleType.Box)
        {
            animator.SetBool("isHit", true);
            // rest multiplier 
            DifficultyManager.Instance.ResetDifficulty();
            SoundManager.Instance.PlaySFX(SoundManager.Instance.hitSound);
        }
        else if(obstacleType == ObstacleType.Barrel)
        {
            animator.SetBool("isDead", true);
            DifficultyManager.Instance.StopDifficulty();
            
        }
        yield return new WaitForSeconds(.2f);
        if(obstacleType == ObstacleType.Barrel)
        {
            SoundManager.Instance.PlaySFX(SoundManager.Instance.deadSound);
            SoundManager.Instance.StopBGMusic();
            GameManager.Instance.SetState(GameState.GameOver);
            Debug.Log("Player Dead");
        }else if(obstacleType == ObstacleType.Box)
        {
            animator.SetBool("isHit", false);
        }
    }
}
