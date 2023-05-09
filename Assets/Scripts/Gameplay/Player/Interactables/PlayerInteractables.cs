using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerInteractables : MonoBehaviour
{
    #region Variables
    private const float FADE_OUT_ANIM_DURATION = 0.25f;
    private const int FRAMES_NEEDED_TO_UPDATE = 10;

    [SerializeField] 
    private Transform cameraTransform;
    [SerializeField] 
    private float maxDistance = 1;
    [SerializeField] 
    private LayerMask layers;
    [SerializeField] 
    private TextMeshProUGUI useText;
    [SerializeField] 
    private GameObject textWindow;

    private bool textWindowVisible = true;

    private Interactable interactable;
    private int framesElapsed;

    private InputActions inputActions;
    #endregion

    #region Unity Methods

    private void Awake()
    {
        inputActions = new();
        inputActions.Player.Enable();

        inputActions.Player.Use.performed += Interact;
    }

    private void Interact(InputAction.CallbackContext context)
    {
        interactable?.Interact();
    }

    private void Update()
    {
        if (framesElapsed >= FRAMES_NEEDED_TO_UPDATE)
        {
            bool lastVisibleValue = textWindowVisible;
            textWindowVisible = false;

            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hitInfo, maxDistance, layers))
            {
                if (hitInfo.collider.TryGetComponent<Interactable>(out Interactable interactable) && interactable.active)
                {
                    useText.text = interactable.GetHoverText();
                    textWindowVisible = true;
                    this.interactable = interactable;
                }
            }

            if (lastVisibleValue != textWindowVisible)
            {
                StopCoroutine(DoFadeInOutAnimation());
                StartCoroutine(DoFadeInOutAnimation());
            }

            framesElapsed = 0;
        }

        framesElapsed++;
    }

    private IEnumerator DoFadeInOutAnimation()
    {
        float time = 0;
        CanvasGroup textWindowFrame = textWindow.GetComponent<CanvasGroup>();
        while (time < FADE_OUT_ANIM_DURATION)
        {
            textWindowFrame.alpha = (textWindowVisible) ? Mathf.InverseLerp(0, FADE_OUT_ANIM_DURATION, time) : Mathf.InverseLerp(FADE_OUT_ANIM_DURATION, 0, time);
            time += Time.deltaTime;
            yield return null;
        }
        textWindowFrame.alpha = (textWindowVisible) ? 1f : 0f;
    }
    #endregion
}
