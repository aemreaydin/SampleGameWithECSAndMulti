using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Enemy
{
    public class EnemyMoveSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var deltaTime = Time.DeltaTime;
            var player = GameManager.FindObjectOfType<Player.PlayerMovement>();
            var position = player.transform.position;
            var playerPos = new float3(position.x,
                                       position.y,
                                       position.z);
            Entities
                .WithName("Enemy_Movement")
                .ForEach((ref Translation translation, ref Rotation rotation, in EnemyMoveSpeedComp moveSpeedComp) =>
                {
                    translation.Value += math.forward(rotation.Value) * moveSpeedComp.Value * deltaTime;
                    rotation.Value = quaternion.LookRotation(playerPos - translation.Value, new float3(0.0f, 1.0f, 0.0f));
                })
                .ScheduleParallel();
        }
    }
}
