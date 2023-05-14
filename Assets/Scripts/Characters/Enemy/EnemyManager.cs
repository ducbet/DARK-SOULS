using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    [RequireComponent(typeof(AnimatorManager))]
    [RequireComponent(typeof(DetectCharacterStateMachine))]
    [RequireComponent(typeof(EnemyActionStateMachine))]
    [RequireComponent(typeof(EnemyMovementStateMachine))]
    public class EnemyManager : CharacterManager
    {
        public static string ENEMY_TAG = "Enemy";

    }
}
