using UnityEngine.InputSystem;
using UnityEngine;
using System;
using Unity.Mathematics;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _force;
    [SerializeField] private Transform _cameraTransform;

    private void Start()
    {
        InputManager.Instance.InputActions.Player.Interact.performed += PerformAttack;
    }

    private void OnDisable()
    {
        InputManager.Instance.InputActions.Player.Interact.performed -= PerformAttack;
    }

    private void PerformAttack(InputAction.CallbackContext context)
    {
        Debug.Log("Used");

        Ray ray = new Ray(_cameraTransform.position, _cameraTransform.forward);
        Physics.Raycast(ray, out RaycastHit hit, 500f);

        var projecrile = Instantiate(_projectilePrefab, _attackPoint.position, new()).GetComponent<Fireball>();

        projecrile.transform.LookAt(hit.point);

        projecrile.Launch();
    }
}
