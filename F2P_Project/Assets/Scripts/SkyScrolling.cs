using UnityEngine;
using GameClasses;

public class SkyScrolling : MonoBehaviour {

    [SerializeField]
    private Transform _sky1 = null, _sky2 = null;

    private void Start()
    {
        SetSkySize(_sky1);
        SetSkySize(_sky2);
        ResetSky();
    }

    private void Update()
    {
        Vector3 move = Vector3.left * DifficultyManager.GetGameSpeed() * 0.5f * Time.deltaTime;
        _sky1.position += move;
        _sky2.position += move;
        if (IsOverCameraView())
        {
            _sky1.position = new Vector3(0f, _sky1.position.y, _sky1.position.z);
            ResetSky();
        }
    }

    private void SetSkySize(Transform sky)
    {
        Vector3 scale = Camera.main.ViewportToWorldPoint(Vector2.one) - Camera.main.ViewportToWorldPoint(Vector2.zero);
        sky.localScale = new Vector3(scale.x * 0.5f, scale.y, 1f);
    }

    private void ResetSky()
    {
        _sky2.position = _sky1.position + (Vector3.right * _sky1.localScale.x * 2f);
    }

    private bool IsOverCameraView()
    {
        return (_sky2.position.x + (_sky2.localScale.x) <= Camera.main.ViewportToWorldPoint(Vector3.right).x);
    }
}
