namespace FKIntegration
{
    internal class FKPUser
    {
        public FKPUser(string userName, string password)
        {
            _userName = userName;
            _password = password;
        }

        private readonly string _userName;
        public string UserName
        {
            get { return _userName; }
        }

        private readonly string _password;
        public string Password
        { get { return _password; } }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(_userName) && (_userName.Length <= 40) && (string.IsNullOrEmpty(_password) || _password.Length <= 8);
        }

        public string GetValidationMessage()
        {
            if (string.IsNullOrEmpty(_userName) || _userName.Length > 40)
                return string.Format("Nazwa uzytkownika powinna miec od 1 do 40 znakow, ma {0}", string.IsNullOrEmpty(_userName) ? 0 : _userName.Length);
            if (!string.IsNullOrEmpty(_password) && _password.Length > 8)
                return string.Format("Haslo nie moze miec wiecej niz 8 znakow, ma {0}", _password.Length);
            return string.Empty;
        }
    }
}