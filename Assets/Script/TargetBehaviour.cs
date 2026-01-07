using UnityEngine;
using System.Collections;

public class TargetBehaviour : MonoBehaviour
{
    public Sprite[] possibleSprites; 
    public Sprite shotSprite;        
    public bool isEnemy;             
    
    private SpriteRenderer sr;
    private Animator anim;
    private bool isHit = false;
    private Coroutine autoWithdrawCoroutine;

    void Awake()
    {
        // Component reference acquisition from child objects
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();

        if (possibleSprites.Length > 0)
        {
            sr.sprite = possibleSprites[Random.Range(0, possibleSprites.Length)];
        }
    }

    void Start()
    {
        // Start of the countdown for automatic withdrawal
        autoWithdrawCoroutine = StartCoroutine(AutoWithdrawSequence());
    }

    IEnumerator AutoWithdrawSequence()
    {
        // Random wait duration between 10 and 20 seconds
        float waitTime = Random.Range(10f, 20f);
        yield return new WaitForSeconds(waitTime);

        if (!isHit)
        {
            StartCoroutine(WithdrawSequence(false));
        }
    }

    public void OnShot()
    {
        if (isHit) return;
        isHit = true;

        if (autoWithdrawCoroutine != null) StopCoroutine(autoWithdrawCoroutine);
        StartCoroutine(WithdrawSequence(true));
    }

    IEnumerator WithdrawSequence(bool wasShot)
    {
        if (wasShot)
        {
            // Visual feedback and score update for hit targets
            sr.sprite = shotSprite; 
            int points = isEnemy ? 1 : -1;
            if(GameManager.instance != null) GameManager.instance.AddScore(points);
            
            yield return new WaitForSeconds(0.5f);
        }

        // Trigger execution for the downward animation
        if (anim != null)
        {
            anim.SetTrigger("Hide");
        }

        // Wait for the closing animation duration before destruction
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
