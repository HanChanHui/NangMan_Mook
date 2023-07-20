using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [Header("UI 오브젝트 넣기")] [SerializeField] private UIManager ui;
    [Header("Player 오브젝트 넣기")][SerializeField] private PlayerController Player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            ui.MapOut();
            Player.GetComponent<Animator>().SetTrigger("isGameOver");
        }
    }
}
