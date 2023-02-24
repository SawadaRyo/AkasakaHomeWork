using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputAsyncAwait
{
    //public IEnumerator RunAsync()
    //{
    //    while (true)
    //    {
    //        Debug.Log("マウスのボタン入力を待ちます");
    //        yield return WaitForMouseButtonDown(out var awaiter);

    //        // Awaiter の終了後は、必ず結果が保証されている
    //        Debug.Log($"マウスの{awaiter.Result}ボタンが押されました");
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
        // どのマウスボタンが押されたのか、結果を返したい。
        while (true)
        {
            for (var i = 0; i < 3; i++)
            {
                if (Input.GetMouseButtonDown(i))
                {
                    awaiter.SetResult(i); // 処理を終了・結果を設定
                    yield break;
                }
            }

            yield return null;
        }
    }
}
