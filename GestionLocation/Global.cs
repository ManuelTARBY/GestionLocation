using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Util;
using Google.Apis.Util.Store;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Security;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
