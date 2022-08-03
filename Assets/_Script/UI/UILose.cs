using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILose : UiWindow<UILose>
{
    public void Retry()
    {
        Level.LevelStart();
    }
}
