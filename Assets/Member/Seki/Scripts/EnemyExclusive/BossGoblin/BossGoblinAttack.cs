using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;
using Cysharp.Threading.Tasks;

public class BossGoblinAttack : MonoBehaviour
{
    [SerializeField] int _attackDamage = 0;

    [SerializeField] EnemyState _enemyState;

    [SerializeField] private GameObject _parentObj;

    public bool _boomerangAttack = false;

    private Vector3 _defaultPos;

    private Quaternion _defaultRot;

    //元の場所に戻る用
    private bool _returnPos;

    //攻撃済みか
    public bool _attacked = false;

    private void Awake()
    {
        //元の位置情報を格納
        _defaultPos = transform.position;
        _defaultRot = transform.rotation;
    }

    private void Update()
    {

    }

    /// <summary>
    /// 攻撃力格納
    /// </summary>
    /// <param name="damage"></param>
    public void SetAttackDamage(int damage)
    {
        _attackDamage = damage;
    }

    private async void OnTriggerEnter2D(Collider2D collision)
    {
        //ブーメラン攻撃時に壁に衝突したとき
        if (_boomerangAttack)
        {
            if (collision.gameObject.tag == "Wall")
            {
                transform.DOKill();
                Debug.Log("ブーメラン、壁に衝突");
                _boomerangAttack = false;
                _returnPos = true;
                await BackBoomerang();
                return;
            }
        }


        //無効タグか判定
        for (int i = 0; i < _enemyState._notEnemyAttackTag.Length; i++)
        {
            if (_enemyState._notEnemyAttackTag[i] == collision.tag)
            {
                //Debug.Log("return");
                return;
            }
        }
        //Debug.Log("アタック");
        PlayerTest player = collision.gameObject.GetComponent<PlayerTest>();
        //player.ReceivedDamage(_attackDamage);
    }


    /// <summary>
    /// ブーメラン攻撃
    /// </summary>
    [Button]
    public async void BoomerangAttack()
    {
        _boomerangAttack = true;
        //元の位置格納
        _defaultPos = transform.position;
        _defaultRot = transform.rotation;

        //位置調整
        _parentObj.transform.position = new Vector3(_parentObj.transform.position.x, _parentObj.transform.position.y - 1.0f, _parentObj.transform.position.z);
        await gameObject.transform.DOMove(new Vector3(-5.0f, 0, 0), 3.0f).SetRelative().AsyncWaitForCompletion();
        if (_boomerangAttack)
        {
            await BackBoomerang();
            _boomerangAttack = false;

        }
    }


    //ブーメラン帰
    private async UniTask BackBoomerang()
    {
        await this.transform.DOMove(_defaultPos, 1.0f).AsyncWaitForCompletion();
    }

    //こん棒攻撃
    [Button]
    public async void BlowAttackSet()
    {
        //アクティブに
        _parentObj.SetActive(true);
        //情報格納
        _defaultPos= transform.position;
        _defaultRot= transform.rotation;
        //アタック中
        _boomerangAttack = false;
        _attacked = true;
        //アタックアニメーション
        await _parentObj.transform.DORotate(new Vector3(0, 0, -100), 1, RotateMode.LocalAxisAdd).AsyncWaitForCompletion();
        //アタック終了
        _attacked = false;
        //初期化
        transform.position = _defaultPos;
        transform.rotation = _defaultRot;
        _parentObj.SetActive(false);
    }
}
