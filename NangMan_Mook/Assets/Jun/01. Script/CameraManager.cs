using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private float targetFollowOffset = 7;
    [SerializeField] private CinemachineVirtualCamera cinemachineTransposer;


    private void Update()
    {
        HandleZoom();
    }

    // ¸¶¿ì½º ÈÙ(ÁÜ)
    private void HandleZoom()
    {
        float speed = 1.5f;

        targetFollowOffset += GetCameraZoomAmount() * speed;
        targetFollowOffset = Mathf.Clamp(targetFollowOffset, 7, 10);

        cinemachineTransposer.m_Lens.OrthographicSize = targetFollowOffset;

    }

    public float GetCameraZoomAmount()
    {
        float zoomAmount = 0f;

        if (Input.mouseScrollDelta.y > 0)
        {
            zoomAmount = -1f;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            zoomAmount = +1f;
        }

        return zoomAmount;
    }
}
