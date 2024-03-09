using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Unity.VisualScripting;

public class HpBar : MonoBehaviour
{
    Animator animator;
    private float HP = 100f;
    public Image Bar;
    private int potionCount = 3; // ���������� ��������� ����� � ����

    public void RestoreHealth(float amount)
    {
        HP += amount;
        HP = Mathf.Clamp(HP, 0f, 100f); // ������������ �������� � �������� �� 0 �� 100
        Bar.fillAmount = HP / 100f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            UsePotion();
        }
    }

    void UsePotion()
    {
        if (potionCount > 0 && HP < 100f)  // ��������� ����������� ����� � �� ��������� �� ������������ ��������
        {
            RestoreHealth(20); // ��������������� �������� �� 20 ������
            potionCount--; // ��������� ������� ��������� �����
            Debug.Log("Potion used, remaining: " + potionCount);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            animator = GetComponent<Animator>();
            animator.SetTrigger("Damage");
            HP -= 30;
            Bar.fillAmount = HP / 100f;

            if (HP <= 0)
            {
                ReloadGame();
            }
        }
    }

    void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
