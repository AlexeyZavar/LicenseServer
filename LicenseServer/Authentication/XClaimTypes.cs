#region

using FastEndpoints.Security;

#endregion

namespace LicenseServer.Authentication;

public static class XClaimTypes
{
    public const string Id = "ID";
    public const string Permissions = Constants.PermissionsClaimType;
}
