using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpUI : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        PlayerController.Instance.HpChange += OnHpChange;

        OnHpChange(PlayerController.Instance.Hp);
    }
    void OnHpChange(int hp)
    {
        if(hp < 0)
            return;

        int hpOnShow = transform.childCount;
        int diff = hpOnShow - hp;
        while (diff > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
            diff--;
        }
        while (diff < 0)
        {
            Instantiate(transform.GetChild(0), transform);
            diff++;
        }
    }
    private void OnDestroy()
    {
        PlayerController.Instance.HpChange-= OnHpChange;
    }
}
