namespace CustomExceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string message) : base(message) { }

        public static UserNotFoundException ForUserName(string username) =>
            new($"User with username '{username}' was not found.");

        public static UserNotFoundException ForUserId(Guid id) =>
            new($"User with ID '{id}' was not found.");
    }

    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException(string message) : base(message) { }
    }

    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException()
            : base("Invalid username or password.") { }

        public InvalidCredentialsException(string message)
            : base(message) { }

        public InvalidCredentialsException(string message, Exception innerException)
            : base(message, innerException) { }
    }
    

    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
    public class AdminDeletionException : Exception
    {
        public AdminDeletionException(string message) : base(message) { }

        public static AdminDeletionException ForAdminUser(string username)
        {
            return new AdminDeletionException($"Cannot delete the user '{username}' because they are an administrator.");
        }
    }


}
