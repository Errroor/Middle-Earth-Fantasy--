using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    private float speed = 5.0f;
   [SerializeField] Transform target;

    public float timer = 1f;
    public HpBar hp;
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        Destroy(gameObject, timer);
    }

    private void Start()
    {
        hp = GameObject.FindWithTag("Player").GetComponent<HpBar>();
    }
    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        // ���������, ����������� �� ��������� ������
        if (collision.gameObject.CompareTag("Player"))
        {
            // ���� �����������, �� ������ �� ������
            return;
        }
        // ����� �������� �������� �� 1 ������� ����� ������������ �������
        Invoke("DestroyObject", 0.2f);
    }*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IDamaged>() != null)
        {
            collision.gameObject.GetComponent<IDamaged>().Damage(hp.damage);
            Invoke("DestroyObject", 0f);
        }

    }
    private void DestroyObject()
    {
        // ���������� ������� ������
        Destroy(gameObject);
    }

}

