using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D _rigidbody;

    public float flapForce = 6f; //�����ϴ� ��
    public float forwardSpeed = 3f; //�������� ���ư��� �ӵ�
    public bool isDead = false; //�׾����� ����
    public float deathCooldown = 0f; // �浹 �� ���������� �ð�

    bool isFlap = false; // �����ߴ��� ����

    public bool godMode = false; // ����� (�׽�Ʈ��)

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
                deathCooldown -= Time.deltaTime; // ���������� �ð� ����
            }

        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) // �����̽��ٳ� ���콺 Ŭ������ ����
            {
                isFlap = true;
            }
        }
    }
    private void FixedUpdate()
    {
        if (isDead) return;

        Vector3 velocity = _rigidbody.velocity;
        velocity.x = forwardSpeed; // �������� ���ư��� �ӵ� ����

        if (isFlap)
        {
            velocity.y = flapForce; // �����ϴ� �� ����
            isFlap = false; // ���� ���� �ʱ�ȭ
        }

        _rigidbody.velocity = velocity; // Rigidbody�� �� �ӵ� ����

        float angle = Mathf.Clamp(_rigidbody.velocity.y*10f, -90, 90);
        transform.rotation = Quaternion.Euler(0, 0, angle); // Rigidbody�� �ӵ��� ���� ȸ�� 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (godMode) return;

        if (isDead) return;

        isDead = true; // �浹 �� ���� ���·� ����
        deathCooldown = 1f; // �װ� 1�ʵ� ���� �����

        animator.SetInteger("IsDie", 1);
        gameManager.GameOver();
    }
}
