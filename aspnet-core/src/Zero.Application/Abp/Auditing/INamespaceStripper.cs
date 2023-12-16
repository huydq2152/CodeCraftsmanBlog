namespace Zero.Abp.Auditing
{
    public interface INamespaceStripper
    {
        string StripNameSpace(string serviceName);
    }
}