using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionManager : MonoBehaviour
{
    Animator animator;
    float speed = 0.2f;
    public float leftTime;

    void Start()
    {
        // ������� ������� ������
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 playerPosition = player.transform.position;

        // ������������� ������� ����� ��� �������
        transform.position = new Vector3(playerPosition.x, playerPosition.y + 0.75f, playerPosition.z);

        // ������ �������� �����
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;

        // ���������� ����� �� ���������� �������
        Destroy(gameObject, leftTime);
    }

    void Update()
    {

    }
}
