using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    // ��������� ���������� ��� ����� � ��������
    public Transform ItenPoint;
    public Transform ShotPoint;
    public GameObject ItemPrefab;
    public GameObject ThrowPrefab;
    public GameObject BowPrefab;
    Rigidbody2D rb; // ���������� ��� ����������� Rigidbody2D � Animator
    Animator animator;
    public float moveSpeed = 1f; // �������� �������� ���������

    [SerializeField]// ��������� ���� ��� ����� ��������
    private Transform shotPointTransform = null;
   
    void Start()
    {
        instance = this;
        // ��������� �������� ������� ������ � ������������� Rigidbody2D � Animator
        Application.targetFrameRate = 60;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {// ��������� ����� �� ������������ ��� ��� X � Y
        float x = Input.GetAxisRaw("Horizontal");
        float y = (x == 0) ? Input.GetAxisRaw("Vertical") : 0.0f;
        // ���������� ��������� ������ � ����������� �� �����
        if (x != 0 || y != 0)
        {
            animator.SetFloat("x", x);
            animator.SetFloat("y", y);
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
        // ���������� �������� ������ � ��������
        StartCoroutine(Action());
        Shot();// �������� �� ������ ��� ������� ��������������� ������
    }
    IEnumerator Action()// �������� ��� ��������� ��������� �������� ������
    { // �������� ��� ������� ������������ ������
        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetTrigger("Slash");
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            animator.SetTrigger("Guard");
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            animator.SetTrigger("Item");
            Instantiate(ItemPrefab, ItenPoint.position, transform.rotation);
        }
                              

        if (Input.GetKeyDown(KeyCode.M))
        {
            animator.SetTrigger("Dead");
            this.transform.position = new Vector2(0f, -0.12f);
            for (var i = 0; i < 64; i++)
            {
                yield return null;
            }
            this.transform.position = Vector2.zero;
        }
    }
    private float GetRotation()// ����� ��� ��������� ���� �������� ��� ��������
    {
        if (animator.GetFloat("x") == -1 && animator.GetFloat("y") == 0)
        {
            return 0;
        }
        else if (animator.GetFloat("x") == 1 && animator.GetFloat("y") == 0)
        {
            return 180;
        }
        else if (animator.GetFloat("x") == 0 && animator.GetFloat("y") == 1)
        {
            return -90;
        }
        else
        {
            return 90;
        }
    }
    // ���������� ��� ���������� �������� ��������

    private float cooldown = 0;
    void Shot()
    {
        if (Input.GetKeyDown(KeyCode.X) && cooldown <= 0)
        {
            cooldown = 1.5f;
            animator.SetTrigger("Throw");
            Instantiate(ThrowPrefab, transform.position, Quaternion.Euler(new Vector3(0,0, GetRotation())), null);
        }

        if (Input.GetKeyDown(KeyCode.C) && cooldown <= 0)
        {
            cooldown = 2f;
            animator.SetTrigger("Bow");
            // ������� ��������� ������� BowPrefab � ������� �������� ������� (transform.position)
            // � ���������, �������� ����� Quaternion.Euler � ������� GetRotation() ��� ��� Z
            Instantiate(BowPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, GetRotation())), null);
        }  
        if(cooldown >=0)    // ���� cooldown ������ ��� ����� ����, ��������� ��� �� ��������, ���������������� ������� ���������� � ����������� ����� (Time.deltaTime)
            cooldown -= Time.deltaTime;
    }
}




