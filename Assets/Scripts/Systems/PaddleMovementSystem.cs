using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

//Worker thread
public class PaddleMovementSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float DeltaTime = Time.DeltaTime;
        float yBound = GameManager.GManager.yBound;

        JobHandle myJob = Entities.ForEach((ref Translation trans, in PaddleMovementData data) =>
        {
            trans.Value.y = math.clamp(trans.Value.y + (data.Speed * data.Direction * DeltaTime), -yBound, yBound);
        }).Schedule(inputDeps);
        return myJob;
    }
}
