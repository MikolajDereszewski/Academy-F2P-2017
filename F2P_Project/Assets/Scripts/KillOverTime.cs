using UnityEngine;

public class KillOverTime : MonoBehaviour {

    [SerializeField]
    private float _time;

	void Start ()
    {
        Invoke("Kill", _time);
	}

    private void Kill()
    {
        Destroy(gameObject);
    }
}
