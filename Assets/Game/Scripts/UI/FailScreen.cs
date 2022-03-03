using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailScreen : UIBase
{
    public void Restart()
    {
        GameManager.Instance.RestartGame();
    }

}
