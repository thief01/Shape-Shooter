﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightRotation : MonoBehaviour
{
    void Update()
    {
        this.gameObject.transform.Rotate(0, 0, 360 * Time.deltaTime);
    }
}
