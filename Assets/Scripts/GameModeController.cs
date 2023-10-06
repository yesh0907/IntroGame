using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public enum GameMode
    {
        NORMAL,
        DISTANCE,
        VISION,
    };

    public class GameModeController : MonoBehaviour
    {

        public static GameMode gameMode;

        // Start is called before the first frame update
        void Start()
        {
            gameMode = GameMode.NORMAL;
        }
    }
}
