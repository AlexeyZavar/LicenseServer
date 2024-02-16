#region

using System.Security.Claims;
using System.Text.Encodings.Web;
using Ardalis.Result;
using LicenseServer.Core.Services;
using LicenseServer.Database.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

#endregion

namespace LicenseServer.Authentication;

public sealed class XAuthHandler : AuthenticationHandler<XAuthSchemeOptions>
{
    private readonly LicenseService _licenseService;

    /// <inheritdoc />
    public XAuthHandler(LicenseService licenseService, IOptionsMonitor<XAuthSchemeOptions> options,
                        ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder,
        clock)
    {
        _licenseService = licenseService;
    }

    /// <inheritdoc />
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey(HeaderNames.Authorization))
        {
            return AuthenticateResult.Fail("No license provided");
        }

        var header = Request.Headers.Authorization[0];

        if (string.IsNullOrWhiteSpace(header))
        {
            return AuthenticateResult.Fail("No license provided");
        }

        var isParsed = Guid.TryParse(header, out var licenseId);

        if (!isParsed)
        {
            return AuthenticateResult.Fail("Wrong license id");
        }

        var licenseResult = licenseId != XAuthSchemeConstants.RootLicenseId
                                ? await _licenseService.Get(licenseId)
                                : GenerateRootLicense();

        if (!licenseResult.IsSuccess)
        {
            return AuthenticateResult.Fail("License not found");
        }

        var license = licenseResult.Value;

        var isExpired = license.Until < DateTime.Now;

        var claims = new List<Claim>
        {
            new(XClaimTypes.Id, license.Id.ToString())
        };
        if (!isExpired)
        {
            claims.Add(new Claim(XClaimTypes.Permissions, "NotExpired"));
        }

        claims.AddRange(license.Features.Select(x => new Claim(x.ProductFeature.Name, x.Value ?? "")));

        var claimsIdentity = new ClaimsIdentity(claims, XAuthSchemeConstants.SchemeName);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        var ticket = new AuthenticationTicket(claimsPrincipal, XAuthSchemeConstants.SchemeName);

        return AuthenticateResult.Success(ticket);
    }

    private Result<License> GenerateRootLicense()
    {
        return Result<License>.Success(new License
        {
            Id = XAuthSchemeConstants.RootLicenseId,
            Until = DateTime.Now.AddYears(30),
            Features = new List<LicenseFeature>
            {
                new()
                {
                    ProductFeature = new ProductFeature
                    {
                        Name = XClaimTypes.Permissions
                    },
                    Value = "Root"
                }
            }
        });
    }
}
