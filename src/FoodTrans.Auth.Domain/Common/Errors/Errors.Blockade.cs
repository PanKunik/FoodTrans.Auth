using ErrorOr;

namespace Domain.Common.Errors;

public static partial class Errors
{
    public static class Blockade
    {
        public static Error BlockadeReasonEmpty => Error.Validation(
            code: "Blockade.EmptyReason",
            description: "Blockade reason cannot be null or empty."
        );
    }
}