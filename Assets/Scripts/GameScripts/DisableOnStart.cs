using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(500)]
public class DisableOnStart : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }

}
