using ErrorOr;

namespace Domain.Common.Errors;

public static partial class Errors
{
    public static class RefreshToken
    {
        public static Error InvalidExpiresDate => Error.Validation(
            code: "RefreshToken.InvalidExpiresDate",
            description: "Expiration date time cannot be from the past.");

        public static Error TokenHasExpired => Error.Validation(
            code: "RefreshToken.Expired",
            description: "Refresh token has expired. You have to log in again.");
    }
}