using UnityEngine;

namespace Sky.Practice
{
    /// <summary>
    /// �{�Ѱj��
    /// while�Bdo while�Bfor�Bforeach
    /// </summary>
    public class LeamLoop : MonoBehaviour
    {
        private void Start()
        {
            //�j�� Loop
            //���ư���{�����e
            //�ݨD�G��X�Ʀr 1 - 5
            print(1);
            print(2);
            print(3);
            print(4);
            print(5);

            //while �j��
            //�y�k�Gif (���L��) {�{�����e}        - ���L�Ȭ� true ����@��
            //�y�k�Gwhile (���L��) {�{�����e}     - ���L�Ȭ� true ������� ��
            int a = 1;
            while (a < 6)
            {
                print("�j�� while�G" + a);
                a++;
            }

            //for �j��
            //�y�k�Gfor(��l��;����(���L��);�j�鵲������{��) {�{�����e}
            for (int i = 1; i < 6; i++)
            {
                print("�j�� while�G" + i);
            }
        }
    }
}