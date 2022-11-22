using ErrorOr;

namespace Domain.Common.Errors;

public static partial class Errors
{
    public static class RefreshToken
    {
        public static Error InvalidExpiresDate => Error.Validation(
            code: "RefreshToken.InvalidExpiresDate",
            description: "Expiration date time cannot be from the past.");
    }
}