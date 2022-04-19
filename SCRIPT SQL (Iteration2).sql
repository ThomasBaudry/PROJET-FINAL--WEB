/* SCRIPT DE Création des bases de données (Iteration2) */
/* TABLE des Dépenses */
CREATE TABLE dbo.T_Depenses (
    idDepense int IDENTITY(1,1) NOT NULL,
    dateTemps datetime NOT NULL,
    Montant money,
    idGarderie int,
	idCategorieDepense int,
	idCommerce int,
    CONSTRAINT PK_idDepense PRIMARY KEY (idDepense),
	CONSTRAINT FK_idGarderie FOREIGN KEY (idGarderie) REFERENCES T_Garderies (idGarderie),
	CONSTRAINT FK_idCategorieDepense FOREIGN KEY (idCategorieDepense) REFERENCES T_CategorieDepense (idCategorieDepense),
	CONSTRAINT FK_idCommerce FOREIGN KEY (idCommerce) REFERENCES T_Commerce (idCommerce)
);

/* TABLE des Catégories de dépenses */
CREATE TABLE dbo.T_CategorieDepense (
    idCategorieDepense int IDENTITY(1,1) NOT NULL,
    description varchar(100) NOT NULL,
    pourcentage float,
    CONSTRAINT PK_idCategorieDepense PRIMARY KEY (idCategorieDepense),
);

/* TABLE des Commerces */
CREATE TABLE dbo.T_Commerce (
    idCommerce int IDENTITY(1,1) NOT NULL,
	description varchar(50) NOT NULL,
	adresse varchar(200) NOT NULL,
	telephone varchar(12) NOT NULL,
    CONSTRAINT PK_idCommerce PRIMARY KEY (idCommerce),
);