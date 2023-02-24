public interface IAwaiter<T> // ���ʂ��󂯎�鑤�̂��߂̃C���^�[�t�F�C�X
{
    /// <summary>
    /// �������I���������ǂ����B
    /// </summary>
    bool IsCompleted { get; }

    /// <summary>
    /// �����̌��ʁB
    /// </summary>
    T Result { get; }
}
class Awaiter<T> : IAwaiter<T> // ���ʂ�ݒ肷�鑤�̎���
{
    public bool IsCompleted { get; private set; }

    public T Result { get; private set; }

    /// <summary>
    /// �������I�����Č��ʂ�ݒ肷��B
    /// </summary>
    public void SetResult(T result)
    {
        Result = result;
        IsCompleted = true;
    }
}
