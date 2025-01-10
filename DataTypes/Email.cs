namespace OrnaLibs.DataTypes
{
    public readonly partial struct Email
    {
        public class EmailFormatException(string input) : FormatException($"{input}") { }

        private string User { get; }
        private string Domain { get; }

        private Email(string user, params string[] domain)
        {
            User = user;
            Domain = string.Join('.', domain);
        }

        public static implicit operator Email(string email)
        {
            var regex = RegExp.Email().Match(email);
            if (!regex.Success)
                throw new EmailFormatException(email);
            return new Email(
                regex.Groups["user"].Value, 
                regex.Groups["domain"].Value.Split('.')
                );
        }
        public static implicit operator string(Email email) =>
            $"{email.User}@{email.Domain}";
    }
}
