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
            Debug.Log("플레이어가 NPC 근처에 있습니다. 'E' 키를 눌러 상호작용하세요.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            Debug.Log("플레이어가 NPC와 멀어졌습니다.");
        }
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("플레이어가 상호작용했습니다. 미니게임으로 이동합니다.");
            StartMinigame();
        }
    }

    // 씬 전환을 처리하는 함수
    void StartMinigame()
    {
        SceneManager.LoadScene("FlappyPlaneGame");
    }
}