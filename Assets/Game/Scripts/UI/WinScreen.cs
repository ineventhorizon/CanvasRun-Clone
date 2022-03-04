using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : UIBase
{
    public void NextLevel()
    {
        GameManager.Instance.NextLevel();
    }
}
