using System.Collections;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public OnscreenInputs inputActions;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] CanvasGroup onScreenShield;
    Animator animator;
    bool sheildActive = false;
     void OnEnable()
    {
        inputActions.Enable();
    }
    
    void OnDisable()
    {
        inputActions.Disable();

    }
    private void Awake()
    {
        inputActions = new OnscreenInputs();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(GameManager.Instance.CurrentState != GameState.inGame) return;
        CheckShieldStatus();
        if(inputActions.MainPlayer.Sheild.WasPressedThisFrame() && !sheildActive)
            OnClickActiveShield();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.GetComponent<Obstacle>())
        {
             if(sheildActive)return;
            var obstacle = collision.collider.GetComponent<Obstacle>();
           StartCoroutine(OnPlayerHitBy(obstacle));
        }
    }

/// <summary>
/// Handle game and player state 
/// </summary>

    IEnumerator OnPlayerHitBy(Obstacle obstacle)
    {
       
        if(obstacle.GetObstacleType() == ObstacleType.Box)
        {
            animator.SetBool("isHit", true);
            // rest multiplier 
            DifficultyManager.Instance.ResetDifficulty();
            SoundManager.Instance.PlaySFX(SoundManager.Instance.hitSound);
        }
        else if(obstacle.GetObstacleType() == ObstacleType.Barrel)
        {
            animator.SetBool("isDead", true);
            DifficultyManager.Instance.StopDifficulty();
            
        }
        yield return new WaitForSeconds(.2f);
        if(obstacle.GetObstacleType() == ObstacleType.Barrel)
        {
            SoundManager.Instance.PlaySFX(SoundManager.Instance.deadSound);
            SoundManager.Instance.StopBGMusic();
            yield return new WaitForSeconds(1.5f);
            GameManager.Instance.SetState(GameState.GameOver);
            
        }else if(obstacle.GetObstacleType() == ObstacleType.Box)
        {
            animator.SetBool("isHit", false);
        }
    }
    public void ResetPlayer()
    {
        animator.SetBool("isDead", false);
    }

/// <summary>
/// cehck if sheild power up is purchased
/// </summary>
    void CheckShieldStatus()
    {
        onScreenShield.alpha = SaveService.GetShieldPower()? 1: .5f;
    }
/// <summary>
/// Sheild active and deactive
/// </summary>

    void OnClickActiveShield()
    {
         StartCoroutine(ActivateShield());
    }
    

    private IEnumerator ActivateShield()
    {
        sheildActive = true;
        spriteRenderer.enabled = sheildActive;
       
        // Wait for 10 seconds while shield is active
        float duration = 10f;
        float timer = 0f;

        while (timer < duration && SaveService.GetShieldPower() 
        && GameManager.Instance.CurrentState == GameState.inGame)
        {
            timer += Time.deltaTime;
            yield return null; 
        }
        sheildActive = false;
        spriteRenderer.enabled = sheildActive;

    }


}
