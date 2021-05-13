using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L10Cube : MonoBehaviour
{

    [SerializeField] private float lifeTime = 2f;
    private void OnEnable()
    {
        this.StartCoroutine("LifeRoutine");
    }
    private void OnDisable()
    {
        this.StopCoroutine("LifeRoutine");
    }

    private IEnumerator LifeRoutine()
    {
        yield return new WaitForSecondsRealtime(this.lifeTime);
        Deactivate();
    }


    void Deactivate()
    {
        this.gameObject.SetActive(false);
    }




}
