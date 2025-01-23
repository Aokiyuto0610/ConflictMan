using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSE : MonoBehaviour
{
    [SerializeField] private string[] allowedTags = { "Player" }; // �����ꂽ�^�O

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �ڐG�����I�u�W�F�N�g�̃^�O���擾
        string tag = collision.gameObject.tag;
        SoundManager.Instance.PlaySE(SESoundData.SE.Walltuch);

        // �����ꂽ�^�O�Ɋ܂܂�Ă���ꍇ�͉������Ȃ�
        if (IsAllowedTag(tag))
        {
            return;
        }

        // �����ꂽ�^�O�ȊO���G�ꂽ�Ƃ��̏���
        Debug.Log($"�֎~�^�O�I�u�W�F�N�g���G�ꂽ: {tag}");
        // �Ⴆ�΁ADestroy(collision.gameObject) ��A�x�����Đ��Ȃ�
    }

    private bool IsAllowedTag(string tag)
    {
        foreach (string allowedTag in allowedTags)
        {
            if (tag == allowedTag)
            {
                return true;
            }
        }
        return false;
    }
}