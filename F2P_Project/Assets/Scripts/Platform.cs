using UnityEngine;
using GameClasses;

public class Platform : MonoBehaviour {

    public PlatformProperties Properties { get { return _properties; } }

    [SerializeField]
    private SpriteRenderer _renderer = null;

    private PlatformProperties _properties;

    public void Initialize()
    {
        _properties.Initialize();
        transform.localScale = new Vector3(_properties.Length, transform.localScale.y, transform.localScale.z);
    }

    private void Update()
    {
        transform.Translate(Vector3.left * DifficultyManager.GetGameSpeed());
        if (!CheckIfVisible())
            Destroy(gameObject);
    }

    private bool CheckIfVisible()
    {
        return (_renderer.isVisible && transform.position.x < 0);
    }

    public Vector3 GetEndPoint()
    {
        return transform.position + Vector3.right * (_properties.Length / 2f);
    }
}
