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

    //���̏ꏊ�ɖ߂�p
    private bool _returnPos;

    //�U���ς݂�
    public bool _attacked=false;

    private void Awake()
    {
        //���̈ʒu�����i�[
        _defaultPos = transform.position;
        _defaultRot=transform.rotation;
    }

    private void Update()
    {

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
                Debug.Log("�u�[�������A�ǂɏՓ�");
                _boomerangAttack = false;
            �@�@_returnPos = true;
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
    /// �u�[�������U��
    /// </summary>
    [Button]
    public async void BoomerangAttack()
    {
        //�ʒu����
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

    //����_�U��
    [Button]
    public void BlowAttackSet()
    {
        _boomerangAttack = false;
        _attacked=true;
    }
}
