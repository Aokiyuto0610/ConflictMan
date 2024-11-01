using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpManager_aoki : MonoBehaviour
{
    [Tooltip("Hp‚Ì‰æ‘œ")]
    [SerializeField]
    private Transform[] Hp;
    private int currenthp;
    [SerializeField]
    private Transform playerTransform;
    [Tooltip("HpImage‚ª“G‚ÌUŒ‚‚ª“–‚½‚é‚æ‚¤‚É‚È‚é‹——£")]
    [SerializeField]
    private float safeDistance = 0.5f;
    private Quaternion _rotation;

    // Start is called before the first frame update
    void Start()
    {
        currenthp = Hp.Length;
        UpdateHpDisplay();
        SetActiveHpCollider();
        _rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        PositionHpPlayer();
        LockRotation();
    }

    private void PositionHpPlayer()
    {
        Vector3 offset = new Vector3(0, 1.5f, 0);
        transform.position = playerTransform.position + offset;
    }

    public void TakeDamage()
    {
        if (currenthp > 0)
        {
            currenthp--;
            UpdateHpDisplay();
            SetActiveHpCollider();
        }
    }

    public void Heal()
    {
        if (currenthp < Hp.Length)
        {
            currenthp++;
            UpdateHpDisplay();
            SetActiveHpCollider();
        }
    }

    private void UpdateHpDisplay()
    {
        for (int i = 0; i < Hp.Length; i++)
        {
            Hp[i].gameObject.SetActive(i < currenthp);
        }
    }

    private void SetActiveHpCollider()
    {
        foreach (var HpObject in Hp)
        {
            HpObject.GetComponent<Collider2D>().enabled = false;
        }

        if(currenthp > 0)
        {
            Hp[currenthp - 1].GetComponent<Collider2D>().enabled = true;
        }
    }

    private void LockRotation()
    {
        transform.rotation = _rotation;
    }

    public void TakeDamageIfFarFromPlayer()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) > safeDistance)
        {
            TakeDamage();
        }
    }
}
