using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager GManager;

    public GameObject ballPrefab;

    public float xBound = 3f;
    public float yBound = 8f;
    public float ballSpeed = 3f;
    public float respawnDelay = 2f;
    public int[] playerScores;

    public Text mainText;
    public Text[] playerTexts;

    private Entity _ballEntityPrefab;
    private EntityManager _manager;

    private WaitForSeconds _oneSecond;
    private WaitForSeconds _delay;

    private void Awake()
    {
        if (GManager != null && GManager != this)
        {
            Destroy(gameObject);
            return;
        }

        GManager = this;
        playerScores = new int[2];

        _manager = World.DefaultGameObjectInjectionWorld.EntityManager;
        BlobAssetStore blob = new BlobAssetStore();
        GameObjectConversionSettings settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, blob);
        _ballEntityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(ballPrefab, settings);
        
        _oneSecond = new WaitForSeconds(1f);
        _delay = new WaitForSeconds(respawnDelay);
        StartCoroutine(CountDownAndSpawnBall());
    }

    public void PlayerScored(int PlayerID)
    {
        playerScores[PlayerID]++;
        for (int i = 0; i < playerScores.Length && i < playerTexts.Length; i++)
        {
            playerTexts[i].text = playerScores[i].ToString();
        }

        StartCoroutine(CountDownAndSpawnBall());
    }

    IEnumerator CountDownAndSpawnBall()
    {
        mainText.text = "Get Ready";
        yield return _delay;
        
        mainText.text = "3";
        yield return _oneSecond;
        
        mainText.text = "2";
        yield return _oneSecond;
        
        mainText.text = "1";
        yield return _oneSecond;
        
        mainText.text = "";

        SpawnBall();

    }

    void SpawnBall()
    {
        Entity ball = _manager.Instantiate(_ballEntityPrefab);
        Vector3 Direction = new Vector3(UnityEngine.Random.Range(0,2) == 0 ? -1 : 1, UnityEngine.Random.Range(-.5f, .5f), 0f).normalized;
        Vector3 Speed = Direction * ballSpeed;

        PhysicsVelocity velocity = new PhysicsVelocity()
        {
            Linear = Speed,
            Angular = float3.zero
        };
        _manager.AddComponentData(ball, velocity);
    }
}
