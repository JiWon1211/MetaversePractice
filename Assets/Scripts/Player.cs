using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D _rigidbody;

    public float flapForce = 6f; //점프하는 힘
    public float forwardSpeed = 3f; //정면으로 나아가는 속도
    public bool isDead = false; //죽었는지 여부
    public float deathCooldown = 0f; // 충돌 뒤 죽음까지의 시간

    bool isFlap = false; // 점프했는지 여부

    public bool godMode = false; // 갓모드 (테스트용)

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;

        animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();

        if (animator == null)
            Debug.LogError("not founded animator");

        if (_rigidbody == null)
            Debug.LogError("not founded rigidbody");
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            if (deathCooldown <= 0f)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    gameManager.RestartGame();
                }
            }
            else 
            {
                deathCooldown -= Time.deltaTime; // 죽음까지의 시간 감소
            }

        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) // 스페이스바나 마우스 클릭으로 점프
            {
                isFlap = true;
            }
        }
    }
    private void FixedUpdate()
    {
        if (isDead) return;

        Vector3 velocity = _rigidbody.velocity;
        velocity.x = forwardSpeed; // 정면으로 나아가는 속도 설정

        if (isFlap)
        {
            velocity.y = flapForce; // 점프하는 힘 설정
            isFlap = false; // 점프 상태 초기화
        }

        _rigidbody.velocity = velocity; // Rigidbody에 새 속도 적용

        float angle = Mathf.Clamp(_rigidbody.velocity.y*10f, -90, 90);
        transform.rotation = Quaternion.Euler(0, 0, angle); // Rigidbody의 속도에 따라 회전 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (godMode) return;

        if (isDead) return;

        isDead = true; // 충돌 시 죽음 상태로 변경
        deathCooldown = 1f; // 죽고 1초뒤 게임 재시작

        animator.SetInteger("IsDie", 1);
        gameManager.GameOver();
    }
}
