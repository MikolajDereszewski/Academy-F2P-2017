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
        Vector3 move = Vector3.left * DifficultyManager.GetGameSpeed() * GetAspect() * Time.deltaTime;
        _sky1.position += move;
        _sky2.position += move;
        if (IsOverCameraView())
            ResetSky();
    }

    private void SetSkySize(Transform sky)
    {
        float scale = GetScreenSize();
        sky.localScale = new Vector3(scale, scale, 1f);
    }

    private void ResetSky()
    {
        _sky1.position = new Vector3(GetScreenSize() * (GetAspect() + 0.5f), _sky1.position.y, _sky1.position.z);
        _sky2.position = _sky1.position + (Vector3.right * GetScreenSize() / GetAspect());
    }

    private bool IsOverCameraView()
    {
        return (_sky2.position.x <= GetScreenSize() * (GetAspect() + 0.5f));
    }

    private float GetScreenSize()
    {
        return Camera.main.ViewportToWorldPoint(Vector2.up).y - Camera.main.ViewportToWorldPoint(Vector2.zero).y;
    }

    private float GetAspect()
    {
        return (710f / 2048f);
    }
}
