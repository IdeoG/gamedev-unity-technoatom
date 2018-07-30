using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusBarControl : MonoBehaviour
{

	[SerializeField] private Text _statusText;
	[SerializeField] private GameObject _imageHealth;
	[SerializeField] private float _maxHealth;

	private RectTransform _rectHealth;

	private float Health
	{
		set { _rectHealth.SetSizeWithCurrentAnchors( RectTransform.Axis.Horizontal, value); }
	}

	private void Awake()
	{
		_rectHealth = _imageHealth.GetComponent<RectTransform>();
	}

	private void OnEnable()
	{
		SetHealthBarWidth();
	}

	private void SetHealthBarWidth()
	{
		_rectHealth.SetSizeWithCurrentAnchors( RectTransform.Axis.Horizontal, _maxHealth); 
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
