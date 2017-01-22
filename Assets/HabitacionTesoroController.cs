using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class HabitacionTesoroController : MonoBehaviour {

    public SpriteRenderer fade;

    // Use this for initialization
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            fade.DOFade(1f, 2f).SetEase(Ease.Linear).OnComplete( () => {
                Invoke("End", 7f); }
            );
        }
    }

    void End()
    {
        SceneManager.LoadScene("menu");
    }
}
