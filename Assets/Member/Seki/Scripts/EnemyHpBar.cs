using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpBar : MonoBehaviour
{
    //HP取得元
    [SerializeField] BossGoblin _manager;

    //変更するスケール
    [SerializeField] Transform _barScale;

    //変更するカラー
    [SerializeField] SpriteRenderer _barColor;

    //色が変化する値
    [SerializeField] int[] _colorChange;

    [SerializeField] int _maxHp;

    [SerializeField,Min(0)] int _nowHp;

    float _hpParam;

    bool _active = false;

    // Update is called once per frame
    void Update()
    {
        //パーセント格納
        _hpParam = (float)_nowHp / (float)_maxHp;
        Debug.Log(_hpParam);
        if (_active)
        {
            if (_barScale.localScale.x > _hpParam)
            {
                Debug.Log("増える");
                _barScale.localScale=new Vector3(_barScale.localScale.x-0.01f,_barScale.localScale.y, _barScale.localScale.z);
            }
            else if(_barScale.localScale.x<=_hpParam)
            {
                Debug.Log("減る");
                _barScale.localScale = new Vector3(_barScale.localScale.x+0.01f, _barScale.localScale.y, _barScale.localScale.z);
                if (_barScale.localScale.x > 1f)
                {
                    Debug.Log("超えた");
                    return;
                }
            }
        }
        if (_barScale.localScale.x > 0.6)
        {
            _barColor.color = Color.green;
        }else if (_barScale.localScale.x <= 0.6)
        {
            if (_barScale.localScale.x <= 0.3)
            {
                _barColor.color = Color.red;
            }else if (_barScale.localScale.x > 0.3)
            {
                _barColor.color = Color.yellow;
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
