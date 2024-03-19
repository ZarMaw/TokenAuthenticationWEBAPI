using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace TokenAuthenticationWEBAPI.Models
{
    public class AuthenticationRepository : IDisposable
    {
        SECURITY_DBEntities context = new SECURITY_DBEntities();
        public async Task<bool> AddRefreshToken(RefreshToken token)
        {
            var existingToken = context.RefreshTokens.FirstOrDefault(r => r.UserName == token.UserName
                            && r.ClientID == token.ClientID);
            if (existingToken != null)
            {
                var result = await RemoveRefreshToken(existingToken);
            }
            context.RefreshTokens.Add(token);
            return await context.SaveChangesAsync() > 0;
        }
        public async Task<bool> RemoveRefreshTokenByID(string refreshTokenId)
        {
            var refreshToken = await context.RefreshTokens.FindAsync(refreshTokenId);
            if (refreshToken != null)
            {
                context.RefreshTokens.Remove(refreshToken);
                return await context.SaveChangesAsync() > 0;
            }
            return false;
        }
        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            context.RefreshTokens.Remove(refreshToken);
            return await context.SaveChangesAsync() > 0;
        }
        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = await context.RefreshTokens.FindAsync(refreshTokenId);
            return refreshToken;
        }
        public List<RefreshToken> GetAllRefreshTokens()
        {
            return context.RefreshTokens.ToList();
        }
        public void Dispose()
        {
            context.Dispose();
        }
    }
}