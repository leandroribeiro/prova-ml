CREATE TABLE Produtos
(
    ID              smallint        identity       primary key,
    Nome            varchar(255)    not null,
    ValorVenda      numeric(15,2)   not null,
    Imagem          varchar(255)    not null
);