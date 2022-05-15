using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsOff : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("setFalseGameObj", 1);
    }

    private void setFalseGameObj()
    {
        gameObject.SetActive(false);
    }
}
