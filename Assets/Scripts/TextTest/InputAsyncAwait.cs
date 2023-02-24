using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputAsyncAwait
{
    //public IEnumerator RunAsync()
    //{
    //    while (true)
    //    {
    //        Debug.Log("�}�E�X�̃{�^�����͂�҂��܂�");
    //        yield return WaitForMouseButtonDown(out var awaiter);

    //        // Awaiter �̏I����́A�K�����ʂ��ۏ؂���Ă���
    //        Debug.Log($"�}�E�X��{awaiter.Result}�{�^����������܂���");
    //        yield return null;
    //    }
    //}

    public IEnumerator WaitForMouseButtonDown(out IAwaiter<int> awaiter)
    {
        var awaiterImpl = new Awaiter<int>();
        var e = WaitForMouseButtonDown(awaiterImpl);
        awaiter = awaiterImpl;
        return e;
    }

    IEnumerator WaitForMouseButtonDown(Awaiter<int> awaiter)
    {
        // �ǂ̃}�E�X�{�^���������ꂽ�̂��A���ʂ�Ԃ������B
        while (true)
        {
            for (var i = 0; i < 3; i++)
            {
                if (Input.GetMouseButtonDown(i))
                {
                    awaiter.SetResult(i); // �������I���E���ʂ�ݒ�
                    yield break;
                }
            }

            yield return null;
        }
    }
}
