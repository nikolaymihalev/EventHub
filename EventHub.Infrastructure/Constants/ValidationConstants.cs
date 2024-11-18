namespace EventHub.Infrastructure.Constants
{
    public static class ValidationConstants
    {
        // User
        public const int UserUsernameMinLength = 3;
        public const int UserUsernameMaxLength = 20;
        public const int UserEmailMinLength = 5;
        public const int UserEmailMaxLength = 50;
        public const int UserNameMinLength = 2;
        public const int UserNameMaxLength = 50;

        // Event
        public const int EventTitleMinLength = 5;
        public const int EventTitleMaxLength = 100;
        public const int EventDescriptionMinLength = 10;
        public const int EventDescriptionMaxLength = 1000;

        // Comment
        public const int CommentContentMinLength = 5;
        public const int CommentContentMaxLength = 500;

        // Category
        public const int CategoryNameMinLength = 3;
        public const int CategoryNameMaxLength = 30;
    }
}
