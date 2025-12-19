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
        if (collision.collider.CompareTag("DeadZone"))
        {
            // Game end
            Debug.Log("Player Dead");
        }
    }


    IEnumerator OnPlayerHitBy(ObstacleType obstacleType)
    {
        if(obstacleType == ObstacleType.Barrel)
        {
            animator.SetBool("isDead", true);
        }else if(obstacleType == ObstacleType.Box)
        {
            animator.SetBool("isHit", true);
        }
        yield return new WaitForSeconds(1f);
        if(obstacleType == ObstacleType.Barrel)
        {
            animator.SetBool("isDead", true);
            // Game end
            Debug.Log("Player Dead");
        }else if(obstacleType == ObstacleType.Box)
        {
            animator.SetBool("isHit", false);
            // rest multiplier 
            DifficultyManager.Instance.ResetDifficulty();
        }
    }
}
