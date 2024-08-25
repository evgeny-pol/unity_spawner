using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraRotator : MonoBehaviour
{
    private const int RotateCommandButton = InputConstants.SecondMouseButton;

    [SerializeField] private Transform _lookAt;
    [SerializeField] private float _moveSensitivity = 1f;

    private bool IsRotating => Input.GetMouseButton(RotateCommandButton);

    private void Update()
    {
        transform.LookAt(_lookAt);

        if (IsRotating)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            transform.RotateAround(_lookAt.position, Vector3.up, Input.GetAxis(InputConstants.HorizontalMouseAxis) * _moveSensitivity);
            transform.RotateAround(_lookAt.position, transform.right, Input.GetAxis(InputConstants.VerticalMouseAxis) * -_moveSensitivity);
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
