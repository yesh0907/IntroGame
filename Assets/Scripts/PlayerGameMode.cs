using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGameMode : MonoBehaviour
{
    void OnToggleGameMode()
    {
        Game.GameMode newGameMode = Game.GameMode.NORMAL;
        switch (Game.GameModeController.gameMode)
        {
            case Game.GameMode.NORMAL:
                newGameMode = Game.GameMode.DISTANCE;
                break;
            case Game.GameMode.DISTANCE:
                newGameMode = Game.GameMode.VISION;
                break;
            case Game.GameMode.VISION:
                newGameMode = Game.GameMode.NORMAL;
                break;
            default:
                break;
        }

        Game.GameModeController.gameMode = newGameMode;
    }
    
}
