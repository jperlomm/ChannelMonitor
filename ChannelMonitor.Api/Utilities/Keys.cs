using Microsoft.IdentityModel.Tokens;

namespace ChannelMonitor.Api.Utilities
{
    public static class Keys
    {
        public const string IssuerLocal = "jwt-server";
        public const string SectionKeys = "Authentication:Schemes:Bearer:SigningKeys";
        public const string SectionKeys_Emitter = "Issuer";
        public const string SectionKeys_Value = "Value";

        // Para emisores propios
        public static IEnumerable<SecurityKey> GetKey(IConfiguration configuration) => GetKey(configuration, IssuerLocal);

        // Para otros emisores ej Facebook.
        public static IEnumerable<SecurityKey> GetKey(IConfiguration configuration, string issuer)
        {
            var signingKey = configuration.GetSection(SectionKeys).GetChildren().SingleOrDefault(key => key[SectionKeys_Emitter] == issuer);

            if (signingKey is not null && signingKey[SectionKeys_Value] is string keyValue) {

                yield return new SymmetricSecurityKey(Convert.FromBase64String(keyValue));
            }

        }

        public static IEnumerable<SecurityKey> GetAllKeys(IConfiguration configuration)
        {
            var signingKeys = configuration.GetSection(SectionKeys).GetChildren();

            foreach (var signingKey in signingKeys) {

                if (signingKey[SectionKeys_Value] is string keyValue)
                {
                    yield return new SymmetricSecurityKey(Convert.FromBase64String(keyValue));
                }
            }

        }

    }
}
