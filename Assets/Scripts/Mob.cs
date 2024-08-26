using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class Mob : MonoBehaviour
{
    private const float _animatorUpdateInterval = 0.1f;

    [SerializeField, Min(0f)] private float _maxMoveSpeed;
    [SerializeField, Min(0f)] private float _acceleration;

    private Vector3 _moveDirection;
    private float _moveSpeed;
    private float _prevMoveSpeed;
    private Animator _animator;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        StartCoroutine(UpdateAnimator());
    }

    private void FixedUpdate()
    {
        if (_moveDirection == Vector3.zero)
            return;

        _moveSpeed = Mathf.Min(_moveSpeed + _acceleration * Time.fixedDeltaTime, _maxMoveSpeed);
        Vector3 currentVelocity = _rigidbody.velocity;
        Vector3 newVelocity = transform.forward * _moveSpeed;
        newVelocity.y = currentVelocity.y;
        _rigidbody.velocity = newVelocity;
    }

    private IEnumerator UpdateAnimator()
    {
        var delay = new WaitForSeconds(_animatorUpdateInterval);

        while (enabled)
        {
            Vector3 velocity = _rigidbody.velocity;
            velocity.y = 0;
            float moveSpeed = velocity.magnitude;

            if (Mathf.Approximately(moveSpeed, _prevMoveSpeed) == false)
            {
                _animator.SetFloat(AnimatorParams.HorizontalSpeed, moveSpeed);
                _prevMoveSpeed = moveSpeed;
            }

            yield return delay;
        }
    }

    public void Move(Vector3 direction)
    {
        _moveDirection = direction;
        transform.rotation = Quaternion.FromToRotation(Vector3.forward, direction);
    }
}
