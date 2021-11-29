using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


namespace Sky.Dialogue
{
    /// <summary>
    /// ��ܨt��
    /// ��ܹ�ܮءB��ܤ��e���r�ĪG
    /// </summary>
    public class DialogueSystem : MonoBehaviour
    {
        #region ���
        [Header("��ܨt�λݭn����������")]
        public CanvasGroup groupDialogue;
        public Text textName;
        public Text textContent;
        public GameObject goTriangle;
        [Header("��ܶ��j"), Range(0, 10)]
        public float dialogueInterval = 0.3f;
        [Header("�U�@�q����")]
        public KeyCode dialogueKey = KeyCode.Mouse0;
        [Header("���r�ƥ�")]
        public UnityEvent onType;
        #endregion

        /// <summary>
        /// �}�l���
        /// </summary>
        public void Dialogue(DataDialogue data)
        {
            StopAllCoroutines();
            StartCoroutine(SeitchDialogueGroup());      //�Ұʨ�P�{��
            StartCoroutine(ShowDialogueContent(data));
        }

        /// <summary>
        /// �����ܡQ������ܥ\��D�����H�X
        /// </summary>
        public void StopDialogue()
        {
            StopAllCoroutines();
            StartCoroutine(SeitchDialogueGroup(false));
        }

        /// <summary>
        /// ������ܮظs��
        /// </summary>
        /// <returns></returns>
        /// <param name="fadeIn">�O�_�H�J:true �H�J�Dfalse �H�X</param>
        private IEnumerator SeitchDialogueGroup(bool fadeIn = true)
        {
            //�T���B��l
            //�y�k�G���L�� ? true ���G : false ���G;
            //�z�L���L�ȨM�w�n�W�[�o�ȡDtrue �W�[0.1�Dfalse �W�[ -0.1
            float increase = fadeIn ? 0.1f : -0.1f;

            for (int i = 0; i < 10; i++)                //�j����w���榸��
            {
                groupDialogue.alpha += increase;            //�s�D���� �z���� ���W
                yield return new WaitForSeconds(0.01f); //���ݮɶ�
            }
        }

        /// <summary>
        /// ��ܹ�ܤ��e
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private IEnumerator ShowDialogueContent(DataDialogue data)
        {
            textName.text = "";//�M�� ��ܪ�
            textName.text = data.nameDialogue;//��s ��ܪ�

            #region �B�z���A�P��ܸ��
            string[] dialogueContents = { };//�x�s ��ܤ��e

            switch (data.stateNPCMission)
            {
                case StateNPCMission.BeforeMission:
                    dialogueContents = data.beforeMission;
                    break;
                case StateNPCMission.Missionning:
                    dialogueContents = data.missionning;
                    break;
                case StateNPCMission.AfterMission:
                    dialogueContents = data.afterMission;
                    break;
                default:
                    break;
            }
            #endregion

            //�M�M�C�@�q���
            for (int j = 0; j < dialogueContents.Length; j++)
            {
                textContent.text = "";// �M�� ��ܤ��e
                goTriangle.SetActive(false);//���� ���ܹϥ�

                //�M�M��ܨC�@�Ӧr
                for (int i = 0; i < dialogueContents[j].Length; i++)
                {
                    onType.Invoke();
                    textContent.text += dialogueContents[j][i];
                    yield return new WaitForSeconds(dialogueInterval);

                }

                goTriangle.SetActive(true);//��� ���ܹϥ�

                //���򵥫� ��J ��ܫ��� null ���ݤ@�Ӽv�檺�ɶ�
                while (!Input.GetKeyDown(dialogueKey)) yield return null;
            }

            StartCoroutine(SeitchDialogueGroup(false));
        }
    }
}
