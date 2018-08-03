using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

/**
 * Inputs
 * Компонента, которая отвечает за входы с клавиатуры и мыши
 */
public class Inputs : MonoBehaviour
{
    public static Inputs Instance { get; private set; }

    public IObservable<Vector2> MovementStick { get; private set; }
    public IObservable<Vector2> MouseLook { get; private set; }
    public IObservable<Vector3> MousePosition { get; private set; }
    public IObservable<Unit> FirePressed { get; private set; }

    // TODO: Переосмыслить название входа для Передвижения и для ведения Огня
    private void Awake()
    {
        Instance = this;
        
        MovementStick = this.FixedUpdateAsObservable()
            .Select(_ =>
            {
                var x = Input.GetAxis("Vertical");
                var y = Input.GetAxis("Horizontal");
                return new Vector2(x, y).normalized;
            });

        FirePressed = this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(0));

        MouseLook = this.UpdateAsObservable()
            .Select(_ =>
            {
                var x = Input.GetAxis("Mouse X");
                var y = Input.GetAxis("Mouse Y");
                return new Vector2(x, y).normalized;
            });

        MousePosition = this.UpdateAsObservable()
            .Select(_ => Input.mousePosition);
    }
}