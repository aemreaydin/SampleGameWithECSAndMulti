using Enemy;
using Player;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public int maxSpawnInSeconds = 20;
    public Transform player;

    private EntityManager _enemyManager;
    private EntityArchetype _enemyArchetype;

    [SerializeField] private Mesh enemyMesh;
    [SerializeField] private Material enemyMaterial;
    
    private void Start()
    {
        _enemyManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        _enemyArchetype = _enemyManager.CreateArchetype(
                                                        typeof(LocalToWorld),
                                                        typeof(RenderMesh),
                                                        typeof(RenderBounds),
                                                        typeof(Translation),
                                                        typeof(Rotation),
                                                        typeof(EnemyMoveSpeedComp));
        InvokeRepeating(nameof(SpawnEnemy), 1.0f, 1.0f);
    }

    // Update is called once per frame
    private void SpawnEnemy()
    {
        var randSpawn = Random.Range(0, maxSpawnInSeconds);

        var entityArray = new NativeArray<Entity>(randSpawn, Allocator.Temp);
        Debug.Log("Spawning");
        for (var i = 0; i != entityArray.Length; ++i)
        {
            entityArray[i] = _enemyManager.CreateEntity(_enemyArchetype);
            _enemyManager.SetComponentData(entityArray[i] , new EnemyMoveSpeedComp
            {
                Value = Random.Range(2.5f, 12.5f)
            });
            _enemyManager.SetComponentData(entityArray[i] , new Translation
            {
                Value = new float3(Random.Range(-30.0f, 30.0f), 1.0f, Random.Range(-30.0f, 30.0f))
            });
            _enemyManager.AddSharedComponentData(entityArray[i] , new RenderMesh
            {
                mesh = enemyMesh,
                material = enemyMaterial
            });
        }

        entityArray.Dispose();
    }
}
