﻿using MySql.Data.MySqlClient;

namespace GestionLocation
{
    static class Global
    {
        // Connexion
        private static MySqlConnection _connexion;
        private static string emailUser;
        private static string pwdEmailUser;
        private static int port;
        private static string serveurSmtp;
        private static string user;
        public static int HeightMaxSignature = 420;
        // Token de connexion à l'API de l'INSEE (pour récupérer l'IRL)
        public static string bearerToken = "f6960065-2fab-3db3-a88a-908fd5d75461";


        /// <summary>
        /// Met à jour ou retourne la connexion
        /// </summary>
        public static MySqlConnection Connexion
        {
            set { _connexion = value; }
            get { return _connexion; }
        }

        /// <summary>
        /// Met une majuscule sur la première lettre d'un mot ou d'une phrase
        /// </summary>
        /// <param name="s">Chaîne concernée par la mise en forme</param>
        /// <returns>Chaine de caractère avec une majuscule sur le premier caractère</returns>
        public static string Capitalize(string s)
        {
            return s[0].ToString().ToUpper() + s.Substring(1).ToLower();
        }


        /// <summary>
        /// Getter et setter sur l'adresse mail de l'utilisateur
        /// </summary>
        public static string EmailUser
        {
            set { emailUser = value; }
            get { return emailUser;  }
        }


        /// <summary>
        /// Getter et setter sur le mot de passe du compte email de l'utilisateur
        /// </summary>
        public static string PwdUser
        {
            set { pwdEmailUser = value; }
            get { return pwdEmailUser; }
        }

        /// <summary>
        /// Getter et setter sur le numéro de port de l'email de l'utilisateur
        /// </summary>
        public static int PortEmail
        {
            set { port = value; }
            get { return port; }
        }


        /// <summary>
        /// Getter et setter sur l'adresse du serveur SMTP de l'email de l'utilisateur
        /// </summary>
        public static string ServeurSmtp
        {
            set { serveurSmtp = value; }
            get { return serveurSmtp; }
        }


        /// <summary>
        /// Getter et setter sur l'utilisateur
        /// </summary>
        public static string User
        {
            set { user = value; }
            get { return user; }
        }
    }
}
