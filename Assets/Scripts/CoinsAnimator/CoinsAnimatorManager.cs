using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Plataformer.Core.Singleton;
using DG.Tweening;
using System.Linq;

public class CoinsAnimatorManager : Singleton<CoinsAnimatorManager>
{
    [Header("Animation")]
    public float ScaleDuration = .2f;
    public float scaleTimeBetweenPieces = .1f;
    public Ease ease = Ease.OutBack;

    public List<ItemCollectableCoin> Itens;

    private void Start()
    {
        Itens = new List<ItemCollectableCoin>();
    }

    public void RegisterCoin(ItemCollectableCoin i)
    {
        if (!Itens.Contains(i))
        {
            Itens.Add(i);
            i.transform.localScale = Vector3.zero;
        }
           
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartAnimations();
        }
    }

    public void StartAnimations()
    {
        StartCoroutine(ScalePiecesByTime());
    }

    IEnumerator ScalePiecesByTime()
    {
        foreach (var p in Itens)
        {
            p.transform.localScale = Vector3.zero;
        }

        Sort();

        yield return null;

        for (int i = 0; i < Itens.Count; i++)
        {
            Itens[i].transform.DOScale(1, ScaleDuration).SetEase(ease);
            yield return new WaitForSeconds(scaleTimeBetweenPieces);

        }
    }

    private void Sort()
    {
        Itens = Itens.OrderBy(x => Vector3.Distance(this.transform.position, x.transform.position)).ToList();
    }
}
