using System.Collections;
using UnityEngine;
using GameClasses;

public class TutorialPauseTrigger : MonoBehaviour {

    [SerializeField]
    private SpriteRenderer _spriteInstruction = null;
    [SerializeField]
    private TapType _requestedInput = TapType.None;

    private IEnumerator PauseGameAtBreakpoint()
    {
        Time.timeScale = 0f;
        _spriteInstruction.enabled = true;
        while (!InputManager.GetRequestedPlayerInput(_requestedInput))
            yield return null;
        _spriteInstruction.enabled = false;
        Time.timeScale = 1f;
        DestroyObject();
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PLAYER")
        {
            StartCoroutine(PauseGameAtBreakpoint());
        }
    }
}
