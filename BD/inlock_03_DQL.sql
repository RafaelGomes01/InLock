USE inlock_games_manha
GO

-- Listar todos os Usuarios
SELECT * FROM Usuarios

-- Listar todos os Estudios
SELECT * FROM Estudio

-- Listar todos os Jogos
SELECT * FROM Jogos

-- Listar todos os jogos e seus estudios
SELECT idjogo, nomeJogo, descricao, dataLancamento, valor, Estudio.nomeEstudio
FROM Jogos
INNER JOIN Estudio
ON Jogos.idEstudio = Estudio.idEstudio;

-- Listar todos os estudios e seus respectivos jogos
SELECT nomeEstudio, Jogos.nomeJogo
FROM Estudio
FULL OUTER JOIN Jogos
ON Estudio.IdEstudio = Jogos.idEstudio;

-- Buscar um usuario por email e senha
SELECT * FROM Usuarios
WHERE 
	email = 'admin@admin.com' AND senha = 'admin';

-- Buscar um jogo pelo idJogo
SELECT * FROM Jogos
WHERE
	idJogo = 2;

-- Buscar um estudio pelo idEstudio
SELECT * FROM Estudio
WHERE
	idEstudio = 3;
