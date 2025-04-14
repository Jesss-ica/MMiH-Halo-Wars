using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonCloud : MonoBehaviour
{
    public float lifeTime;
    public bool blink;

    private void Start()
    {
        StartCoroutine(TimeTillDeath(lifeTime));
    }
    public IEnumerator TimeTillDeath(float CooldownDuration)
    {
        yield return new WaitForSeconds(CooldownDuration - 1);
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        yield return new WaitForSeconds(0.2f);
        GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        Destroy(gameObject);
    }

}
