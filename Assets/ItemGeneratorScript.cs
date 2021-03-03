using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ItemGeneratorScript : MonoBehaviour
{
    public float baseLuckSpawnItem = 10f;
    public float CurrentLuck = 10f;
    public float LuckPercent = 100f;
    public List<GameObject> lPowerUps = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnEnable()
    {
        CharacterData.eventEnemyDeath.AddListener(EnemyDied);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnemyDied(float score,Vector2 position,GameObject Levelpart)
    {
        float luckValue = 0.0f;
        luckValue = Random.Range(0.0f, LuckPercent);
        if (luckValue <= CurrentLuck)
        {
            SpawnPowerup(position,Levelpart);
            CurrentLuck = baseLuckSpawnItem;
        }
        else
        {
            CurrentLuck +=(score / 2) / 10;
        }
        
    }
    void SpawnPowerup(Vector2 position, GameObject LevelPart)
    {
        GameObject PU;
        PU = Instantiate(lPowerUps[Random.Range(0, lPowerUps.Count)]);
        PU.transform.parent = LevelPart.transform;
        PU.transform.position = position;
    }
}
