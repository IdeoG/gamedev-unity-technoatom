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

    public IObservable<Vector2> Movement { get; private set; }
    public IObservable<Vector2> MouseLook { get; private set; }
    public IObservable<Unit> Fire { get; private set; }

    private void Awake()
    {
        Instance = this;

        Movement = this.FixedUpdateAsObservable()
            .Select(_ =>
            {
                var x = Input.GetAxis("Vertical");
                var y = Input.GetAxis("Horizontal");
                return new Vector2(x, y).normalized;
            });

        Fire = this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(0));

        MouseLook = this.UpdateAsObservable()
            .Select(_ =>
            {
                var x = Input.GetAxis("Mouse X");
                var y = Input.GetAxis("Mouse Y");
                return new Vector2(x, y).normalized;
            });
    }
}