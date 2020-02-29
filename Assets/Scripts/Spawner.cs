using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject mushroomPrefab;
    [SerializeField] int maxMushrooms = 1;
    [SerializeField] float delay = 1; // only meaningful when maxMushrooms > 1
    
    int mushrooms = 0;

    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            SpawnAttacker();
        }
    }

    private void SpawnAttacker()
    {
        if (mushrooms < maxMushrooms)
        {
            mushrooms += 1;
            Instantiate(mushroomPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        }
    }

    public void MushroomKilled()
    {
        mushrooms -= 1;
    }
}
