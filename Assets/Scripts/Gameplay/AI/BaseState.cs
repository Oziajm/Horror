using System;
using UnityEngine;
public abstract class BaseState
{
    public BaseState(GameObject gameObj) 
    {
        parentGameObj = gameObj;
        parentTransform = gameObj.transform;
    }

    public abstract Type Tick();

    protected GameObject parentGameObj;
    protected Transform parentTransform;

}
