[System.Serializable]
public class EnemyData
{
    public int no;
    public int hp;
    public int attackPower;
    public float moveSpeed;

    /// <summary>
    /// �R���X�g���N�^
    /// </summary>
    /// <param name="datas"></param>
    public EnemyData(string[] datas) {
        no = int.Parse(datas[0]);
        hp = int.Parse(datas[1]);
        attackPower = int.Parse(datas[2]);
        moveSpeed = int.Parse(datas[3]);
    }

    /// <summary>
    /// �������q�p
    /// </summary>
    public EnemyData() {
    }
}
