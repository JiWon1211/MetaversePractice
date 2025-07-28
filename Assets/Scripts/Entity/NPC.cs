using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class NPC : MonoBehaviour
{
    private bool isPlayerInRange = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log("�÷��̾ NPC ��ó�� �ֽ��ϴ�. 'E' Ű�� ���� ��ȣ�ۿ��ϼ���.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            Debug.Log("�÷��̾ NPC�� �־������ϴ�.");
        }
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("�÷��̾ ��ȣ�ۿ��߽��ϴ�. �̴ϰ������� �̵��մϴ�.");
            StartMinigame();
        }
    }

    // �� ��ȯ�� ó���ϴ� �Լ�
    void StartMinigame()
    {
        SceneManager.LoadScene("FlappyPlaneGame");
    }
}