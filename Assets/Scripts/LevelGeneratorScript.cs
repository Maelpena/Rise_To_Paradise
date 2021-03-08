using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelGeneratorScript : MonoBehaviour
{
    public List<GameObject> lAllLevelsPart = new List<GameObject>();
    public List<GameObject> lInGameLevelsPart = new List<GameObject>();
    public List<GameObject> ListMob = new List<GameObject>();
    public int nbMaxLevelPartInGame = 6;
    public int nbPlayerOnForDestroyOldParts = 3;
    public Transform Player;
    public int indexPlayerIsOn = 0;
    public bool isAlreadySpawning = false;
    private float brightness = 0.01f;
    private float blueness = 0f;
    public float actualDifficulty = 1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnFewLevelParts());
    }

    // Update is called once per frame
    void Update()
    {
        if(Player != null)
        {
            CheckForNewLevelPart();
        }
        
    }

    IEnumerator SpawnLevelPart(GameObject levelPart)
    {
        if (lInGameLevelsPart.Count < nbMaxLevelPartInGame)
        {
            GameObject LV;
            LV = Instantiate(levelPart);
            LV.transform.position = lInGameLevelsPart[lInGameLevelsPart.Count - 1].GetComponentInChildren<EndOfLevel>().transform.position;

            foreach (GameObject tilemap in LV.GetComponent<LevelPart>().TileMaps)
            {
                tilemap.GetComponent<Tilemap>().color = new Color(0.35f + brightness - blueness, 0.35f + brightness, 0.35f + brightness, 255f);
            }
            if (brightness + 0.025f <= 0.65f )
            {
                brightness += 0.025f;
            } else if (blueness < 0.5f )
            {
                blueness += 0.01f;
                brightness = 0.65f;
            } else
            {
                brightness = 0.65f;
                blueness = 0.5f;
            }

            SpawnEnemiesFollowingDifficulty(LV);
          
            lInGameLevelsPart.Add(LV);
        }
        isAlreadySpawning = false;
        yield return null;

    }
    void SpawnEnemiesFollowingDifficulty(GameObject LV)
    {
        UpdateDifficulty();
        GameObject ennemyToSpawn = new GameObject();
        List<Vector4> lSpawns = LV.GetComponent<LevelPart>().lSpawnDifficulty;
        for (int i = 0; i < lSpawns.Count; i++)
        {
            if(lSpawns[i].x == actualDifficulty)
            {
                ennemyToSpawn = Instantiate(ListMob[Mathf.RoundToInt(lSpawns[i].w)], LV.transform);
                ennemyToSpawn.transform.position = new Vector2(lSpawns[i].y, lSpawns[i].z); 
                ennemyToSpawn.transform.position = ennemyToSpawn.transform.position + LV.transform.position;
            }
            else if(lSpawns[i].x> actualDifficulty)
            {
                break;
            }
        }
        /*
        foreach (GameObject spawn in LV.GetComponent<LevelPart>().spawnPositions)
        {
            
            if (Random.Range(0, 100) + DifficultyFromPlayerHeight > 70)
            {
                if (Random.Range(0, 100) > 70)
                {
                    ennemy = Instantiate(ListMob[0], LV.transform);
                    ennemy.transform.position = new Vector2(0, 0);
                    ennemy.transform.position = spawn.transform.position + LV.transform.position;
                }
                else
                {
                    ennemy = Instantiate(ListMob[1], LV.transform);
                    ennemy.transform.position = new Vector2(0, 0);
                    ennemy.transform.position = spawn.transform.position + LV.transform.position;
                }
            }
        }*/
    }

    void UpdateDifficulty()
    {
        GameObject GM = GameObject.Find("GameManager");
        if (GM.GetComponentInParent<ScoreRegister>().TotalScore > 500)
        {
            actualDifficulty = 3;
        }
        else if(GM.GetComponentInParent<ScoreRegister>().TotalScore > 200)
        {
            actualDifficulty = 2;
        }
        else
        {
            actualDifficulty = 1;
        }
    }
    IEnumerator AddNewLevelPartAndDestroyOldPart ()
    {
        yield return StartCoroutine(TryRemoveOldPart());
        yield return StartCoroutine(SpawnLevelPart(lAllLevelsPart[Random.Range(0, lAllLevelsPart.Count)]));    
        yield return null;
    }

    IEnumerator TryRemoveOldPart()
    {
        if (indexPlayerIsOn > nbPlayerOnForDestroyOldParts)
        {
            Destroy(lInGameLevelsPart[0]);
            lInGameLevelsPart.RemoveAt(0);
            indexPlayerIsOn--;
        }
        yield return null;
    }
    void CheckForNewLevelPart()
    {

        if (Player.position.y > lInGameLevelsPart[indexPlayerIsOn].GetComponentInChildren<EndOfLevel>().gameObject.transform.position.y)
        {
            indexPlayerIsOn++;
            if(!isAlreadySpawning)
            {
                isAlreadySpawning = true;
                StartCoroutine(AddNewLevelPartAndDestroyOldPart());
            }        
        }
    }

    IEnumerator SpawnFewLevelParts()
    {
        for (int i = 1; i < nbMaxLevelPartInGame; i++)
        {
            yield return StartCoroutine(SpawnLevelPart(lAllLevelsPart[Random.Range(0, lAllLevelsPart.Count)]));
        }
        indexPlayerIsOn = 0;

        yield return null;
    }
}
