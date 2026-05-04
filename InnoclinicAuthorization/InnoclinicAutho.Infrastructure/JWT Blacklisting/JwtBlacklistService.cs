using InnoclinicAutho.Application.Interfaces;
using InnoclinicAutho.Application.UseCases.Cache;

namespace InnoclinicAutho.Infrastructure.JWT_Blacklisting;

public class JwtBlacklistService : IJwtBlackListService
{
    private readonly ICacheService _cacheService;
    private readonly IJwtService _jwtService;

    public JwtBlacklistService(ICacheService cacheService, IJwtService jwtService)
    {
        _cacheService = cacheService;
        _jwtService = jwtService;
    }

    public async Task AddToBlacklistAsync(CachedUser cachedUser, string token, CancellationToken cancellationToken)
    {
        var cacheKey = $"blacklist:{token}";
        var expiryTime = _jwtService.ReadTokenExpiryTime(token);
        var expirationSpan = expiryTime.Subtract(DateTime.UtcNow);

        if (expirationSpan > TimeSpan.Zero)
            await _cacheService.SetAsync(
                cacheKey,
                cachedUser,
                expirationSpan,
                cancellationToken);
    }

    public async Task<bool> IsBlacklistedAsync(string token, CancellationToken cancellationToken = default)
    {
        var cacheKey = $"blacklist:{token}";
        return await _cacheService.KeyExistsAsync(cacheKey, cancellationToken);
    }
}
