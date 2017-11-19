using System.Collections;
using UnityEngine;
using GameClasses;
using UnityEngine.UI;

public class TutorialPauseTrigger : MonoBehaviour
{
    public Image image;
    public Image arrow;
    public Text text;
    public Text doNothink;
    public int positionX;
    public string discribe;
    public bool isLastTrigger;

    private int iteration = 0;

    [SerializeField]
    private SpriteRenderer _spriteInstruction = null;
    [SerializeField]
    private TapType _requestedInput = TapType.None;

    private IEnumerator PauseGameAtBreakpoint()
    {
        Time.timeScale = 0f;
        _spriteInstruction.enabled = true;
        while (!InputManager.GetRequestedPlayerInput(_requestedInput) || doNothink != null)
            yield return null;

        if (!isLastTrigger)
        {
            _spriteInstruction.enabled = false;
            Time.timeScale = 1f;
            DestroyObject();
        }
    }

    private void DestroyObject()
    {
        text.enabled = false;
        image.enabled = false;
        arrow.enabled = false;
        if (doNothink != null)
            doNothink.enabled = false;

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PLAYER")
        {

            text.text = discribe;
            image.rectTransform.position = new Vector2(positionX, image.rectTransform.position.y);
            iteration++;
            image.enabled = true;
            text.enabled = true;
            arrow.enabled = true;
            if (doNothink != null)
                doNothink.enabled = true;

            StartCoroutine(PauseGameAtBreakpoint());
        }
    }
}
