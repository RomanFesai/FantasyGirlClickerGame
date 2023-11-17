using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingDoll : EnemyBehaviour
{
    public override void Die()
    {
        DestroyDolls.dollsDestroyed++;
        base.Die();
    }
}
