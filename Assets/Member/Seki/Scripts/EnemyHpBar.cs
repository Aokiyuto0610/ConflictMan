using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBar : MonoBehaviour
{
    [SerializeField] Slider _slider;

    [SerializeField] int _maxHp;

    [SerializeField,Min(0)] int _nowHp;

    float _hpParam;

    bool _active = false;

    // Update is called once per frame
    void Update()
    {
        //ƒp[ƒZƒ“ƒgŠi”[
        _hpParam = (float)_nowHp / (float)_maxHp;
        Debug.Log(_hpParam);
        if (_active)
        {
            if (_slider.value > _hpParam)
            {
                //-0.01
                _slider.value -= 0.01f;
            }else if (_slider.value<_hpParam)
            {
                _slider.value += 0.01f;
            }
        }
    }

    public void SetEnemyHp(int hp)
    {
        _maxHp = hp;
        _nowHp = hp;
        _active = true;
    }

    public void SetNowHp(int hp)
    {
        _nowHp = hp;
    }
}
