using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cysharp.Threading.Tasks;

/// <summary>
/// �X�v���b�h�V�[�g����擾�����f�[�^���V�[�g�P�ʂŔC�ӂ̃X�N���v�^�u���E�I�u�W�F�N�g�ɒl�Ƃ��Ď�荞��
/// </summary>
[RequireComponent(typeof(GSSReader))]
public class GSSReceiver : MonoBehaviour {

    public static GSSReceiver instance;

    public bool IsLoading { get; set; }


    private void Awake() {   // Awake �őҋ@�������ꍇ�ɂ� async + UniTask �ɂ���BUniTaskVoid �ɂ��Ă� await ����ΏI���܂őҋ@���邪�A���̃N���X�͓����Ă��܂�

        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

        Debug.Log("GSS �f�[�^�擾�J�n");

        // GSS �̃f�[�^�擾����
        PrepareGSSLoadStartAsync().Forget();

        Debug.Log("Awake �I��");
    }

    /// <summary>
    /// GSS �̃f�[�^�擾����
    /// </summary>
    /// <returns></returns>
    private async UniTask PrepareGSSLoadStartAsync() {

        IsLoading = true;

        // �ǂݍ���łȂ��ꍇ�ɂ́AGSS ���擾���� SO �Ɏ擾����
        await GetComponent<GSSReader>().GetFromWebAsync();

        IsLoading = false;

        Debug.Log("GSS �f�[�^�� SO �Ɏ擾");
    }

    /// <summary>
    /// �C���X�y�N�^�[���� GSSReader �� OnLoadEnd �ɂ��̃��\�b�h��ǉ����邱�Ƃ� GSS �̓ǂݍ��݊������ɃR�[���o�b�N�����
    /// </summary>
    public void OnGSSLoadEnd() {
        GSSReader reader = GetComponent<GSSReader>();

        // �X�v���b�h�V�[�g����擾�����e�V�[�g�̔z��� List �ɕϊ�
        List<SheetData> sheetDataslist = reader.sheetDatas.ToList();

        // ��񂪎擾�ł����ꍇ
        if (sheetDataslist != null) {

            // �X�N���v�^�u���E�I�u�W�F�N�g�ɑ��
            DataBaseManager.instance.enemyDataSO.enemyDataList =
                new List<EnemyData>(sheetDataslist.Find(x => x.SheetName == SheetName.EnemyData).DatasList.Select(x => new EnemyData(x)).ToList());

            // TODO ���� SO ��ǉ�����

        }
    }
}

