using UnityEngine;
using System.Collections;

public class StaminaController : PlayerSettings
{
    protected StaminaBarController staminaBarController;

    public Coroutine staminaRegeneration = null;
    
    private void Start()
    {
        staminaBarController = GetComponent<StaminaBarController>();

        staminaBarController.SetMaxValue(maxStamina);
        staminaBarController.SetStaminaVisible(false);
    }

    public void UpdateStaminaBarValues()
    {
        staminaBarController.SetStaminaVisible(stamina < maxStamina);
        staminaBarController.SetValue(stamina);
    }

    public IEnumerator RegenerateStamina()
    {
        yield return new WaitForSeconds(staminaRegenerationDelay);

        while (stamina < maxStamina)
        {
            stamina += Time.deltaTime * staminaRegenerationSpeed;
            staminaBarController.SetValue(stamina);
            yield return null;
        }

        staminaRegeneration = null;
    }
}
