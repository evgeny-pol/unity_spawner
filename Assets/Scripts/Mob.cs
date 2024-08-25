using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class Mob : MonoBehaviour
{
    // private const int = Animator.StringToHash(nameof(HorizontalSpeed));

    [SerializeField, Min(0f)] private float _maxMoveSpeed;
    [SerializeField, Min(0f)] private float _acceleration;

    private Vector3 _moveDirection;
    private float _moveSpeed;
    private Animator _animator;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _moveSpeed = Mathf.Min(_moveSpeed + _acceleration * Time.fixedDeltaTime, _maxMoveSpeed);
        Vector3 currentVelocity = _rigidbody.velocity;
        Vector3 newVelocity = transform.forward * _moveSpeed;
        newVelocity.y = currentVelocity.y;
        _rigidbody.velocity = newVelocity;
    }

    private void Update()
    {
        Vector3 velocity = _rigidbody.velocity;
        velocity.y = 0;
        _animator.SetFloat(AnimatorParams.HorizontalSpeed, velocity.magnitude);
    }

    public void Move(Vector3 direction)
    {
        _moveDirection = direction;
        transform.rotation = Quaternion.FromToRotation(Vector3.forward, direction);
    }
}
