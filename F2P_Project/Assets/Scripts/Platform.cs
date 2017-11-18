using UnityEngine;
using GameClasses;

public class Platform : MonoBehaviour {

    public PlatformProperties Properties { get { return _properties; } }
    
    [SerializeField]
    private ScrollingObject _obstaclePrefab = null;
    [SerializeField]
    private SpriteRenderer _renderer1, _renderer2;

    private PlatformProperties _properties;

    public void Initialize(Vector3 spawnPosition, bool createObstacles = true)
    {
        _properties = new PlatformProperties();
        _properties.Initialize();
        _renderer1.sprite = _properties.Sprite;
        _renderer2.sprite = _properties.Sprite;
        _renderer1.size = new Vector2(_properties.Length, 0.45f);
        _renderer2.size = new Vector2(_properties.Length, 0.45f);
        GetComponent<BoxCollider2D>().size = new Vector2(_properties.Length, 0.1f);
        transform.position = spawnPosition + Vector3.right * (_properties.Length * 0.5f) * 5f;
        if(createObstacles)
            CreateObstacles();
    }

    private void CreateObstacles()
    {
        int obstaclesCount = Random.Range(1, (int)(_properties.Length/2f));
        for(int i = 0; i < obstaclesCount; i++)
        {
            float randomizer = (_properties.Length * 0.5f - (_obstaclePrefab.transform.localScale.x * 0.5f));
            Vector3 spawnPosition = new Vector3(Random.Range(-randomizer, randomizer) + transform.position.x, _obstaclePrefab.transform.position.y, _obstaclePrefab.transform.position.z);
            Instantiate(_obstaclePrefab, spawnPosition, Quaternion.identity);
        }
    }

    public Vector3 GetEndPoint()
    {
        return transform.position + Vector3.right * (_properties.Length / 2f) * 5f;
    }
}
