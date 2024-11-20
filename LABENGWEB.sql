use master

create database Database_LABENGWEB
use Database_LABENGWEB

--TABELA UTILIZADOR
CREATE TABLE Utilizador(
	ID				integer			not null	IDENTITY(1,1)	primary key,
	Nome			varchar(50)		not null,
	Email			varchar(150)	not null,
	Username		varchar(50)		not null,
	Pass			varchar(150)	not null,
)

--TABELA LEITOR
CREATE TABLE Leitor(
	ID_Leitor		integer			not null	primary key,
	Nome			varchar(50)		not null,
	Email			varchar(150)	not null,
	Username		varchar(50)		not null,
	Pass			varchar(150)	not null,
	Morada			varchar(150)	not null,
	Contacto		integer			not null,
	CHECK (Contacto LIKE '[9][1-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
	FOREIGN KEY (ID_Leitor) REFERENCES Utilizador(ID),
)

--TABELA Bibliotecário
CREATE TABLE Bibliotecario(
	ID_Bibliotecario		integer			not null	primary key,
	Nome			varchar(50)		not null,
	Email			varchar(150)	not null,
	Username		varchar(50)		not null,
	Pass			varchar(150)	not null,
	Contacto		integer			not null,
	CHECK (Contacto LIKE '[9][1-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
	FOREIGN KEY (ID_Bibliotecario) REFERENCES Utilizador(ID),
)

--TABELA Administrador
CREATE TABLE Administrador(
	ID_Admin		integer			not null	primary key,
	Nome			varchar(50)		not null,
	Email			varchar(150)	not null,
	Username		varchar(50)		not null,
	Pass			varchar(150)	not null,
	FOREIGN KEY (ID_Admin) REFERENCES Utilizador(ID),
)

--TABELA Livro
CREATE TABLE Livro(
	ISBN					integer			not null	IDENTITY(1,1)	primary key,
	Titulo					varchar(50)		not null,
	Ano_Publicacao			integer			not null,
	Numero_Exemplares		integer			not null,
	Descricao				varchar(300)	not null,
)

--TABELA Categoria
CREATE TABLE Categoria(
	ID_Categoria			integer			not null	primary key,
	Nome					varchar(50)		not null,
	Descricao				varchar(50)		not null,
)

--TABELA Produtora
CREATE TABLE Produtora(
	ID_Produtora			integer			not null	primary key,
	Nome					varchar(50)		not null,
	Descricao				varchar(50)		not null,
)

--TABELA Autor
CREATE TABLE Autor(
	ID_Autor			integer			not null	primary key,
	Nome				varchar(50)		not null,
	Descricao			varchar(50)		not null,
)

--TABELA Gerir
CREATE TABLE Gerir(
	Data_Gerir			DATE			NOT NULL,
	ID_Livro			INT				NOT NULL,
	ID_Bibliotecario	INT				NOT NULL,
	Tipo_Gerenciamento	VARCHAR(20)		NOT NULL,
	FOREIGN KEY (ID_Livro) REFERENCES Livro(ISBN),
	FOREIGN KEY (ID_Bibliotecario) REFERENCES Bibliotecario(ID_Bibliotecario),
	PRIMARY KEY	(Data_Gerir, ID_Livro, ID_Bibliotecario)
)

--TABELA Requisitar
CREATE TABLE Requisitar(
	Data_Requisicao		DATE			NOT NULL,
	ID_Leitor			INT				NOT NULL,
	ID_Livro			INT				NOT NULL,
	N_Disponivel		INT				NOT NULL,
	Data_Entrega		DATE,
	Prazo_Entrega		DATE,
	FOREIGN KEY (ID_Leitor) REFERENCES Leitor(ID_Leitor),
	FOREIGN KEY (ID_Livro) REFERENCES Livro(ISBN),
	PRIMARY KEY (Data_Requisicao, ID_Leitor, ID_Livro)
)

--TABELA Gerir_Utilizadores
CREATE TABLE Gerir_Utilizadores(
	Data_Mudanca		DATE			NOT NULL,
	ID_Admin			INT				NOT NULL,
	ID_Utilizador		INT				NOT NULL,
	FOREIGN KEY (ID_Admin) REFERENCES Administrador(ID_Admin)
)

--TABELA Pertence_Categoria
CREATE TABLE Pertence_Categoria(
	ID_Liv		INT	NOT NULL,
	ID_Categoria	INT NOT NULL,
	FOREIGN KEY (ID_Liv) REFERENCES Livro(ISBN),
	FOREIGN KEY (ID_Categoria) REFERENCES Categoria(ID_Categoria),
	PRIMARY KEY ( ID_Liv, ID_Categoria)
	)

--TABELA Pertence_Produtora
CREATE TABLE Publicado_Produtora(
	ID_Liv		INT	NOT NULL,
	ID_Produtora	INT NOT NULL,
	FOREIGN KEY (ID_Liv) REFERENCES Livro(ISBN),
	FOREIGN KEY (ID_Produtora) REFERENCES Produtora(ID_Produtora),
	PRIMARY KEY ( ID_Liv, ID_Produtora)
	)

--TABELA Tem_Autor
CREATE TABLE Tem_Autor(
	ID_Liv		INT	NOT NULL,
	ID_Autor		INT NOT NULL,
	FOREIGN KEY (ID_Liv) REFERENCES Livro(ISBN),
	FOREIGN KEY (ID_Autor) REFERENCES Autor(ID_Autor),
	PRIMARY KEY ( ID_Liv, ID_Autor)
	)



