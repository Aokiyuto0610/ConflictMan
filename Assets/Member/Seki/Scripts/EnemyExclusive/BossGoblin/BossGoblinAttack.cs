using UnityEngine;

public class BossGoblinAttack : MonoBehaviour
{
    [SerializeField] int _attackDamage = 0;

    [SerializeField] EnemyState _enemyState;

    private bool _boomerangAttack=false;

    private Vector3 _defaultPos;

    private Quaternion _defaultRot;

    //���̏ꏊ�ɖ߂�p
    private bool _returnPos;

    private void Awake()
    {
        //���̈ʒu�����i�[
        _defaultPos = transform.position;
        _defaultRot=transform.rotation;
    }

    private void Update()
    {
        //���̏ꏊ�ɖ߂�
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
    /// �U���͊i�[
    /// </summary>
    /// <param name="damage"></param>
    public void SetAttackDamage(int damage)
    {
        _attackDamage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�u�[�������U�����ɕǂɏՓ˂����Ƃ�
        if (_boomerangAttack)
        {
            if (collision.gameObject.tag == "Wall")
            {
                Debug.Log("�u�[�������ǂɏՓ�");
                _boomerangAttack = false;
                return;
            }
        }


        //�����^�O������
        for (int i = 0; i <_enemyState._notEnemyAttackTag.Length; i++)
        {
            if (_enemyState._notEnemyAttackTag[i] == collision.tag)
            {
                //Debug.Log("return");
                return;
            }
        }
        //Debug.Log("�A�^�b�N");
        PlayerTest player = collision.gameObject.GetComponent<PlayerTest>();
        //player.ReceivedDamage(_attackDamage);
    }


    /// <summary>
    /// �u�[�������U�������Z�b�g
    /// </summary>
    public void BoomerangAttackSet()
    {
        _boomerangAttack = true;
    }

}
