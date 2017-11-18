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
            ResetSky();
    }

    private void SetSkySize(Transform sky)
    {
        float scale = GetScreenSize().y;
        sky.localScale = new Vector3(scale, scale, 1f);
    }

    private void ResetSky()
    {
        _sky1.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector2.zero).x, Camera.main.ViewportToWorldPoint(Vector2.zero).y, _sky1.position.z);
        _sky2.position = new Vector3(_sky1.position.x + _sky1.localScale.x / GetAspect(), _sky1.position.y, _sky1.position.z);
    }

    private bool IsOverCameraView()
    {
        return (_sky2.position.x <= Camera.main.ViewportToWorldPoint(Vector2.zero).x);
    }

    private Vector2 GetScreenSize()
    {
        return Camera.main.ViewportToWorldPoint(Vector2.one) - Camera.main.ViewportToWorldPoint(Vector2.zero);
    }

    private float GetAspect()
    {
        return 2048f / 5906f;
    }
}
