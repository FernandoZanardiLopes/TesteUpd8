Create database upd8	

use upd8 

create table Cliente (
	 CPF varchar(11) NOT NULL Primary key,
	 Nome varchar(366),
	 DTNascimento varchar(10),
	 Sexo Varchar(1),
	 Endereco varchar(500),
	 Estado Varchar(3),
	 Cidade varchar(100)
)

