using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour
{
    public string Plan;  // �̵��� �� �̸�

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // �÷��̾�� �浹���� ��
        {
        Debug.Log("Plan: " + Plan);

            SceneManager.LoadScene("Plan");
        }
    }
}
