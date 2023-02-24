public interface IAwaiter<T> // 結果を受け取る側のためのインターフェイス
{
    /// <summary>
    /// 処理が終了したかどうか。
    /// </summary>
    bool IsCompleted { get; }

    /// <summary>
    /// 処理の結果。
    /// </summary>
    T Result { get; }
}
class Awaiter<T> : IAwaiter<T> // 結果を設定する側の実装
{
    public bool IsCompleted { get; private set; }

    public T Result { get; private set; }

    /// <summary>
    /// 処理を終了して結果を設定する。
    /// </summary>
    public void SetResult(T result)
    {
        Result = result;
        IsCompleted = true;
    }
}
