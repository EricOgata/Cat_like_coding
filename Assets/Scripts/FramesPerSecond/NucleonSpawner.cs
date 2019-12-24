using UnityEngine;
using System;
using System.Collections;

public class NucleonSpawner : MonoBehaviour {

    public float timeBetweenSpawns;
    public float spawnDistance;
    public Nucleon[] nucleonPrefabs;

    float timeSinceLastSpawn;

    void FixedUpdate() {
        timeSinceLastSpawn += Time.deltaTime;
        if(timeSinceLastSpawn >= timeBetweenSpawns) {
            timeSinceLastSpawn -= timeBetweenSpawns;
            SpawnNucleon();
        }
    }

    private void SpawnNucleon() {
        Nucleon prefab = nucleonPrefabs[UnityEngine.Random.Range(0, nucleonPrefabs.Length)];
        Nucleon spawn = Instantiate<Nucleon>(prefab);
        spawn.transform.localPosition = UnityEngine.Random.onUnitSphere * spawnDistance;
    }
}
