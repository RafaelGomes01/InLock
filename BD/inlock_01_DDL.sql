-- Criar Banco
CREATE DATABASE inlock_games_manha
GO

-- Usar Banco
USE inlock_games_manha
GO

-- Tabela Estudios
CREATE TABLE Estudios (
	idEstudio INT PRIMARY KEY IDENTITY,
	nomeEstudio VARCHAR(200) NOT NULL,
);
GO

-- Tabela Jogos
CREATE TABLE Jogos (
	idJogo INT PRIMARY KEY IDENTITY,
	nomeJogo VARCHAR(200) NOT NULL,
	descricao TEXT NOT NULL,
	dataLancamento DATE NOT NULL,
	valor SMALLMONEY NOT NULL,
	idEstudio INT FOREIGN KEY REFERENCES Estudios (idEstudio),
);
GO

-- Tabela Tipo Usuario
CREATE TABLE TiposUsuarios (
	idTipoUsuario INT PRIMARY KEY IDENTITY,
	titulo VARCHAR (200) NOT NULL,
);
GO

-- Tabela Usuario
CREATE TABLE Usuarios (
	idUsuario INT PRIMARY KEY IDENTITY,
	email VARCHAR(200) NOT NULL,
	senha VARCHAR(200) NOT NULL,
	idTipoUsuario INT FOREIGN KEY REFERENCES TiposUsuarios (idTipoUsuario),
);
GO
