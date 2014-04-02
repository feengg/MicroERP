USE MircoERP;

CREATE TABLE Rechnungen
(
	ID_Rechnungen INT IDENTITY NOT NULL PRIMARY KEY,
	FK_Kontakte INT,
	Datum date, 
	Faelligkeit date,
	Rechnungsnummer numeric(38,0),
	Kommentar varchar(100),
	Nachricht varchar(100),
	FOREIGN KEY (FK_Kontakte) REFERENCES Kontakte (ID_Kontakte)
);

CREATE TABLE Kontakte
(
	ID_Kontakte INT IDENTITY NOT NULL PRIMARY KEY,
	Adresse varchar(50),
	Rechnungsadresse varchar(50),
	Lieferadresse varchar(50)	
);

CREATE TABLE Person
(
	ID_Person INT IDENTITY NOT NULL PRIMARY KEY,
	FK_Kontakte INT,
	FK_FIRMA INT,
	Titel varchar(10),
	Vorname varchar(30),
	Nachname varchar(30),
	Suffix varchar(10),
	Geburtsdatum date,
	FOREIGN KEY (FK_Kontakte) REFERENCES Kontakte (ID_Kontakte),
	FOREIGN KEY (FK_Firma) REFERENCES Firma (ID_Firma)
	
);

CREATE TABLE Firma
(
	ID_Firma INT IDENTITY NOT NULL PRIMARY KEY,
	FK_Kontakte INT,
	Name varchar(30),
	UID varchar(30),
	FOREIGN KEY (FK_Kontakte) REFERENCES Kontakte (ID_Kontakte)
	
);

CREATE TABLE Artikel
(
	ID_Artikel INT IDENTITY NOT NULL PRIMARY KEY,
	FK_RZeile INT,
	Name varchar(30),
	Preis numeric(38,0),
	UST numeric(38,0),
	FOREIGN KEY (FK_RZeile) REFERENCES Rechnungszeile (ID_RZeile)	
);

CREATE TABLE Rechnungszeile
(
	ID_RZeile INT IDENTITY NOT NULL PRIMARY KEY,
	FK_Rechnungen INT,
	Menge numeric(38,0),
	Netto numeric(38,0),
	FOREIGN KEY (FK_Rechnungen) REFERENCES Rechnungen (ID_Rechnungen)
);