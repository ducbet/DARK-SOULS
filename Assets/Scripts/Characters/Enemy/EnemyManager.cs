using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    [RequireComponent(typeof(AnimatorManager))]
    [RequireComponent(typeof(DetectCharacterStateMachine))]
    public class EnemyManager : CharacterManager
    {
        public static string ENEMY_TAG = "Enemy";

    }
}
