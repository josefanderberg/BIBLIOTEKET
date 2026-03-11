public class MemberRegistry
{
    private List<Member> members = new List<Member>();

    public void AddMember(Member member)
    {
        members.Add(member);
    }

    public Member? GetMemberById(string memberId)
    {
        return members.FirstOrDefault(m => m.MemberId == memberId);
    }

    public List<Member> GetAllMembers()
    {
        return new List<Member>(members);
    }

    public int GetTotalMemberCount()
    {
        return members.Count;
    }   

}
