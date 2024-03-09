using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slime : MonoBehaviour, IDamaged
{
    private float HP = 100f;
    private UnityEngine.Object enemyRef;
    public NPC_Task statue;
    public Transform target;
    private void Start()
    {
        enemyRef = Resources.Load("Slime");
        target = GetComponent<Pathfinding.AIDestinationSetter>().target;
    }

    void Respawn()
    {
        GameObject enemyCopy = (GameObject)Instantiate(enemyRef);
        enemyCopy.transform.position = transform.position;
        enemyCopy.GetComponent<Pathfinding.AIDestinationSetter>().target = target;


        Destroy(gameObject);
    }

    public void Damage(int count)
    {
        HP -= 50;

        if (HP <= 0)
        {
            gameObject.SetActive(false);
            statue.smiles++;
            Invoke("Respawn", 10f);
        }
    }
}