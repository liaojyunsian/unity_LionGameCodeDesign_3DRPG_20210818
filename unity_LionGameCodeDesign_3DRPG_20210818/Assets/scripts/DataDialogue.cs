using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sky.Dialogue
{
    /// <summary>
    /// ��ܨt�Ϊ����
    /// NPC �n��ܪ��T�Ӷ��q���e
    /// �����ȫe�B���ȶi�椤�B��������
    /// </summary>
    // ScriptableObject �~�Ӧ����O�|�ܦ��}���ƪ���
    // �i�N���}����Ʒ�����O�s�b�M�� Project ��
    // CreateAssetMenu ���O�ݩ�:�������O�إ߱M�פ����
    // menuName ���W�١D�i�� / ���h
    // fileName �ɮצW��
    [CreateAssetMenu(menuName = "Sky/��ܸ��", fileName = "NPC ��ܸ��")]
    public class DataDialogue : ScriptableObject
    {
        [Header("��ܪ̦W��")]
        public string nameDialogue;
        // �}�C:�O�s�ۦP������������c
        // Text Area �r����ݩʡD�i�]�w���
        [Header("���ȫe��ܤ��e"), TextArea(2, 7)]
        public string[] beforeMission;
        [Header("���ȶi�椤��ܤ��e"), TextArea(2, 7)]
        public string[] missionning;
        [Header("���ȧ�����ܤ��e"), TextArea(2, 7)]
        public string[] afterMission;
        [Header("���ȻݨD�ƶq"), Range(0, 100)]
        public int countNeed;
        //
        //
        [Header("NPC ���Ȫ��A")]
        public StateNPCMission stateNPCMission = StateNPCMission.BeforeMission;
    }
}