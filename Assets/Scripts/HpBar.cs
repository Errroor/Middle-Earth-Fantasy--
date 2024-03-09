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
    public Image Lvl_bar;
    private int potionCount = 3; //  оличество доступных зелий в игре
    public int Lvl = 2;
    public float Now_exp = 200;
    public float Max_exp = 220;
    public float Multiply_value = 2.2f;
    public Text Lvl_text;
    public Text Max_exp_text;

    public void RestoreHealth(float amount)
    {
        HP += amount;
        HP = Mathf.Clamp(HP, 0f, 100f); // ќграничиваем здоровье в пределах от 0 до 100
        Bar.fillAmount = HP / 100f;
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            UsePotion();
        }
        Lvl_bar.fillAmount = Now_exp / Max_exp;
        Lvl_text.text = "Lvl" +" " + Lvl.ToString();       
        Max_exp_text.text = Now_exp.ToString() + " / " + Max_exp.ToString();
        if (Now_exp >= Max_exp)
        {
            Lvl += 1;
            Now_exp= 0;
            Max_exp *= Multiply_value;
            Convert.ToInt32(Max_exp);
            Multiply_value += 0.4f;
        }
    }

    void UsePotion()
    {
        if (potionCount > 0 && HP < 100f)  // ѕровер€ем доступность зель€ и не превышено ли максимальное здоровье
        {
            RestoreHealth(20); // ¬осстанавливаем здоровье на 20 единиц
            potionCount--; // ”меньшаем счетчик доступных зелий
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
