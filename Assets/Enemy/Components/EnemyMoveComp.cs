using Unity.Entities;

namespace Enemy
{
    [GenerateAuthoringComponent]
    public struct EnemyMoveSpeedComp : IComponentData
    {
        public EnemyMoveSpeedComp(float speed)
        {
            Value = speed;
        }
        public float Value;
    }
}