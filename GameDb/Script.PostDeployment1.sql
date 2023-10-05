/*
Modèle de script de post-déploiement							
--------------------------------------------------------------------------------------
 Ce fichier contient des instructions SQL qui seront ajoutées au script de compilation.		
 Utilisez la syntaxe SQLCMD pour inclure un fichier dans le script de post-déploiement.			
 Exemple :      :r .\monfichier.sql								
 Utilisez la syntaxe SQLCMD pour référencer une variable dans le script de post-déploiement.		
 Exemple :      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
INSERT INTO Genre VALUES ('Action'), ('RPG'), ('FPS'), ('Meuporg')

INSERT INTO Game (Title, Description, IdGenre) VALUES 
('Rocket league', 'Best jeu de foot ever', 1),
('Baldur''s Gate 3', 'Anne PC Killer', 2),
('CS:GO', 'Pour ceux qui aime le pan pan', 3),
('World of Warcraft', 'Best perte de temps ever', 4)

EXEC UserRegister 'admin@mail.com', 'Test1234!', 'Arthur'
EXEC UserRegister 'user@mail.com', 'Test1234!', 'Merlin'

UPDATE Users SET RoleId = 3 WHERE Id = 1

INSERT INTO Favoris VALUES (1, 1), (2, 1), (2, 2), (4,1)