using System;
using UnityEngine;

public class UnityInputService : MonoBehaviour, IInputService
{
    public event Action<Vector3> OnTap;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 screenPoint = Input.mousePosition;
            Ray ray = mainCamera.ScreenPointToRay(screenPoint);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                OnTap?.Invoke(hit.point);
            }
        }
    }
}
