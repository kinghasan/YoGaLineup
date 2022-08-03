using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIKeyState : GameEntity
{
    public GameObject[] Keys;

    public void Update()
    {
        var value = Save.Key.Value;

        for (var i = 0; i < Keys.Length; i++)
        {
            var key = Keys[i];
            key.gameObject.SetActive(i < value);
        }
    }
}
