using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionManager : MonoBehaviour {

    void Start()
    {
        StartCoroutine(MyCoroutine());
    }

    IEnumerator MyCoroutine()
    {
        yield return new WaitForSeconds(0.3f);    //Wait one frame

        DestroyMe();
    }


    public void DestroyMe()
    {
        Destroy(gameObject);
    }

}
