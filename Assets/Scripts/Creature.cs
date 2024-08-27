using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class Creature : MonoBehaviour
{
    private const float _animatorUpdateInterval = 0.1f;

    [SerializeField, Min(0f)] private float _maxMoveSpeed;
    [Tooltip("Degrees per second.")]
    [SerializeField, Min(0f)] private float _rotateSpeed;
    [Tooltip("Коэффициент скорости при приближении к цели.")]
    [SerializeField] private AnimationCurve _speedCurve;

    protected Rigidbody _rigidbody;

    private Transform _moveTarget;
    private float _prevMoveSpeed;
    private Animator _animator;

    protected virtual void Awake()
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
        if (_moveTarget == null)
            return;

        Vector3 toTarget = VectorUtils.HorizontalDirection(transform.position, _moveTarget.position);

        _rigidbody.rotation = QuaternionUtils.RotateTowards(transform.rotation, toTarget, _rotateSpeed * Time.fixedDeltaTime);

        Vector3 newVelocity = _maxMoveSpeed * _speedCurve.Evaluate(toTarget.magnitude) * transform.forward;
        newVelocity.y = _rigidbody.velocity.y;
        _rigidbody.velocity = newVelocity;
    }

    public void Follow(Transform target)
    {
        _moveTarget = target;
    }

    private IEnumerator UpdateAnimator()
    {
        var delay = new WaitForSeconds(_animatorUpdateInterval);

        while (enabled)
        {
            float moveSpeed = VectorUtils.HorizontalMagnitude(_rigidbody.velocity);

            if (Mathf.Approximately(moveSpeed, _prevMoveSpeed) == false)
            {
                _animator.SetFloat(AnimatorParams.HorizontalSpeed, moveSpeed);
                _prevMoveSpeed = moveSpeed;
            }

            yield return delay;
        }
    }
}
