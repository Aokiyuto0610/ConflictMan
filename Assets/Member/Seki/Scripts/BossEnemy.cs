using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Cysharp.Threading.Tasks;

public class BossEnemy : MonoBehaviour
{
    [SerializeField, Label("EnemyState")] EnemyState _enemyState;

    [SerializeField, Label("何ステージ目のオブジェクトか")] private int _enemyAssignment=1;

    [SerializeField,Label("HP")] private int _enemyHp;

    [SerializeField, Label("攻撃力")] private int _enemyPower;

    [SerializeField, Label("移動スピード")] private float _enemyMoveSpeed;

    [SerializeField, Label("弱点倍率")] private float _enemyWeekPointDamage=1.5f;

    [SerializeField, Label("攻撃間隔")] private float _enemyAttackSpan;

    //span計測用
    private float _attackSpanTime = 0;

    //アタック中か
    private bool _attacking=false;

    [SerializeField] private EnemyAttack _enemyAttack;

    [SerializeField] private GameObject _enemySp;

    [SerializeField] private GameObject _enemyAttackObj;

    [SerializeField] private EnemyMove _enemyMove;

    [SerializeField] Animator _enemyAnimator;


    void Awake()
    {
        //開発時用
        Application.targetFrameRate = 60;

        //データ格納
        for (int i=0;i<_enemyState._stageEnemyDate.Length;i++)
        {
            if (_enemyState._stageEnemyDate[i]._stageNum == _enemyAssignment)
            {
                if (_enemyState._stageEnemyDate[i]._enemyTag == this.gameObject.tag)
                {
                    _enemyHp = _enemyState._stageEnemyDate[i]._enemyHp;
                    _enemyPower = _enemyState._stageEnemyDate[i]._enemyPower;
                    _enemyMove._moveSpeed= _enemyState._stageEnemyDate[i]._enemySpeed;
                    _enemyWeekPointDamage= _enemyState._stageEnemyDate[i]._weekPointDamage;
                    _enemyAttackSpan= _enemyState._stageEnemyDate[i]._attackSpan+1;
                    _enemyAttack.SetAttackDamage(_enemyState._stageEnemyDate[i]._enemyPower);
                    break;
                }
            }
        }

        //nullチェック
        /*
        if(_enemyAnimator == null)
        {
            Debug.LogError("Animatorがない");
        }
        */
    }

    private async void Update()
    {
        _attackSpanTime += Time.deltaTime;
        if(_attackSpanTime >= 0.1)
        {
            await EnemyAttackMove(1);
            _attackSpanTime = 0;
        }

        //移動仮
        //this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x+0.005f, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
    }


    //衝突したオブジェクトの判定
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("衝突");

        PlayerTest _objSt;

        //オブジェクト情報格納クラス取得ないなら終了
        if (collision.gameObject.GetComponent<PlayerTest>())
        {
            //Debug.Log("取得");
            _objSt = collision.gameObject.GetComponent<PlayerTest>();
        }
        else
        {
            //Debug.Log("取得できなかった");
            return;
        }

        //攻撃済みか、済なら終了
        if (_objSt._afterDamage)
        {
            //Debug.Log("攻撃済");
            return;
        }

        //衝突オブジェクトのタグ取得
        string ColTag = collision.gameObject.tag;


        //アタックオブジェクトなのか判定
        for (int j = 0; j < _enemyState._setDamageClasses.Length; j++)
        {
            if (ColTag == _enemyState._setDamageClasses[j]._tag)
            {
                //アタックオブジェクトの場合、無効化されたタグが設定されていないか
                for (int i = 0; i < _enemyState._notPlayerAttackTag.Length; i++)
                {
                    if (ColTag == _enemyState._notPlayerAttackTag[i])
                    {
                        Debug.Log("ダメージ無効化オブジェクトです");
                        return;
                    }
                }
                //Debug.Log("無効化オブジェクトじゃないよ〜");

                //ダメージ計算
                int ColDamage = _enemyState._setDamageClasses[j]._damage;
                ColDamage = (_enemyState._reflectionMagnification * _objSt._reflection) + ColDamage;
                Debug.Log("反射回数" + _objSt._reflection);

                //弱点判定がオンになっているか
                if (_objSt._weakness)
                {
                    //弱点ダメージ処理
                    _objSt._afterDamage = true;
                    WeekPointDamage(ColDamage);
                    return ;
                }
                else
                {
                    //通常ダメージ処理
                    _objSt._afterDamage = true;
                    UsuallyDamage(ColDamage);
                    return ;
                }
            }
        }
        //Debug.Log("抜けた");
    }

    /// <summary>
    /// 弱点ダメージ処理
    /// </summary>
    /// <param name="_colDamage">通常ダメージ数値</param>
    async void WeekPointDamage(int _colDamage)
    {
        int _weekDamage= Mathf.CeilToInt(_colDamage * 1.5f);
        Debug.Log("弱点ダメージ：" + _weekDamage);
        _enemyHp -= _weekDamage;
        if (_enemyHp <= 0)
        {
            await EnemySlain();
        }
    }

    /// <summary>
    /// 通常ダメージ処理
    /// </summary>
    /// <param name="_colDamage">ダメージ数値</param>
    async void UsuallyDamage(int _colDamage)
    {
        Debug.Log("通常ダメージ："+_colDamage);
        _enemyHp -= _colDamage;
        if (_enemyHp <= 0)
        {
            await EnemySlain();
        }
    }

    /// <summary>
    /// Deth処理
    /// </summary>
    /// <returns></returns>
    async UniTask EnemySlain()
    {
        for(int i=0; i < 2; i++)
        {
            _enemySp.SetActive(false);
            await UniTask.Delay(100);
            _enemySp.SetActive(true);
            await UniTask.Delay(100);
        }
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// アタック
    /// </summary>
    /// <param name="speed"></param>
    /// <returns></returns>
    [Button]
    async UniTask EnemyAttackMove(int speed=1)
    {
        if (!_attacking)
        {
            _attacking = true;
            _enemyAttackObj.SetActive(true);
            _enemyAnimator.SetFloat("AttackSpeed", speed);
            _enemyAnimator.SetTrigger("Attack");
            await UniTask.Delay(1000 / speed);
            _enemyAttackObj.SetActive(false);
            _enemyAnimator.Play("Idle");
            _attacking = false;
        }
        else
        {
            return;
        }
    }

}