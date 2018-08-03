using System;
using UniRx;
using UnityEngine;

/**
 * MovementComponent
 * По сути класс, отвечающий за возможность передвижения с помощью Inputs
 *
 * TODO: Этот класс также будет отвечать за Угловое Перемещение объекта и за ротацию танка. Таким обращом название не соответствует тому, что делается
 */
[RequireComponent(typeof(Rigidbody))]
public class MovementComponent : MonoBehaviour
{
    [SerializeField] private Transform _turret;
    [SerializeField] private float _maxMovementSpeed;
    [SerializeField] private float _turnSpeed;

    private Rigidbody _rigidbody;

    public float MaxMovementSpeed { get; private set; }
    public float MovementSpeed { get; set; }

    private void Start()
    {
        var inputs = Inputs.Instance;

        // TODO: Поменять Movement на отдельные Move и Turn
        inputs.Movement
            .Where(v2 => v2 != Vector2.zero)
            .Subscribe(inputMovement =>
            {
                Move(inputMovement);
                Turn(inputMovement);
            })
            .AddTo(this);

        inputs.MousePosition
            .Subscribe(TurnTurret);

        Console.Out.WriteLine("");
    }


    private void TurnTurret(Vector3 mousePosition)
    {
        var ray = Camera.main.ScreenPointToRay(mousePosition);

        if (!Physics.Raycast(ray, out var hit, 100)) return;

        var cameraHitIntersectionPoint = hit.point;
        cameraHitIntersectionPoint.y = _turret.position.y;
        _turret.LookAt(cameraHitIntersectionPoint);
    }

    private void Turn(Vector2 inputMovement)
    {
        var turn = _turnSpeed * Time.deltaTime * inputMovement.y;
        turn = inputMovement.x < 0 ? -turn : turn;
        var turnY = Quaternion.Euler(0, turn, 0);

        _rigidbody.MoveRotation(_rigidbody.rotation * turnY);
    }

    private void Move(Vector2 inputMovement)
    {
        var shift = MovementSpeed * transform.forward * Time.deltaTime * inputMovement.x;
        _rigidbody.MovePosition(_rigidbody.position + shift);
    }


    private void Awake()
    {
        MovementSpeed = _maxMovementSpeed;
        MaxMovementSpeed = _maxMovementSpeed;
        _rigidbody = GetComponent<Rigidbody>();
    }
}