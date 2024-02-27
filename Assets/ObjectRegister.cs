using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectRegister : MonoBehaviour
{
    public Motion[] allobjects;

    private void Awake()
    {
        allobjects = GetComponentsInChildren<Motion>();
    }
}
