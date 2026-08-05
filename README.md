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
- [État d'avancement du nettoyage du code](#état-davancement-du-nettoyage-du-code)
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

## État d'avancement du nettoyage du code

Le code a été repris progressivement, fenêtre par fenêtre, pour corriger des problèmes de sécurité et de robustesse récurrents dans la version d'origine. Fenêtres déjà passées en revue et corrigées :

| Fenêtre | Principaux correctifs |
|---|---|
| `Connexion.cs` | Requêtes paramétrées, séparation connexion technique / authentification, mots de passe hashés (BCrypt) |
| `Accueil.cs` | Colonnes nommées, `using`, correction du chemin de fichier signature |
| `AjoutModifUtilisateurs.cs` | Passage au DTO `UtilisateurDTO`, requêtes paramétrées, préservation de `clientid`/`clientsecret`, correctif GDI+ sur le redimensionnement de la signature |
| `Biens.cs` | Requêtes paramétrées, suppression d'un `try/catch` utilisé comme contrôle de flux |
| `AjoutModifBiens.cs` | Requêtes 100% paramétrées, gestion de la table vide, validations numériques, prise en charge des champs DPE/fiscal |
| `FicheBien.cs` | Requêtes paramétrées, **6 bugs de plantage corrigés** (charges annuelles inconnues, bien sans location, incompatibilités de type lors de lectures BDD, divisions par zéro, agrégats sans `COALESCE`), taille de fenêtre adaptée aux petits écrans |
| `ListeCharges.cs` | Remplacement des dictionnaires indexés par texte affiché par une vraie structure (`LigneCharge`), correction d'un bug de reader SQL imbriqué (connexion partagée) |
| `AjoutModifChargeAnnuelle.cs` | Requêtes paramétrées, génération d'id sécurisée, suppression de code mort, unification de la gestion des montants (virgule/point) |
| `GroupesDeBiens.cs` | Requêtes paramétrées, `IFNULL` sur la génération d'id, transactions pour l'atomicité (création/suppression), navigation clavier corrigée (`SelectedIndexChanged` au lieu de `MouseClick`) |
| `AjoutModifLocations.cs` | Requêtes paramétrées, IDs sécurisés, remplacement du calcul de mensualités dépendant des paramètres régionaux de la machine par un calcul `DateTime` fiable, coquille de format de date corrigée ; passage ultérieur à `decimal`, transactions et `ListItem` typé |
| `Locations.cs` | Correctifs identiques au reste du projet (requêtes paramétrées, `using`, IDs sécurisés) |
| `AjoutModifLocataires.cs` / `Locataires.cs` | Déjà largement nettoyé ; quelques finitions mineures (cas limite sur la saisie des prénoms) |
| `Cautions.cs` / `AjoutModifCautions.cs` | Nettoyé sur le même modèle que `Locataires.cs` / `AjoutModifLocataires.cs` |
| `DateAssurance.cs` | Correctifs identiques au reste du projet |
| `ModifPaiements.cs`, `Paiements.cs`, `Stats.cs` | Nettoyés |

**Constantes rencontrées dans le code d'origine**, corrigées de façon systématique à chaque passage :
- Requêtes SQL construites par concaténation de chaînes (injection SQL) → requêtes paramétrées
- `SELECT *` avec accès aux colonnes par index numérique → colonnes nommées
- `MySqlCommand`/`MySqlDataReader` sans `using` → fuites de ressources
- Exceptions utilisées comme contrôle de flux normal (ex: détecter une valeur `NULL`) → vérifications explicites (`IsDBNull`, `reader.Read()`)
- Génération manuelle d'identifiants via `MAX(...) + 1` sans gérer le cas d'une table vide → `IFNULL(MAX(...), 0) + 1`

**Fenêtres restant à revoir** : toute fenêtre non listée ci-dessus (à confirmer au fur et à mesure — par exemple les éventuelles fenêtres de génération de quittances, si elles existent).

## Structure du projet

```
GestionLocation/
├── Connexion.cs                   # Authentification (connexion technique + applicative)
├── Accueil.cs                     # Fenêtre principale après connexion
├── AjoutModifUtilisateurs.cs      # Création / modification d'un utilisateur
├── DTO/
│   └── UtilisateurDTO.cs          # DTO représentant un utilisateur (namespace GestionLocation.DTO)
├── Biens.cs                       # Liste des biens
├── AjoutModifBiens.cs             # Création / modification d'un bien
├── FicheBien.cs                   # Fiche détaillée d'un bien ou groupe de biens (stats, graphique CF)
├── ListeCharges.cs                # Liste des charges annuelles d'un bien / groupe
├── AjoutModifChargeAnnuelle.cs    # Création / modification d'une charge annuelle
├── GroupesDeBiens.cs              # Gestion des groupes de biens
├── Locataires.cs                  # Liste des locataires
├── AjoutModifLocataires.cs        # Création / modification d'un locataire
├── Cautions.cs                    # Liste des cautions
├── AjoutModifCautions.cs          # Création / modification d'une caution
├── Locations.cs                   # Liste des locations
├── AjoutModifLocations.cs         # Création / modification d'une location (génération bail/état des lieux Word, IRL INSEE)
├── DateAssurance.cs               # Saisie des dates d'assurance (colocations)
├── Paiements.cs                   # Suivi des paiements
├── ModifPaiements.cs              # Modification d'un paiement
├── Stats.cs                       # Statistiques
├── Global.cs                      # État partagé (connexion BDD, session utilisateur)
├── Quittances/                    # Quittances générées (créé au premier lancement)
└── Signature/                     # Signatures des utilisateurs (créé au premier lancement)
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
- Éditer une charge annuelle (`AjoutModifChargeAnnuelle.cs`) initialement créée pour un **groupe** de biens ne permet de modifier que la ligne du bien affiché (le groupe n'est pas ré-éditable en tant que tel) — comportement existant, documenté dans le code
- Pas de tests automatisés identifiés à ce jour
