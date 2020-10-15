using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace greenSacrifice.Blazor.Repo.Context
{
    public abstract class AppDbContext : DbContext
    {
        protected AppDbContext(AzureADOptions azureAdOptions, DbContextOptions options) : base(options)
        {
            SetSqlConnectionToken(azureAdOptions);
        }

        protected AppDbContext(AzureADOptions azureAdOptions, DbContextOptions<AppDbContext> options) : base(options)
        {
            SetSqlConnectionToken(azureAdOptions);
        }

        private void SetSqlConnectionToken(AzureADOptions azureAdOptions)
        {
            var conn = (SqlConnection)Database.GetDbConnection();

            var authenticationContext = new AuthenticationContext($"{azureAdOptions.Instance.TrimEnd('/')}/{azureAdOptions.TenantId}");

            var clientCredential = new ClientCredential(azureAdOptions.ClientId, azureAdOptions.ClientSecret);

            var authenticationResult = authenticationContext.AcquireTokenAsync("https://database.windows.net", clientCredential).Result;
            conn.AccessToken = authenticationResult.AccessToken;
        }
    }
}
