namespace GestionLocation.DTO
{
    public class UtilisateurDTO
    {
        public int IdUser { get; set; }
        public string Login { get; set; }
        public string Pwd { get; set; }          // hash BCrypt
        public string Prenom { get; set; }
        public string Nom { get; set; }
        public string Adresse { get; set; }
        public string CodePostal { get; set; }
        public string Ville { get; set; }
        public string Email { get; set; }
        public string PwdEmail { get; set; }
        public string ServeurSmtp { get; set; }
        public int Port { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string CheminSignature { get; set; }
    }
}
