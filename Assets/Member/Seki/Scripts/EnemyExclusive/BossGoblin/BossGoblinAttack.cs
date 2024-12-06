using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;

public class BossGoblinAttack : MonoBehaviour
{
    [SerializeField] int _attackDamage = 0;

    [SerializeField] EnemyState _enemyState;

    [SerializeField] private GameObject _parentObj;

    public bool _boomerangAttack=false;

    private Vector3 _defaultPos;

    private Quaternion _defaultRot;

    //元の場所に戻る用
    private bool _returnPos;

    //攻撃済みか
    public bool _attacked=false;

    private void Awake()
    {
        //元の位置情報を格納
        _defaultPos = transform.position;
        _defaultRot=transform.rotation;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ブーメラン攻撃時に壁に衝突したとき
        if (_boomerangAttack)
        {
            if (collision.gameObject.tag == "Wall")
            {
                Debug.Log("ブーメラン、壁に衝突");
                _boomerangAttack = false;
            　　_returnPos = true;
                return;
            }
        }


        //無効タグか判定
        for (int i = 0; i <_enemyState._notEnemyAttackTag.Length; i++)
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
        //位置調整
        _parentObj.transform.position = new Vector3(_parentObj.transform.position.x, _parentObj.transform.position.y - 1.0f,_parentObj.transform.position.z);
        await ToBoomerang();
        await BackBoomerang();
    }

    private UniTask ToBoomerang()
    {
        this.gameObject.transform.DOMove(new Vector3(transform.position.x - 5.0f, transform.position.y, transform.position.z), 3.0f);
        return UniTask.CompletedTask;
    }

    private UniTask BackBoomerang()
    {
        this.transform.DOMove(_defaultPos, 1.0f);
        return UniTask.CompletedTask;
    }

    //こん棒攻撃
    [Button]
    public void BlowAttackSet()
    {
        _boomerangAttack = false;
        _attacked=true;
    }
}
