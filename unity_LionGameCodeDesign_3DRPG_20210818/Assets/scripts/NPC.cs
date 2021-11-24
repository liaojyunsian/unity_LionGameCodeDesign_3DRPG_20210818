using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sky.Dialogue
{
    /// <summary>
    /// NPC 系統
    /// 偵測目標是否進入對話範圍
    /// 並開啟對話系統
    /// </summary>
    public class NPC : MonoBehaviour
    {
        [Header("對話資料")]
        public DataDialogue dataDialogue;
        [Header("相關資訊"), Range(0, 10)]
        public float checkPlayerRadius = 3f;
        public GameObject goTip;
        [Range(0, 10)]
        public float speedLookAt = 3;

        private Transform target;
        private bool startDialogueKey { get => Input.GetKeyDown(KeyCode.E); }

        [Header("對話系統")]
        public DialogueSystem dialogueSystem;

        /// <summary>
        /// 目前任務數量
        /// </summary>
        private int countCurrent;

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0, 1, 0.2f, 0.3f);
            Gizmos.DrawSphere(transform.position, checkPlayerRadius);
        }

        private void Update()
        {
            goTip.SetActive(CheckPlayer());
            LookAtPlayer();
            StartDialogue();
        }

        /// <summary>
        /// 檢查玩家是否進入 進入後記錄變形資訊
        /// </summary>
        /// <returns>玩家進入 傳回 true 否則 false</returns>
        private bool CheckPlayer()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, checkPlayerRadius, 1 << 6);

            if (hits.Length > 0) target = hits[0].transform;

            return hits.Length > 0;
        }
        /// <summary>
        /// 面向玩家
        /// </summary>
        private void LookAtPlayer()
        {
            if (CheckPlayer())
            {
                Quaternion angle = Quaternion.LookRotation(target.position - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, angle, Time.deltaTime * speedLookAt);
            }
        }

        /// <summary>
        /// 玩家進入範圍內 並且 按下只指定按鍵 請對話系統執行 開始對話 玩家退出範圍外 停止對話
        /// </summary>
        private void StartDialogue()
        {
            if (CheckPlayer() && startDialogueKey)
            {
                dialogueSystem.Dialogue(dataDialogue);
            }
            else if (!CheckPlayer())
            {
                dialogueSystem.StopDialogue();
            }
        }

        public void UpDateMissionCount()
        {
            countCurrent++;

            // 目前數量 等於 需求數量 狀態 等於 完成任務
            if (countCurrent == dataDialogue.countNeed)
            {
                dataDialogue.stateNPCMission = StateNPCMission.AfterMission;
            }
        }
    }
}