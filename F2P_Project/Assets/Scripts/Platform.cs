using UnityEngine;
using GameClasses;

public class Platform : MonoBehaviour {

    public PlatformProperties Properties { get { return _properties; } }

    [SerializeField]
    private SpriteRenderer _renderer = null;

    private PlatformProperties _properties;

    public void Initialize()
    {
        _properties = new PlatformProperties();
        _properties.Initialize();
        transform.localScale = new Vector3(_properties.Length, transform.localScale.y, transform.localScale.z);
    }

    private void Update()
    {
        transform.Translate(Vector3.left * DifficultyManager.GetGameSpeed() * Time.deltaTime);
        if (!CheckIfVisible())
            Destroy(gameObject);
    }

    private bool CheckIfVisible()
    {
        return (_renderer.isVisible || GetEndPoint().x > Camera.main.ViewportToWorldPoint(Vector3.zero).x);
    }

    public Vector3 GetEndPoint()
    {
        return transform.position + Vector3.right * (_properties.Length / 2f);
    }
}
