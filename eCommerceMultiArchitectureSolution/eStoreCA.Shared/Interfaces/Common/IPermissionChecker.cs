namespace eStoreCA.Shared.Interfaces;

public interface IPermissionChecker
{
    bool HasClaim(string requiredClaim);
}