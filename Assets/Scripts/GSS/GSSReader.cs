using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;

/// <summary>
/// �V�[�g���̓o�^�p
/// </summary>
public enum SheetName {
    None,        // �����̍쐬�����X�v���b�h�V�[�g���̃V�[�g�̖��O��o�^���܂�
    EnemyData,

}

/// <summary>
/// �ǂݍ��ރV�[�g�̃f�[�^�Q�B�V�[�g�̏����Ǘ����܂�
/// </summary>
[System.Serializable]
public class SheetData {
    public SheetName SheetName;
    public List<string[]> DatasList = new List<string[]>();
}

/// <summary>
/// �X�v���b�h�V�[�g�̃f�[�^���擾���ăV�[�g�ʂɉ�͂��܂�
/// ��͂��I��������AOnLoadEnd �ɓo�^�������\�b�h���Ăяo���܂�
/// ��͌��ʂ� SheetData �N���X�� DatasList �ϐ�����V�[�g���ƂɎ擾
/// </summary>
public class GSSReader : MonoBehaviour {

    public string SheetID = "�ǂݍ��ރX�v���b�h�V�[�g�̃A�h���X";

    public UnityEvent OnLoadEnd;�@�@�@// ���̕ϐ��ɃC���X�y�N�^�[���烁�\�b�h��o�^���Ă����ƁA�X�v���b�h�V�[�g��ǂݍ��݌�ɃR�[���o�b�N����

    [Header("�ǂݍ��݂����V�[�g����I��")]
    public SheetData[] sheetDatas;

    public async UniTask Reload() => await GetFromWebAsync();


    public async UniTask GetFromWebAsync() {

        // CancellationToken �̍쐬
        var token = this.GetCancellationTokenOnDestroy();

        // �����̃V�[�g�̓ǂݍ���
        for (int i = 0; i < sheetDatas.Length; i++) {

            // �V�[�g����������ǂݍ��ݐ��ύX����
            string url = "https://docs.google.com/spreadsheets/d/" + SheetID + "/gviz/tq?tqx=out:csv&sheet=" + sheetDatas[i].SheetName.ToString();

            // Web �� GoogleSpreadSheet ���擾
            UnityWebRequest request = UnityWebRequest.Get(url);

            // �擾�ł���܂őҋ@
            // �񓯊������̏����ɉ����� CancellationToken �̐ݒ���s���A�񓯊��������L�����Z�������ꍇ�ɂ͏�������~����悤�ɃZ�b�g����
            await request.SendWebRequest().WithCancellation(token);�@�@//�@<=�@���B�@���������������܂�

            //Debug.Log(request.downloadHandler.text);

            // �G���[���������Ă��邩�m�F
            bool protocol_error = request.result == UnityWebRequest.Result.ProtocolError ? true : false;
            bool connection_error = request.result == UnityWebRequest.Result.ConnectionError ? true : false;

            // �G���[������ꍇ
            if (protocol_error || connection_error) {

                // �G���[�\�����s���A�������I������
                Debug.LogError(request.error);
                return;
            }

            // GSS �̊e�V�[�g���Ƃ̃f�[�^�� List<string[]> �̌`�Ŏ擾
            sheetDatas[i].DatasList = ConvertToArrayListFromCSV(request.downloadHandler.text);
        }

        // GSSLoader �̃��\�b�h��o�^���Ă����Ď��s����
        OnLoadEnd.Invoke();
    }

    /// <summary>
    /// �擾���� GoogleSpreadSheet(GSS) �� CSV �t�@�C���̏��� ArrayList �`���ɕϊ�
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    private List<string[]> ConvertToArrayListFromCSV(string text) {
        StringReader reader = new StringReader(text);
        reader.ReadLine();  // 1�s�ڂ̓w�b�_�[���Ȃ̂ŁA�ǂݍ���ŉ������Ȃ��œǂݔ�΂�

        List<string[]> rows = new List<string[]>();

        while (reader.Peek() >= 0) {  // Peek ���\�b�h���g���Ɩ߂�l�̒l�ɂ��t�@�C���̖����܂ŒB���Ă��邩�m�F�ł���B�����ɂȂ�� -1 ���߂�̂ŁA�����Ȃ�܂ŌJ��Ԃ�
            string line = reader.ReadLine();        // ��s���ǂݍ���
            string[] elements = line.Split(',');    // �s�̃Z����,�ŋ�؂��Ă���̂ŁA����𕪊����ĂP�������̏�񂪓������z��ɂ���

            for (int i = 0; i < elements.Length; i++) {�@�@// 1���������o��
                if (elements[i] == "\"\"") {
                    continue;                       // ���o�����������󔒂ł���ꍇ�͏���
                }
                elements[i] = elements[i].TrimStart('"').TrimEnd('"');  // �����̍ŏ��ƍŌ�ɂ��� "" ���폜����
                //Debug.Log(elements[i]);
            }
            rows.Add(elements);
        }
        return rows;
    }
}