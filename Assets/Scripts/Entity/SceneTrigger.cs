using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour
{
    public string Plan;  // 이동할 씬 이름

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 플레이어와 충돌했을 때
        {
        Debug.Log("Plan: " + Plan);

            SceneManager.LoadScene("Plan");
        }
    }
}
