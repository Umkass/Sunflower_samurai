using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class SunflowerSamurai : MonoBehaviour
{

    public static SunflowerSamurai instance = null;
    [SerializeField]
    GameObject[] characters;
    Animator animator;
    Rigidbody2D rb;
    [SerializeField]
    float moveSpeed = 6.5f;
    [SerializeField]
    float jumpForce = 18f;
    [SerializeField]
    float defaultJumpForce = 18f;
    [SerializeField]
    bool isJumping;
    [SerializeField]
    float jumpTimeCounter;
    [SerializeField]
    float jumpTime = 0.18f;
    LayerMask whatIsGround;
    [SerializeField]
    GameObject groundCheck;
    [SerializeField]
    bool isGround = false;
    float groundRadius = 0.2f;
    [SerializeField]
    public int extraJumpCount;
    [SerializeField]
    int maxExtraJumpCount;
    public GameState state = GameState.Starting;
    int counterCollisions = 0;
    public bool Gravity = true;
    [SerializeField]
    GameObject shadowCharacter;
    Light lightCharacter;
    public int ExtraJumpCount { get => extraJumpCount; set => extraJumpCount = value; }
    public void Move()
    {
        if (isGround)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
    }
    public void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }
    public void CheckGround()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.transform.position, groundRadius, whatIsGround);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Die") && !(state == GameState.Dead))
        {
            state = GameState.Dead;
            StartCoroutine(coroutineDie(other.transform));
            if (SceneManager.GetActiveScene().buildIndex == 8)
            {
                PlayerPrefs.SetInt("Coins", UIManager.instance.allCoins);
            }
        }
        if (other.CompareTag("LevelComplete") && !(state == GameState.LevelComplete))
        {
            state = GameState.LevelComplete;
            animator.enabled = false;
            StartCoroutine(coroutineAnimLevelComplete());
            PlayerPrefs.SetInt("Coins", UIManager.instance.allCoins);
        }
        if (other.CompareTag("Cave") && !(state == GameState.Dead))
        {
            lightCharacter.enabled = !lightCharacter.enabled;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        counterCollisions++;
        if (collision.gameObject.CompareTag("Ground") && counterCollisions > 1)
            SoundManager.instance.FallCharacterPlay();
    }
    IEnumerator coroutineAnimLevelComplete()
    {
        moveSpeed = 0;
        defaultJumpForce = 0;
        if (shadowCharacter != null)
        {
            Destroy(shadowCharacter);
        }
        SoundManager.instance.LevelCompleteCharacterPlay();
        int currentLevel = SceneManager.GetActiveScene().buildIndex - 1;
        AnalyticsResult analyticsResult = Analytics.CustomEvent("Level Complete",
            new Dictionary<string, object>
            {
                {"Level Complete", currentLevel}
            });
        Debug.Log(analyticsResult);
        yield return new WaitForSeconds(1.3f);
        Time.timeScale = 0;
        gameObject.SetActive(false);
        UIManager.instance.LevelComplete();
    }
    IEnumerator coroutineDie(Transform transformOther)
    {
        moveSpeed = 0;
        jumpForce = 0;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePosition;
        if (shadowCharacter != null)
        {
            Destroy(shadowCharacter);
        }
        animator.enabled = false;
        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
        SoundManager.instance.DieCharacterPlay();
        ParticleController.instance.DustEffectStop();
        ParticleController.instance.SmokeEffectStop();
        ParticleController.instance.DeathEffectStart(transformOther);
        int currentLevel = SceneManager.GetActiveScene().buildIndex - 1;
        AnalyticsResult analyticsResult = Analytics.CustomEvent("Die",
            new Dictionary<string, object>
            {
                {"Level", currentLevel},
               {"Position", Mathf.RoundToInt(transform.position.x /10)}
            });
        Debug.Log(analyticsResult);
        Debug.Log(Mathf.RoundToInt(transform.position.x / 10));
        yield return new WaitForSeconds(0.9f);
        Time.timeScale = 0;
        UIManager.instance.Die();
    }
    public void DefaultExtraJumpCount()
    {
        extraJumpCount = maxExtraJumpCount;
    }
    private void SetExtraJumpCount(int ExtraJumpCount)
    {
        extraJumpCount += ExtraJumpCount;
    }
    public void TouchingPetal()
    {
        jumpForce = 2f * defaultJumpForce;
        SetExtraJumpCount(1);
    }
    public void UnTouchingPetal()
    {
        jumpForce = defaultJumpForce;
        SetExtraJumpCount(0);
    }
    public void TouchingTrampoline()
    {
        jumpForce = 16.5f * defaultJumpForce;
        Jump();
    }
    public void UnTouchingTrampoline()
    {
        jumpForce = defaultJumpForce;
    }
    public void TouchingPetalInfinity()
    {
        jumpForce = 5f * defaultJumpForce;
        Jump();
        jumpForce = defaultJumpForce;
        SetExtraJumpCount(34);
    }
    public void UnTouchingPetalInfinity()
    {
        jumpForce = defaultJumpForce;
        DefaultExtraJumpCount();
    }
    public void ChangeGravity()
    {
        rb.gravityScale *= -1;
        SpriteRenderer spriteflip = gameObject.GetComponentInChildren<SpriteRenderer>();
        lightCharacter.transform.position = new Vector3(lightCharacter.transform.position.x, lightCharacter.transform.position.y, -lightCharacter.transform.position.z);
        ParticleController.instance.Dust.transform.position = new Vector3(ParticleController.instance.Dust.transform.position.x, ParticleController.instance.Dust.transform.position.y, -ParticleController.instance.Dust.transform.position.z);
        ParticleController.instance.Smoke.transform.position = new Vector3(ParticleController.instance.Smoke.transform.position.x, ParticleController.instance.Smoke.transform.position.y, -ParticleController.instance.Smoke.transform.position.z);
        if (Gravity)
        {
            transform.eulerAngles = new Vector3(0, 180, 180);
        }
        else
            transform.eulerAngles = Vector3.zero;
        Gravity = !Gravity;
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        instance.state = GameState.Starting;
        rb = gameObject.GetComponent<Rigidbody2D>();
        whatIsGround = LayerMask.GetMask("Ground");
        lightCharacter = gameObject.GetComponentInChildren<Light>();
        if (lightCharacter != null)
            lightCharacter.enabled = false;
        DefaultExtraJumpCount();
        SoundManager.instance.AudioSourceCharacter = gameObject.GetComponentInChildren<AudioSource>();
    }
    void Start()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].SetActive(false);
        }
        characters[PlayerPrefs.GetInt("currentCharacter")].SetActive(true);
        animator = GetComponentInChildren<Animator>();
        Time.timeScale = 1;
        instance.state = GameState.Playing;
        Gravity = true;
        CheckGround();
        ParticleController.instance.DustEffectPlay();
    }
    void Update()
    {
        CheckGround();
        if (isGround)
        {
            Move();
            DefaultExtraJumpCount();
            jumpTimeCounter = jumpTime;
            animator.SetBool("isGround", true);
        }
        else
        {
            animator.SetBool("isGround", false);
        }

        if (isGround && (state == GameState.Playing || state == GameState.Starting))
        {
            ParticleController.instance.DustEffectPlay();
        }
        else if (!isGround && (state == GameState.Playing || state == GameState.Starting))
        {
            ParticleController.instance.DustEffectPause();
        }
        else
        {
            ParticleController.instance.DustEffectStop();
        }
#if UNITY_STANDALONE
        if (extraJumpCount >= 0 && state != GameState.Dead)
        {
            if (Input.GetButtonDown("Jump"))
            {
                SoundManager.instance.JumpCharacterPlay();
                isJumping = true;
                jumpTimeCounter = jumpTime;
                extraJumpCount--;
                //Jump();
                //Debug.Log("Нажал-прыжок" + extraJumpCount);
            }
            if (Input.GetButton("Jump") && isJumping)
            {
                if (jumpTimeCounter > 0)
                {
                    Jump();
                    jumpTimeCounter -= Time.fixedDeltaTime;
                    //Debug.Log("Зажал-прыжок" + extraJumpCount);
                }
                else
                {
                    isJumping = false;
                    //Debug.Log("Зажал-прыжок = false");
                }
            }
            if (Input.GetButtonUp("Jump") && !isGround)
            {
                //isJumping = false;
                //Debug.Log("Отжал-прыжок" + extraJumpCount);
            }
        }
        else
        {
            isJumping = false;
        }
#elif UNITY_ANDROID || UNITY_IOS
        
        if (extraJumpCount >= 0 && state != GameState.Dead && Input.touchCount>0)
        {
            Touch myTouch = Input.GetTouch(0);
            if (myTouch.phase == TouchPhase.Began)
            {
                audioSourceCharacter.PlayOneShot(sounds[0]);
                isJumping = true;
                jumpTimeCounter = jumpTime;
                extraJumpCount--;
                //Jump();
                //Debug.Log("Нажал-прыжок" + extraJumpCount);
            }
            if (myTouch.phase == TouchPhase.Stationary && isJumping)
            {
                if (jumpTimeCounter > 0)
                {
                    Jump();
                    jumpTimeCounter -= Time.fixedDeltaTime;
                    //Debug.Log("Зажал-прыжок" + extraJumpCount);
                }
                else
                {
                    isJumping = false;
                    //Debug.Log("Зажал-прыжок = false");
                }
            }
            if (myTouch.phase == TouchPhase.Ended && !isGround)
            {
                //isJumping = false;
        //Debug.Log("Отжал-прыжок" + extraJumpCount);
            }
        }
        else
        {
            isJumping = false;
        }

#endif
    }
}