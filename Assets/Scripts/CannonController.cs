using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    [SerializeField] PlayerBullet bulletPrefab;
    [SerializeField] Vector3 bulletRotation;
    Quaternion bulletQuaternionRotation;

    private void Start()
    {
        bulletQuaternionRotation = Quaternion.Euler(bulletRotation);
    }

    private void OnEnable()
    {
        CustomInputManager.OnPressedLMouse += Fire;
    }

    private void OnDisable()
    {
        CustomInputManager.OnPressedLMouse -= Fire;
    }

    private void Fire()
    {
        Instantiate(bulletPrefab, transform.position, bulletQuaternionRotation);
    }
}
