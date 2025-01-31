using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;
using Cysharp.Threading.Tasks;

public class BossGoblinAttack : MonoBehaviour
{
    [SerializeField] int _attackDamage = 0;

    [SerializeField] EnemyState _enemyState;

    [SerializeField] private GameObject _parentObj;

    [SerializeField, Label("�ǂ̂��炢��΂���")] float _distance = 5.0f;

    [SerializeField, Label("���b�Ŕ�΂���")] float _toTime = 3.0f;

    [SerializeField, Label("�߂鏊�v����")] float _backTime = 1.0f;

    public bool _boomerangAttack = false;

    private Vector3 _defaultPos;

    private Quaternion _defaultRot;

    //���̏ꏊ�ɖ߂�p
    private bool _returnPos;

    //�U������
    public bool _attacking = false;

    //�E������
    public bool _attackRight=false;

    [SerializeField] GameObject _bodySp;

    [SerializeField] BossGoblinMove _move;

    private void Start()
    {
    }

    
    private void Awake()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 1);
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
    public async UniTask BoomerangAttack()
    {
        _parentObj.SetActive(true);
        _attacking = true;
        _boomerangAttack = true;
        //���̈ʒu�i�[
        _defaultPos = transform.position;
        _defaultRot = transform.rotation;

        _bodySp.transform.DOLocalRotate(new Vector3(0, 0, 20), 0.5f, RotateMode.LocalAxisAdd).AsyncWaitForCompletion();
        if (_attackRight)
        {
            gameObject.transform.DOLocalRotate(new Vector3(0, 0, -700), _toTime, RotateMode.LocalAxisAdd).AsyncWaitForCompletion();
            await gameObject.transform.DOMove(new Vector3(_distance, -1.0f, 0), _toTime).SetRelative().AsyncWaitForCompletion();
        }
        else
        {
            gameObject.transform.DOLocalRotate(new Vector3(0, 0, -700), _toTime, RotateMode.LocalAxisAdd).AsyncWaitForCompletion();
            await gameObject.transform.DOMove(new Vector3(-_distance, -1.0f, 0), _toTime).SetRelative().AsyncWaitForCompletion();
        }
        if (_boomerangAttack)
        {
            await BackBoomerang();
            _boomerangAttack = false;
        }
    }


    //�u�[�������A
    private async UniTask BackBoomerang()
    {
        //_bodySp.transform.DOLocalRotate(new Vector3(0, 0, -20), 0.5f, RotateMode.LocalAxisAdd).AsyncWaitForCompletion();
        gameObject.transform.DOLocalRotate(new Vector3(0, 0, 700), _toTime, RotateMode.LocalAxisAdd).AsyncWaitForCompletion();
        await this.transform.DOMove(_defaultPos, _backTime).AsyncWaitForCompletion();
        transform.DOKill();
        _attacking=false;
        transform.position = _defaultPos;
        transform.rotation = _defaultRot;
        _parentObj.SetActive(false);
    }

    //����_�U��
    [Button]
    public async UniTask BlowAttackSet()
    {
        //�A�N�e�B�u��
        _parentObj.SetActive(true);
        _attacking = true;
        //���i�[
        _defaultPos= _parentObj.transform.position;
        _defaultRot= _parentObj.transform.rotation;
        //�A�^�b�N��
        _boomerangAttack = false;
        //�A�^�b�N�A�j���[�V����
        _bodySp.transform.DOKill();
        _move.MoveReset();
        _bodySp.transform.DOLocalRotate(new Vector3(0, 0, 20), 1f, RotateMode.LocalAxisAdd).AsyncWaitForCompletion();
        await _parentObj.transform.DOLocalRotate(new Vector3(0, 0, -100), 1, RotateMode.LocalAxisAdd).AsyncWaitForCompletion();
        _bodySp.transform.Rotate(0, 0, -20);
        //�A�^�b�N�I��
        //������
        _parentObj.transform.rotation = _defaultRot;
        _parentObj.transform.position = _defaultPos;
        _attacking = false;
        _parentObj.SetActive(false);
    }
}
