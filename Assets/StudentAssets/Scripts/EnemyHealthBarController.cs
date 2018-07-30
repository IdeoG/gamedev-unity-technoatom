using UnityEngine;

public class EnemyHealthBarController : MonoBehaviour
{
    [SerializeField] private GameObject _imageBackground;
    [SerializeField] private GameObject _imageHealth;
    [SerializeField] private float _maxHealth;

    private RectTransform _rectBackground;
    private RectTransform _rectHealth;

    private float Health
    {
        set { _rectHealth.SetSizeWithCurrentAnchors( RectTransform.Axis.Horizontal, value); }
    }

    private void Awake()
    {
        _rectBackground = _imageBackground.GetComponent<RectTransform>();
        _rectHealth = _imageHealth.GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        SetHealthBarWidth();
        HideHealthBar();
    }

    private void SetHealthBarWidth()
    {
        _rectBackground.SetSizeWithCurrentAnchors( RectTransform.Axis.Horizontal, _maxHealth);
        _rectHealth.SetSizeWithCurrentAnchors( RectTransform.Axis.Horizontal, _maxHealth); 
    }
    
    public void SetHealth(float enemyHealth, float enemyMaxHealth)
    {
        Health = enemyHealth * _maxHealth / enemyMaxHealth;  
    }

    public void HideHealthBar()
    {
        _imageBackground.SetActive(false);
        _imageHealth.SetActive(false);
    }
    
    public void ShowHealthBar()
    {
        _imageBackground.SetActive(true);
        _imageHealth.SetActive(true);
    }
}
