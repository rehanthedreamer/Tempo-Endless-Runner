using UnityEngine;

public static class GameConstants
{
        // =============================
        // SAVE KEYS
        // =============================
        public const string SAVE_KEY_PREFIX = "PLAYER_SAVE_";
        public const string MUSIC_KEY = "MUSIC_ON";
        public const string SFX_KEY = "SFX_ON";

        // =============================
        // PERFORMANCE
        // =============================
        public const int POOL_INITIAL_SIZE = 4;
        public const float GAME_DIFFICULTY_MULTIPLIER = 1.05f;

        // =============================
        // Initial Data
        // =============================
        public static readonly Vector3 PLAYERPOS = new Vector3(-7.3f,1.63f,-1);
         public static readonly Vector3 P1_Pos = new Vector3(-8.92f,0,-1);
          public static readonly Vector3 P2_Pos  = new Vector3(-5.03f,0,0);
           public static readonly Vector3 P3_Pos  = new Vector3(7.92f, .46f, 0f);

}