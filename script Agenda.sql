CREATE DATABASE db_Agenda;

CREATE TABLE Usuario(
id_usuario SERIAL PRIMARY KEY,
nome VARCHAR(100) NOT NULL,
apelido VARCHAR(100),
cpf VARCHAR(11) UNIQUE NOT NULL,
telefone VARCHAR(11) NOT NULL,
email VARCHAR(100) NOT NULL
);

CREATE TABLE Historico(
id_usuario INT PRIMARY KEY,
dataCadastro TIMESTAMP NOT NULL,
dataUltimaAlteracao TIMESTAMP,
FOREIGN KEY (id_usuario) REFERENCES Usuario(id_usuario) ON DELETE CASCADE
);

--VIEW
CREATE VIEW VW_UsuarioHistorico AS
SELECT 
    U.id_usuario,
    U.nome,
    U.apelido,
    U.cpf,
    U.email,
    U.telefone,
    H.dataCadastro,
    H.dataUltimaAlteracao
FROM Usuario AS U
INNER JOIN Historico AS H ON U.id_usuario = H.id_usuario
ORDER BY U.nome;

--STORE PROCEDURE INSERT USUARIO
CREATE PROCEDURE sp_inserir_usuario(
spnome VARCHAR(100),
spapelido VARCHAR(100),
spcpf VARCHAR(11),
sptelefone VARCHAR(11),
spemail VARCHAR(100)
)
LANGUAGE plpgsql
AS $$
BEGIN
    INSERT INTO Usuario (nome, apelido, cpf, telefone, email) 
    VALUES (spnome, spapelido, spcpf, sptelefone, spemail);
END $$

--FUNCTION INSERT HISTORICO
CREATE FUNCTION inserir_historico()
RETURNS TRIGGER AS $$
BEGIN
    INSERT INTO Historico (id_usuario, dataCadastro)
    VALUES (NEW.id_usuario, CURRENT_TIMESTAMP);
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

--FUNCTION ATUALIZAR HISTORICO
CREATE FUNCTION atualizar_historico()
RETURNS TRIGGER AS $$
BEGIN
    UPDATE Historico 
    SET dataUltimaAlteracao = CURRENT_TIMESTAMP
    WHERE id_usuario = NEW.id_usuario;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

--TRIGGER INSERIR USUARIO
CREATE TRIGGER trg_inserir_historico
AFTER INSERT ON Usuario
FOR EACH ROW
EXECUTE FUNCTION inserir_historico();

--TRIGGER ATUALIZAR USUARIO
CREATE TRIGGER trg_atualizar_historico
AFTER UPDATE ON Usuario
FOR EACH ROW
EXECUTE FUNCTION atualizar_historico();