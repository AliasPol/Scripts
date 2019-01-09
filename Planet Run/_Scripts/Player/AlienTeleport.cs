using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienTeleport : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }

    public void EndTeleport()
    {
        FindObjectOfType<SpawnSystem>().ChangeSceneBackground();
        StartCoroutine(EndSequences());
    }

    private IEnumerator EndSequences()
    {
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }
}
