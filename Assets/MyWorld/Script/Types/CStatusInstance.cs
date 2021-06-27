
// --
// ���ݿ� ���� ���� ����� �����ϱ� ���� Ŭ����
// �����̻� ������ �����ϰ� �ʹٸ� �ڽ����� �߰��ϰ�,
// �����Ǹ� �ڽĿ��� �����ϴ� ����� �����մϴ�.
// --
public class CStatusInstance
{
    public CStatusInstance()
    {
        statusTree = new CStatusTree(GetLocal);
        statusTree.AddListener(OnUpdateStatus);
    }

    public CStatusInstance(FStatusData data)
    {
        localStatus = data;
        totalStatus = data;

        statusTree = new CStatusTree(GetLocal);
        statusTree.AddListener(OnUpdateStatus);
    }

    public FStatusData GetTotal() => totalStatus;
    public float MoveSpeed => totalStatus.MoveSpeed;
    public float Hp => totalStatus.Hp;
    public float Attack => totalStatus.Attack;
    public float Armor => totalStatus.Armor;
    public FStatusData GetLocal() => localStatus;
    public void SetLocal(FStatusData value)
    {
        FStatusData invData = value - localStatus;
        localStatus = value;
        statusTree.UpdateStatus(invData);
    }


    private FStatusData totalStatus;
    private FStatusData localStatus;
    private readonly CStatusTree statusTree;

    public CStatusTree Tree => statusTree;

    private void OnUpdateStatus(FStatusData data)
    {
        totalStatus += data;
    }

}


// --
// CStatusInstance�� Local������ ����
// �θ� ������ �� ������ �ڽ��� �߰��� �� ����.
// --
public class CStatusRef
{
    public CStatusRef(CStatusInstance inOrigin)
    {
        origin = inOrigin;
        statusTree = new CStatusTree(GetOrigin);

        origin.Tree.AddListener(OnUpdateStatus);
    }


    public FStatusData GetOrigin() => origin.GetTotal();

    public void SetParent(CStatusInstance instance)
    {
        if (instance == null)
        {
            UnLink();
            return;
        }

        instance.Tree.AddChild(statusTree);
    }

    public void UnLink()
    {
        statusTree.UnLink();
    }


    private readonly CStatusInstance origin;
    private readonly CStatusTree statusTree;

    private void OnUpdateStatus(FStatusData data)
    {
        statusTree.UpdateStatus(data);
    }
}
