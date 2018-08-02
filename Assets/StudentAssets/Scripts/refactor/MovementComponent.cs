using UniRx;
using UnityEngine;

/**
 * MovementComponent
 * По сути класс, отвечающий за возможность передвижения с помощью Inputs
 */
[RequireComponent( typeof(Rigidbody) )]
public class MovementComponent : MonoBehaviour
{
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
                var shift = MovementSpeed * transform.forward * Time.deltaTime * inputMovement.x;
                
                var turn = _turnSpeed * Time.deltaTime * inputMovement.y;
                turn = inputMovement.x < 0 ? -turn : turn;
                var turnY = Quaternion.Euler(0, turn, 0);

                _rigidbody.MovePosition(_rigidbody.position + shift);
                _rigidbody.MoveRotation(_rigidbody.rotation * turnY);
            })
            .AddTo(this);
    }

    private void Awake()
    {
        MovementSpeed = _maxMovementSpeed;
        MaxMovementSpeed = _maxMovementSpeed;
        _rigidbody = GetComponent<Rigidbody>();
    }
}