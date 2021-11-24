using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Sky.Enemy
{
    /// <summary>
    /// �ĤH�欰
    /// �ĤH���A �Q ���� ���� �l�� ���� ���� ���`
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        #region ��� ���}
        [Header("���ʳt��"), Range(0, 20)]
        public float speed = 2.5f;
        [Header("�����O"), Range(0, 200)]
        public float attack = 35f;
        [Header("�d��G�l�ܻP����")]
        [Range(0, 7)]
        public float rangeAttack = 3.5f;
        [Range(7, 20)]
        public float rangeTrack = 8f;
        [Header("�����H������")]
        public Vector2 v2RandomWait = new Vector2(1f, 5f);
        [Header("�����H������")]
        public Vector2 v2RandomWalk = new Vector2(3, 7);
        #endregion

        #region ��� �p�H
        /// <summary>
        /// ���A
        /// </summary>
        [SerializeField]
        private StateEnemy state;
        /// <summary>
        /// �O�_���ݪ��A
        /// </summary>
        private bool isIdle;
        /// <summary>
        /// �O�_�������A
        /// </summary>
        private bool isWalk;
        private Animator ani;
        private NavMeshAgent nma;
        private string parameterIdleWalk = "�����}��";
        /// <summary>
        /// �H���樫�y��
        /// </summary>
        private Vector3 v3RandomWalk
        {
            get => Random.insideUnitSphere * rangeTrack + transform.position;
        }
        /// <summary>
        /// �H���樫�y�СQ�z�LAPI ���o���椺�i���쪺��m
        /// </summary>
        private Vector3 v3RandomWalkFinal;
        /// <summary>
        /// ���a�O�_�b�l�ܽd�򤺡Dtrue �O�Dfalsr �_
        /// </summary>
        private bool playerInTrackRange { get => Physics.OverlapSphere(transform.position, rangeTrack, 1 << 6).Length > 0; }

        #endregion

        [Header("�����ϰ�첾�P�ؤo")]
        public Vector3 v3AttackOffset;
        public Vector3 v3AttackSize = Vector3.one;


        #region �ϧ�ø�s
        private void OnDrawGizmos()
        {
            #region ���� �l�� �H���樫
            Gizmos.color = new Color(1, 0, 0.2f, 0.3f);
            Gizmos.DrawSphere(transform.position, rangeAttack);

            Gizmos.color = new Color(0.2f, 1, 0, 0.3f);
            Gizmos.DrawSphere(transform.position, rangeTrack);

            if (state == StateEnemy.Walk)
            {
                Gizmos.color = new Color(1, 0, 0.2f, 0.3f);
                Gizmos.DrawSphere(v3RandomWalkFinal, 0.3f);
            }
            #endregion

            #region �����I���P�w�ϰ�
            Gizmos.color = new Color(0.8f, 0.2f, 0.7f, 0.3f);
            //ø�s��ΡD�ݭn��ۨ������ɽШϥ� matrix ���w�y�Ш��׻P�ؤo
            Gizmos.matrix = Matrix4x4.TRS(
                transform.position +
                transform.right * v3AttackOffset.x +
                transform.up * v3AttackOffset.y +
                transform.forward * v3AttackOffset.z,
                transform.rotation, transform.localScale);
            Gizmos.DrawCube(Vector3.zero, v3AttackSize);
            #endregion
        }
        #endregion

        #region �ƥ�
        private Transform traPlayer;
        private string namePlayer = "���a�@";

        private void Awake()
        {
            ani = GetComponent<Animator>();
            nma = GetComponent<NavMeshAgent>();
            nma.speed = speed;

            traPlayer = GameObject.Find(namePlayer).transform;

            nma.SetDestination(transform.position);//������ �@�}�l�N���Ұ�
        }

        private void Update()
        {
            StateManager();
        }
        #endregion

        #region ��k �p�H
        /// <summary>
        /// ���A�޲z
        /// </summary>
        private void StateManager()
        {
            switch (state)
            {
                case StateEnemy.Idle:
                    Idle();
                    break;
                case StateEnemy.Walk:
                    Walk();
                    break;
                case StateEnemy.Track:
                    Track();
                    break;
                case StateEnemy.Attack:
                    Attack();
                    break;
                case StateEnemy.Hurt:
                    break;
                case StateEnemy.Dead:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// ���ݡQ�H�����ƫ�i�樫��
        /// </summary>
        private void Idle()
        {
            if (!targetIsDead && playerInTrackRange) state = StateEnemy.Track;//�p�G ���a�i�J �l�ܽd�� �N�����l�ܪ��A

            #region �i�J����
            if (isIdle) return;
            isIdle = true;
            #endregion

            ani.SetBool(parameterIdleWalk, false);
            StartCoroutine(IdleEffect());
        }

        /// <summary>
        /// ���ݮĪG
        /// </summary>
        /// <returns></returns>
        private IEnumerator IdleEffect()
        {
            float randomWait = Random.Range(v2RandomWait.x, v2RandomWait.y);
            yield return new WaitForSeconds(randomWait);

            state = StateEnemy.Walk;//�i�J�������A

            #region �X�h����
            isIdle = false;
            #endregion
        }

        /// <summary>
        /// �����Q�H�����ƫ�i�浥�ݪ��A
        /// </summary>
        private void Walk()
        {
            #region �������ϰ�
            if (!targetIsDead && playerInTrackRange) state = StateEnemy.Track;//�p�G ���a�i�J �l�ܽd�� �N�����l�ܪ��A

            nma.SetDestination(v3RandomWalkFinal);//�N�z��.�]�w�ت��a(�y��)
            ani.SetBool(parameterIdleWalk, nma.remainingDistance > 0.1f);//�����ʵe - ���ت��a�Z���j��0.1 �ɨ���
            #endregion

            #region �i�J����
            if (isWalk) return;
            isWalk = true;
            #endregion

            NavMeshHit hit;//��������I�� - �x�s����I����T
            NavMesh.SamplePosition(v3RandomWalk, out hit, rangeTrack, NavMesh.AllAreas);//��������.�����y��(�H���y�СD�I����T�D�b�|�D�ϰ�) - ���椺�i�樫���y��
            v3RandomWalkFinal = hit.position;//�̲׮y�� = �I����T �� �y��

            StartCoroutine(WalkEffect());
        }

        /// <summary>
        /// �����ĪG
        /// </summary>
        /// <returns></returns>
        private IEnumerator WalkEffect()
        {
            float randomWalk = Random.Range(v2RandomWalk.x, v2RandomWalk.y);
            yield return new WaitForSeconds(randomWalk);

            state = StateEnemy.Idle;//�i�J�������A

            #region �X�h����
            isWalk = false;
            #endregion
        }

        /// <summary>
        /// �O�_�l��
        /// </summary>
        private bool isTrack;

        /// <summary>
        /// �l�ܪ��a
        /// </summary>
        private void Track()
        {
            #region �i�J����
            if (!isTrack)
            {
                StopAllCoroutines();
            }

            isTrack = true;
            #endregion

            nma.isStopped = false;//������ �Ұ�
            nma.SetDestination(traPlayer.position);
            ani.SetBool(parameterIdleWalk, true);

            //�Z���p�󵥩���� �N�i �������A
            if (nma.remainingDistance <= rangeAttack)
            {
                state = StateEnemy.Attack;
            }
        }

        [Header("�����ɶ�"), Range(0, 5)]
        public float timeAttack = 2.5f;
        [Header("��������ǰe�ˮ`�ɶ�"), Range(0, 5)]
        public float delaySendDamage = 0.5f;
        /// <summary>
        /// �����ʵe�W��
        /// </summary>
        private string parameterAttack = "����Ĳ�o";
        /// <summary>
        /// �O�_����
        /// </summary>
        public bool isAttack;

        /// <summary>
        /// �������a
        /// </summary>
        private void Attack()
        {
            nma.isStopped = true;//����������
            ani.SetBool(parameterIdleWalk, false);//�����
            nma.SetDestination(traPlayer.position);
            LookAtPlayer();

            if (nma.remainingDistance > rangeAttack)
            {
                state = StateEnemy.Track;
            }

            //�p�G ���b������ �N���X (�קK���Ƨ���)
            if (isAttack)
            {
                return;
            }

            isAttack = true;//���b������

            ani.SetTrigger(parameterAttack);

            StartCoroutine(DelaySendDamageToTarget());//�Ұʩ���ǰe�ˮ`���ؼШ�{
        }

        /// <summary>
        /// �ؼЬO�_���`
        /// </summary>
        private bool targetIsDead;

        /// <summary>
        /// ����ǰe�ˮ`���ؼ�
        /// </summary>
        /// <returns></returns>
        private IEnumerator DelaySendDamageToTarget()
        {
            yield return new WaitForSeconds(delaySendDamage);

            //���z ���θI��(�����I�D�@�b�ؤo�D���סD�ϼh)
            Collider[] hits = Physics.OverlapBox(
                transform.position +
                transform.right * v3AttackOffset.x +
                transform.up * v3AttackOffset.y +
                transform.forward * v3AttackOffset.z,
                v3AttackSize / 2, Quaternion.identity, 1 << 6);

            // �p�G �I������ƶq�j�� �s�D�ǰe�����O���I�����󪺨��˨t��
            if (hits.Length > 0)
            {
                targetIsDead = hits[0].GetComponent<HurtSystem>().Hurt(attack);
            }
            if (targetIsDead)
            {
                TargetDead();
            }

            float waitToNextAttack = timeAttack - delaySendDamage;//�p��Ѿl�N�o�ɶ�
            yield return new WaitForSeconds(waitToNextAttack);//����

            isAttack = false;//��_ �������A
        }

        /// <summary>
        /// �ؼЦ��`
        /// </summary>
        private void TargetDead()
        {
            state = StateEnemy.Walk;
            isIdle = false;
            isWalk = false;
            nma.isStopped = false;
        }
        #endregion

        [Header("���۪��a�t��"), Range(0, 50)]
        public float speedLookAt = 10;

        /// <summary>
        /// ���V���a
        /// </summary>
        private void LookAtPlayer()
        {
            Quaternion angle = Quaternion.LookRotation(traPlayer.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, angle, Time.deltaTime * speedLookAt);
            ani.SetBool(parameterIdleWalk, transform.rotation != angle);
        }
    }
}