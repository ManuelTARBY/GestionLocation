# GestionLocation
Application permettant de gérer ses locations (biens, locataires, cautions, locations, paiements)

######## Mise en oeuvre de l'application ########

1. Créer la base de données avec la commande "CREATE DATABE IF NOT EXISTS nomdelabasededonnees"

2. Créer la structure de votre base de données
	--> Le fichier ReqCreationTables.txt contient la requête SQL pour générer les tables nécessaires

3. Créer la chaîne de connexion
	--> Dans le fichier Connexion.cs (méthode GenererChaineConnexion()), remplacer "adressebdd" par l'adresse serveur de la bdd

4. Lancer l'application pour créer vos premiers enregistrements !
