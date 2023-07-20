using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveJon : MonoBehaviour
{
    [SerializeField] private int SaveNumber = 1;
   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            DataController.Instance.gameData.Pos = this.transform.position.x + "/" + this.transform.position.y;
            DataController.Instance.SaveUnlock(SaveNumber);
            Destroy(this.gameObject);
        }
    }
}
