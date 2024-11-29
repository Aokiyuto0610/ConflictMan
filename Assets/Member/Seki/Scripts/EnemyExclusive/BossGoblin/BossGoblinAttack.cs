using UnityEngine;

public class BossGoblinAttack : MonoBehaviour
{
    [SerializeField] int _attackDamage = 0;

    [SerializeField] EnemyState _enemyState;

    private bool _boomerangAttack=false;

    private Vector3 _defaultPos;

    private Quaternion _defaultRot;

    //元の場所に戻る用
    private bool _returnPos;

    private void Awake()
    {
        //元の位置情報を格納
        _defaultPos = transform.position;
        _defaultRot=transform.rotation;
    }

    private void Update()
    {
        //元の場所に戻る
        if (_returnPos)
        {
            //XPos
            if (this.transform.position.x > _defaultPos.x)
            {
                this.transform.position = new Vector3(this.transform.position.x - 0.1f,this.transform.position.y,this.transform.position.z);
            }
            else if(this.transform.position.x < _defaultPos.x)
            {
                this.transform.position = new Vector3(this.transform.position.x + 0.1f, this.transform.position.y, this.transform.position.z);
            }
        }
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
                Debug.Log("ブーメラン壁に衝突");
                _boomerangAttack = false;
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
    /// ブーメラン攻撃かをセット
    /// </summary>
    public void BoomerangAttackSet()
    {
        _boomerangAttack = true;
    }

}
