using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class CUIComponent : MonoBehaviour
{
    private void Awake()
    {
        Init();
    }

    public abstract void Setup();
    public abstract void Configure();

    [Button("Configure Now")]
    public void Init()
    {
        Setup();
        Configure();
    }

    private void OnValidate()
    {
        Init();
    }
}
