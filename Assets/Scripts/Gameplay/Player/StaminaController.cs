using UnityEngine;
using System.Collections;
using Gameplay.Managers;

public class StaminaController : MonoBehaviour
{
    #region Variables

    [SerializeField] 
    private PlayerSettings playerSettings;

    private Coroutine staminaRegeneration = null;
    private Coroutine usageCoroutine = null;
    private float stamina;

    #endregion

    private void Awake()
    {
        stamina = playerSettings.maxStamina;
    }

    #region Public Methods

    public bool IsFull()
    {
        return stamina >= playerSettings.maxStamina;
    }

    public bool IsAvaiable()
    {
        return stamina > 0;
    }

    public void StartRegenerating()
    {
        if (staminaRegeneration == null)
        {
            staminaRegeneration = StartCoroutine(RegenerateStamina());
        }
    }

    public void StopRegenerating()
    {
        if (staminaRegeneration != null)
        {
            StopCoroutine(staminaRegeneration);
            staminaRegeneration = null;
        }
    }

    public void StartUsing()
    {
        if (usageCoroutine == null)
        {
            usageCoroutine = StartCoroutine(UseStamina());
        }
    }

    public void StopUsing()
    {
        if (usageCoroutine != null)
        {
            StopCoroutine(usageCoroutine);
            usageCoroutine = null;
        }
    }

    #endregion

    #region Private Coroutines

    private IEnumerator RegenerateStamina()
    {
        yield return new WaitForSeconds(playerSettings.staminaRegenerationDelay);

        while (stamina < playerSettings.maxStamina)
        {
            stamina += Time.deltaTime * playerSettings.staminaRegenerationSpeed;
            EventsManager.Instance.SetStamina(stamina);
            yield return null;
        }

        EventsManager.Instance.ToggleStaminaBarVisibility(false);
        staminaRegeneration = null;
    }

    private IEnumerator UseStamina()
    {
        EventsManager.Instance.ToggleStaminaBarVisibility(true);

        while (stamina > 0)
        {
            stamina = Mathf.Clamp(stamina - Time.deltaTime * playerSettings.staminaUsageSpeed, 0, playerSettings.maxStamina);
            EventsManager.Instance.SetStamina(stamina);
            yield return null;
        }
    }

    #endregion
}