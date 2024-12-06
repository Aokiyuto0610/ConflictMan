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

    //���̏ꏊ�ɖ߂�p
    private bool _returnPos;

    //�U���ς݂�
    public bool _attacked = false;

    private void Awake()
    {
        //���̈ʒu�����i�[
        _defaultPos = transform.position;
        _defaultRot = transform.rotation;
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

    private async void OnTriggerEnter2D(Collider2D collision)
    {
        //�u�[�������U�����ɕǂɏՓ˂����Ƃ�
        if (_boomerangAttack)
        {
            if (collision.gameObject.tag == "Wall")
            {
                transform.DOKill();
                Debug.Log("�u�[�������A�ǂɏՓ�");
                _boomerangAttack = false;
                _returnPos = true;
                await BackBoomerang();
                return;
            }
        }


        //�����^�O������
        for (int i = 0; i < _enemyState._notEnemyAttackTag.Length; i++)
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
        _boomerangAttack = true;
        //���̈ʒu�i�[
        _defaultPos = transform.position;
        _defaultRot = transform.rotation;

        //�ʒu����
        _parentObj.transform.position = new Vector3(_parentObj.transform.position.x, _parentObj.transform.position.y - 1.0f, _parentObj.transform.position.z);
        await gameObject.transform.DOMove(new Vector3(-5.0f, 0, 0), 3.0f).SetRelative().AsyncWaitForCompletion();
        if (_boomerangAttack)
        {
            await BackBoomerang();
            _boomerangAttack = false;

        }
    }


    //�u�[�������A
    private async UniTask BackBoomerang()
    {
        await this.transform.DOMove(_defaultPos, 1.0f).AsyncWaitForCompletion();
    }

    //����_�U��
    [Button]
    public async void BlowAttackSet()
    {
        //�A�N�e�B�u��
        _parentObj.SetActive(true);
        //���i�[
        _defaultPos= transform.position;
        _defaultRot= transform.rotation;
        //�A�^�b�N��
        _boomerangAttack = false;
        _attacked = true;
        //�A�^�b�N�A�j���[�V����
        await _parentObj.transform.DORotate(new Vector3(0, 0, -100), 1, RotateMode.LocalAxisAdd).AsyncWaitForCompletion();
        //�A�^�b�N�I��
        _attacked = false;
        //������
        transform.position = _defaultPos;
        transform.rotation = _defaultRot;
        _parentObj.SetActive(false);
    }
}
