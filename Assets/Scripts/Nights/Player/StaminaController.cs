using UnityEngine;
using System.Collections;

public class StaminaController : MonoBehaviour
{
    #region Variables

    [SerializeField] private PlayerSettings playerSettings;

    private StaminaBarController staminaBarController;

    private Coroutine staminaRegeneration = null;
    private Coroutine usageCoroutine = null;
    private float stamina;

    #endregion

    #region Unity Methods
    private void Start()
    {
        staminaBarController = GetComponent<StaminaBarController>();

        stamina = playerSettings.maxStamina;
        staminaBarController.SetMaxValue(stamina);
        staminaBarController.SetStaminaVisible(false);
    }

    #endregion

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
            staminaBarController.SetValue(stamina);
            yield return null;
        }

        staminaRegeneration = null;
    }

    private IEnumerator UseStamina()
    {
        while (stamina > 0)
        {
            stamina = Mathf.Clamp(stamina - Time.deltaTime * playerSettings.staminaUsageSpeed, 0, playerSettings.maxStamina);
            yield return null;
        }
    }

    #endregion
}
