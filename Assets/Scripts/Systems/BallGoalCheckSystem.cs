using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Transforms;
using UnityEngine;
[AlwaysSynchronizeSystem]
public class BallGoalCheckSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.TempJob);
        
        Entities
            .WithAll<BallTag>()
            .WithoutBurst()
            .ForEach((Entity entity, in Translation trans) =>
            {
                float3 pos = trans.Value;
                float bound = GameManager.GManager.xBound;

                if (pos.x >= bound)
                {
                    GameManager.GManager.PlayerScored(0);
                    ecb.DestroyEntity(entity);
                }
                else if (pos.x <= -bound)
                {
                    GameManager.GManager.PlayerScored(1);
                    ecb.DestroyEntity(entity);
                }
            }).Run();
        
        ecb.Playback(EntityManager);
        ecb.Dispose();
        
        return default;
    }
}
