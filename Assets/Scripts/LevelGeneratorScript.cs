using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneratorScript : MonoBehaviour
{
    public List<GameObject> lAllLevelsPart = new List<GameObject>();
    public List<GameObject> lInGameLevelsPart = new List<GameObject>();
    public int nbMaxLevelPartInGame = 6;
    public int nbPlayerOnForDestroyOldParts = 3;
    public Transform Player;
    public int indexPlayerIsOn = 0;
    public bool isAlreadySpawning = false;
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
        if(lInGameLevelsPart.Count < nbMaxLevelPartInGame)
        {
            GameObject LV;
            LV = Instantiate(levelPart);
            LV.transform.position = lInGameLevelsPart[lInGameLevelsPart.Count - 1].GetComponentInChildren<EndOfLevel>().transform.position;
            lInGameLevelsPart.Add(LV);
            
        }
        isAlreadySpawning = false;
        yield return null;

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
