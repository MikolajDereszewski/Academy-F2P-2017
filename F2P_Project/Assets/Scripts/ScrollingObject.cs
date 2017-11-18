using UnityEngine;
using GameClasses;

public class ScrollingObject : MonoBehaviour {
    
    [SerializeField]
    private SpriteRenderer _renderer = null;

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
        return transform.position + Vector3.right * (transform.localScale.x * 0.5f);
    }
}
