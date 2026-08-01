# GestionLocation

Application bureau (Windows Forms, C#) pour la gestion de locations immobilières : biens, locataires, cautions, contrats de location, paiements de loyers, charges, quittances et statistiques.

> Ce README a été rédigé à partir du code vu et modifié au fil du développement. Certaines sections (fenêtres non encore explorées, procédure d'installation complète) sont à compléter au fur et à mesure.

## Sommaire

- [Fonctionnalités](#fonctionnalités)
- [Stack technique](#stack-technique)
- [Prérequis](#prérequis)
- [Installation](#installation)
- [Configuration](#configuration)
- [Authentification](#authentification)
- [Migration des mots de passe (installations existantes)](#migration-des-mots-de-passe-installations-existantes)
- [Structure du projet](#structure-du-projet)
- [Sécurité](#sécurité)
- [Limitations connues](#limitations-connues)

## Fonctionnalités

- Connexion utilisateur (multi-utilisateurs)
- Gestion des biens immobiliers
- Gestion des locataires
- Gestion des cautions
- Gestion des locations (association bien / locataire / caution, dates, filtrage multi-critères)
- Suivi des paiements de loyers
- Gestion des charges
- Génération de contrats de location et de quittances (PDF, avec signature)
- Groupes de biens
- Statistiques
- Envoi d'emails (quittances, contrats...) via le compte SMTP propre à chaque utilisateur

## Stack technique

- **UI** : Windows Forms (.NET / C#)
- **Base de données** : MySQL 8.0 (`MySql.Data.MySqlClient`)
- **Mots de passe** : hashés avec [BCrypt.Net-Next](https://www.nuget.org/packages/BCrypt.Net-Next)
- **Images** : System.Drawing (redimensionnement des signatures)

## Prérequis

- Windows
- .NET Framework / .NET (selon la cible du projet — à préciser)
- MySQL Server 8.0
- Packages NuGet :
  - `MySql.Data`
  - `BCrypt.Net-Next`

## Installation

1. Créer la base de données MySQL `gestionlocation` et importer le schéma (tables `utilisateur`, `bien`, `locataire`, `caution`, `location`, etc.).
2. Créer un compte MySQL de service dédié à l'application (droits limités : SELECT/INSERT/UPDATE/DELETE sur `gestionlocation`, pas de droits admin).
3. Renseigner ce compte dans la chaîne de connexion technique (`ChaineConnexionTechnique` dans `Connexion.cs`) — à terme, à sortir du code vers un fichier de configuration.
4. Restaurer les packages NuGet et compiler la solution.
5. Au premier lancement, les dossiers `Quittances` et `Signature` sont créés automatiquement à côté de l'exécutable (voir `Connexion.CheckDir()`).
6. Se connecter avec un login existant, ou en créer un nouveau depuis l'écran de connexion.

## Configuration

Chaque utilisateur applicatif configure, depuis l'écran **Ajout/Modification d'un utilisateur** :

- Ses informations personnelles (nom, prénom, adresse)
- Son adresse email et le mot de passe associé, utilisés pour l'envoi de quittances/contrats par email
- Le serveur et le port SMTP sont déduits automatiquement du domaine de l'adresse email saisie (Gmail, Orange, Free, SFR, Outlook/Hotmail/Live, Yahoo, IONOS, La Poste, AOL, Alice ADSL — sinon l'adresse est rejetée)
- Une image de signature (PNG), redimensionnée et enregistrée dans le dossier `Signature/`

## Authentification

L'authentification repose sur deux couches distinctes :

1. **Connexion technique à la BDD** : un compte de service unique, indépendant des utilisateurs de l'application.
2. **Authentification applicative** : vérification du couple login / mot de passe contre la table `utilisateur`, mot de passe hashé avec BCrypt (jamais stocké ni comparé en clair).

Si le login saisi n'existe pas, l'application propose la création d'un nouvel utilisateur.

## Migration des mots de passe (installations existantes)

Les installations antérieures au passage à BCrypt stockaient les mots de passe en clair. Un script de migration one-shot (projet console séparé) est disponible pour hasher les mots de passe existants sans casser les comptes déjà créés :

- Sauvegarder la base avant exécution (`mysqldump gestionlocation > backup.sql`)
- Le script est idempotent : les mots de passe déjà hashés (préfixe `$2a$`/`$2b$`/`$2y$`) sont ignorés, il peut donc être relancé sans risque
- À exécuter une seule fois, puis à retirer du projet

## Structure du projet

```
GestionLocation/
├── Connexion.cs                  # Authentification (connexion technique + applicative)
├── Accueil.cs                    # Fenêtre principale après connexion
├── AjoutModifUtilisateurs.cs     # Création / modification d'un utilisateur
├── UtilisateurDTO.cs             # DTO représentant un utilisateur
├── Locations.cs                  # Gestion des locations
├── Locataires.cs                 # Gestion des locataires
├── Biens.cs                      # Gestion des biens
├── Cautions.cs                   # Gestion des cautions
├── ListeCharges.cs                # Gestion des charges
├── Paiements.cs                  # Suivi des paiements
├── GroupesDeBiens.cs             # Gestion des groupes de biens
├── Stats.cs                      # Statistiques
├── Global.cs                     # État partagé (connexion BDD, session utilisateur)
├── Quittances/                   # Quittances générées (créé au premier lancement)
└── Signature/                    # Signatures des utilisateurs (créé au premier lancement)
```

> Structure indicative basée sur les fenêtres identifiées jusqu'ici — à compléter si d'autres fichiers existent dans le projet.

## Sécurité

- Mots de passe applicatifs hashés avec BCrypt
- Requêtes SQL paramétrées (protection contre l'injection SQL)
- Connexion technique à la BDD séparée des identifiants applicatifs
- Points de vigilance restants :
  - Le mot de passe du compte email (`Global.PwdUser`) reste en mémoire en clair pendant la session — acceptable en usage desktop mono-utilisateur, à surveiller si l'usage évolue
  - La chaîne de connexion technique est actuellement en dur dans le code — à externaliser (fichier de config, variable d'environnement) avant tout partage du code source

## Limitations connues

- Pas de fonctionnalité de réinitialisation de mot de passe applicatif si oublié (conséquence normale du hashage)
- L'écran Ajout/Modification d'utilisateur ne permet pas de modifier `clientid`/`clientsecret` (probablement liés à une authentification OAuth pour l'envoi d'email) — ces champs sont préservés tels quels lors des modifications
- Pas de tests automatisés identifiés à ce jour
