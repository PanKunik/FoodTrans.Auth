using ErrorOr;

namespace Domain.Common.Errors;

public static partial class Errors
{
    public static class Auth
    {
        public static Error InvalidCredentials => Error.Conflict(
            code: "User.InvalidCred",
            description: "Wrong email/username and/or password.");

        public static Error EmptyEmail => Error.Validation(
            code: "User.EmptyEmail",
            description: "Email cannot be null or empty.");

        public static Error InvalidEmailLength => Error.Validation(
            code: "User.InvalidEmailLength",
            description: "Email maximum length is 100 characters.");

        public static Error InvalidEmail => Error.Validation(
            code: "User.InvalidEmailFormat",
            description: "Email has incorrect format.");

        public static Error EmptyUserName => Error.Validation(
            code: "User.EmptyUserName",
            description: "User name cannot be null or empty.");

        public static Error InvalidUsernameLength => Error.Validation(
            code: "User.InvalidUserNameLength",
            description: "User name must have between 5 and 20 characters.");

        public static Error InvalidUsername => Error.Validation(
            code: "User.InvalidUserName",
            description: "User name must contain only digits and letters");

        public static Error EmptyFirstName => Error.Validation(
            code: "User.EmptyFirstName",
            description: "First name cannot be null or empty.");

        public static Error InvalidFirstNameLength => Error.Validation(
            code: "User.InvalidFirstNameLength",
            description: "First name must have between 3 and 30 characters.");

        public static Error EmptyLastName => Error.Validation(
            code: "User.EmptyLastName",
            description: "Last name cannot be null or empty.");

        public static Error InvalidLastNameLength => Error.Validation(
            code: "User.InvalidLastNameLength",
            description: "Last name must have between 3 and 30 characters.");

        public static Error EmptyPassword => Error.Validation(
            code: "User.EmptyPassword",
            description: "Password cannot be null or empty.");

        public static Error InvalidPasswordLength => Error.Validation(
            code: "User.InvalidPasswordLength",
            description: "Password must have between 8 and 20 characters.");

        public static Error PasswordWithoutSpecialCharacter => Error.Validation(
            code: "User.PasswordWithoutSpecialCharacter",
            description: "Password must contain at least 1 special character.");

        public static Error PasswordWithoutDigit => Error.Validation(
            code: "User.PasswordWithoutDigit",
            description: "Password must contain at least 1 digit.");

        public static Error PasswordWithoutUpperCaseLetter => Error.Validation(
            code: "User.PasswordWithoutUpperCaseLetter",
            description: "Password must contain at least 1 upper case letter.");

        public static Error AlreadyLogedIn => Error.Conflict(
            code: "User.AlreadyLogedIn",
            description: "You are already loged in.");

        public static Error AlreadyLogedOut => Error.Conflict(
            code: "User.AlreadyLogedOut",
            description: "You are already loged out.");
    }
}