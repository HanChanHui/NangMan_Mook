using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveCreate3 : MonoBehaviour
{
    public GameObject Save1;
    Transform Tr;
    void Start()
    {
        Tr = GetComponent<Transform>();

        if (DataController.Instance.gameData.isClear4Count < 1)
        {
            Create();
        }
    }

    private void Create()
    {
        Save1 = Instantiate(Save1, Tr.position, Tr.rotation);
    }
}
