using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen = false;

    [SerializeField] private float duration = 1;
    [SerializeField] private Vector3 closedRotation;
    [SerializeField] private Vector3 openRotation;

    private Coroutine doorAnimation;
    private float closingTime;
    private float openingTime;

    private void Awake()
    {
        if(isOpen)
        {
            closingTime = 0;
            openingTime = duration;
            return;
        }
        closingTime = duration;
        openingTime = 0;
    }

    public void Open()
    {
        if(!isOpen)
        {
            if(doorAnimation != null)
            {
                StopCoroutine(doorAnimation);
            }
            doorAnimation = StartCoroutine(DoOpenRotation());
        }
    }

    IEnumerator DoOpenRotation()
    {
        Quaternion startRot = transform.rotation;
        Quaternion endRot = Quaternion.Euler(openRotation);

        isOpen = true;

        float newDuration = duration - openingTime;
        float newTime = 0;
        while(newTime < newDuration)
        {
            transform.rotation = Quaternion.Slerp(startRot, endRot, newTime / newDuration);
            yield return null;
            newTime += Time.deltaTime;
            openingTime += Time.deltaTime;
            closingTime -= Time.deltaTime;
        }
    }

    public void Close()
    {
        if (isOpen)
        {
            if (doorAnimation != null)
            {
                StopCoroutine(doorAnimation);
            }
            doorAnimation = StartCoroutine(DoCloseRotation());
        }
    }

    IEnumerator DoCloseRotation()
    {
        Quaternion startRot = transform.rotation;
        Quaternion endRot = Quaternion.Euler(closedRotation);

        isOpen = false;

        float newDuration = duration - closingTime;
        float newTime = 0;
        while (newTime < newDuration)
        {
            transform.rotation = Quaternion.Slerp(startRot, endRot, newTime/newDuration);
            yield return null;
            newTime += Time.deltaTime;
            closingTime += Time.deltaTime;
            openingTime -= Time.deltaTime;
        }
    }
}
