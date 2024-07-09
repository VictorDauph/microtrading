tuto: https://www.youtube.com/watch?v=FGsXzIgFgJg&ab_channel=NehanthWorld

Pour configurer la synchro � la BDD, dans la console du package manager (nuget)

------------
Scaffold-DbContext "Data Source=MACBOOLPORTABLE;Initial Catalog=micro_trading;Integrated Security=True;Trust Server Certificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
----------------

La string entre "" est la connection string propre � la database que l'on veut utiliser. On la retrouve dans le fichier DotnetEfcoreDbfirstTutorialContext g�n�r� automatiquement dans le dossier models
Cette commande va g�n�rer des mod�les � partir des tables pr�sentes dans la base de donn�es.

Supprimer totalement la fonction OnConfiguring du context de la BDD quand le scaffolding est lanc�. La connection � la BDD est g�r�e par Program.ce

Pour repliqer la structure de la database

1) cr�er une migration � partir des mod�les scaffold�s (et �ventuellement modifi�s dans leurs fichiers .cs)
------------------
add-migration [migration-name]
---------------------

2) importer la nouvelle structure dans la BDD
--------------------------
update-database
-------------------------


