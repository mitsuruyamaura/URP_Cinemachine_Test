using UnityEngine;
using LineTrace;

/// <summary>
/// ���[�g�̐ݒ�p�N���X
/// </summary>
public class RootChanger : MonoBehaviour
{
    [SerializeField, Header("���[�g���Ƃɓo�^")]
    private LineManager[] lineManagers;


    void Start() {
        // �f�o�b�O�p�ɁA�����_���ȃ��[�g���P�I�����ă��[�g��ݒ�
        SetLine(Random.Range(0, lineManagers.Length));
    }

    /// <summary>
    /// ���[�g�̐ݒ�
    /// </summary>
    /// <param name="lineIndex"></param>
    public void SetLine(int lineIndex) {

        // ���p���� LineManager ���P�ݒ肵�A��������[�g�Ƃ���
        for (int i = 0; i < lineManagers.Length; i++) {
            if (i == lineIndex) {
                lineManagers[i].gameObject.SetActive(true);
            } else {
                lineManagers[i].gameObject.SetActive(false);
            }
        }
    }
}