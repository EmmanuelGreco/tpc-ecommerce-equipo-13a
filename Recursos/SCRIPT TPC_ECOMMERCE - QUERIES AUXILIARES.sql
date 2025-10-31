USE master
GO

USE TPC_ECOMMERCE
GO

alter table productos 
ADD CONSTRAINT df_estado_producto DEFAULT 1 FOR Estado;

ALTER TABLE Productos
ALTER COLUMN Estado bit NOT NULL;



/*SELECT P.Id, P.Codigo, P.Nombre, P.Descripcion, P.IdMarca, M.Nombre Marca, 
P.IdCategoria, C.Nombre Categoria, P.Origen, Precio
FROM PRODUCTOS P, MARCAS M, CATEGORIAS C
WHERE M.Id = P.IdMarca AND C.Id = P.IdCategoria*/


--SELECT * FROM PRODUCTOS
--SELECT * FROM MARCAS
--SELECT * FROM CATEGORIAS