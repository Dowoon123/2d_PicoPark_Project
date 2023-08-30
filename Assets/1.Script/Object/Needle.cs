using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needle : MonoBehaviour
{
    [SerializeField] private float MoveX;
    [SerializeField] private float MoveY;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("플레이어 체크됨");

            Rigidbody2D rigidbody2D = collision.transform.GetComponent<Rigidbody2D>();
            PlayerController player = collision.transform.GetComponent<PlayerController>();

            rigidbody2D.AddForce(transform.right * MoveX * player.facingDir, ForceMode2D.Impulse); 

        }
    }

}
