USE MASTER
GO

USE TPC_ECOMMERCE
GO

--ALTER TABLE productos 
--ADD CONSTRAINT df_estado_producto DEFAULT 1 FOR Estado;

--ALTER TABLE Productos
--ALTER COLUMN Estado BIT NOT NULL;


/*SELECT P.Id, P.Codigo, P.Nombre, P.Descripcion, P.IdMarca, M.Nombre Marca, 
P.IdCategoria, C.Nombre Categoria, P.Origen, Precio
FROM PRODUCTOS P, MARCAS M, CATEGORIAS C
WHERE M.Id = P.IdMarca AND C.Id = P.IdCategoria*/

-- INSERT INTO PRODUCTOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Origen, Stock, Precio ) 
--                                        VALUES (1, 1, 1, 1, 1, 1, 1, 1);
--                                        SELECT SCOPE_IDENTITY() AS 'IdInsertado';

-- DELETE FROM PRODUCTOS where id >= 10

--UPDATE MARCAS SET Activo = CASE WHEN Activo = 1 THEN 0 ELSE 1 END WHERE Id = 1
--UPDATE CATEGORIAS SET Activo = CASE WHEN Activo = 1 THEN 0 ELSE 1 END WHERE Id = 1

--SELECT * FROM PRODUCTOS
--SELECT * FROM MARCAS
--SELECT * FROM CATEGORIAS
--SELECT * FROM IMAGENES
