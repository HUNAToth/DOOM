using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SG{
    public class CombatStanceState : State
    {
        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager){
            //check for attack range
            //potentially circle player or walk around them
            //if in attack range return attack stance 
            return this;
        }
   
    }
}