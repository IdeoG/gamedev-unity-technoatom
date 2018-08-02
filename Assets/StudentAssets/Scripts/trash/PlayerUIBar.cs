using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIBar : MonoBehaviour
{
    [SerializeField] private Text _statusText;
    [SerializeField] private GameObject _imageHealth;
    [SerializeField] private float _maxHealth;

    [SerializeField] private GameObject _player;

    private RectTransform _rectHealth;

    private float Health
    {
        set => _rectHealth.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, value);
    }

    private void Awake()
    {
        _rectHealth = _imageHealth.GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        SetHealthBarWidth();
    }

    private void Start()
    {
        var playerHealth = _player.GetComponent<HealthComponent>();
        playerHealth.Health
            .Subscribe(health =>
            {
                Debug.Log($"health changed: {health}");
                Health = health * _maxHealth / playerHealth.MaxHealth;
            });
    }

    private void SetHealthBarWidth()
    {
        _rectHealth.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _maxHealth);
    }

    public void SetHealth(float playerHealth, float playerMaxHealth)
    {
        Health = playerHealth * _maxHealth / playerMaxHealth;
    }

    public void SetText(string text)
    {
        _statusText.text = text;
    }
}