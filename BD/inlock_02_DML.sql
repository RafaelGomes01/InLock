USE inlock_games_manha
GO

-- Tipo Usuario
INSERT INTO TiposUsuarios (titulo)
VALUES
	('Administrador'),
	('Cliente');
GO

-- Estudios
INSERT INTO Estudio (nomeEstudio)
VALUES
	('Blizzard'),
	('Rockstar Studios'),
	('Square Enix');
GO

-- Usuarios
INSERT INTO Usuarios (email, senha, idTipoUsuario)
VALUES
	('admin@admin.com', 'admin', 1),
	('cliente@cliente.com', 'cliente', 2);
GO

-- Jogos
INSERT INTO Jogos (nomeJogo, descricao, dataLancamento, valor, idEstudio)
VALUES
	('Diablo 3', '� um jogo que cont�m bastante a��o e � viciante, seja voc� um novato ou um f�.', '15/05/2012', 99.00, 1),
	('Red Dead Redemption II', 'Jogo eletr�nico de a��o-aventura western', '26/10/2018', 120.00, 2 );
GO