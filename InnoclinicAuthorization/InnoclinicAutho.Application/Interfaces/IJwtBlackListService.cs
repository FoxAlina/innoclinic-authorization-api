using InnoclinicAutho.Application.UseCases.Cache;

namespace InnoclinicAutho.Application.Interfaces;

public interface IJwtBlackListService
{
    Task AddToBlacklistAsync(CachedUser cachedUser, string token, CancellationToken cancellationToken);
    Task<bool> IsBlacklistedAsync(string token, CancellationToken cancellationToken = default);
}
