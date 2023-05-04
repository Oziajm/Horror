using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerInteractables : MonoBehaviour
{
    #region Variables
    private const float FADE_OUT_ANIM_DURATION = 0.25f;

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
    #endregion

    #region Unity Methods

    private void Update()
    {
        bool lastVisibleValue = textWindowVisible;
        textWindowVisible = false;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hitInfo, maxDistance, layers))
        {
            if (hitInfo.collider.TryGetComponent<Interactable>(out Interactable interactable) && interactable.active)
            {
                useText.text = interactable.GetHoverText();
                textWindowVisible = true;
                if(Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact();
                }
            }
        }
        if(lastVisibleValue != textWindowVisible)
        {
            StopCoroutine(DoFadeInOutAnimation());
            StartCoroutine(DoFadeInOutAnimation());
        }
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
