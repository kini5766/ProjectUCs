
// --
// 스텟에 증감 연산 사용을 지양하기 위한 클래스
// 상태이상 등으로 증감하고 싶다면 자식으로 추가하고,
// 복구되면 자식에서 제거하는 방법을 권장합니다.
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
// CStatusInstance의 Local스텟을 참조
// 부모를 설정할 수 있으나 자식을 추가할 수 없다.
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
