using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

//Main Thread
[AlwaysSynchronizeSystem]
public class PlayerInputSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        Entities.ForEach((ref PaddleMovementData moveData, in PaddleInputData inputData) =>
        {
            moveData.Direction = 0;

            moveData.Direction += Input.GetKey(inputData.upKey) ? 1 : 0;
            moveData.Direction -= Input.GetKey(inputData.downKey) ? 1 : 0;
        }).Run();
        return default;
    }
}
