USE master
GO

DROP DATABASE IF EXISTS TPC_ECOMMERCE
GO

CREATE DATABASE TPC_ECOMMERCE
GO

USE TPC_ECOMMERCE
GO

CREATE TABLE [dbo].[CATEGORIAS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Activo] [bit] NOT NULL DEFAULT 1,
 CONSTRAINT [PK_CATEGORIAS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[MARCAS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Activo] [bit] NOT NULL DEFAULT 1,
 CONSTRAINT [PK_MARCAS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[PRODUCTOS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Codigo] [varchar](50) NULL,
	[Nombre] [varchar](50) NULL,
	[Descripcion] [varchar](150) NULL,
	[IdMarca] [int] NULL,
	[IdCategoria] [int] NULL,
    [Origen] [varchar](50) NULL,
	[Precio] [money] NULL,
    [Activo] [bit] NOT NULL DEFAULT 1,
 CONSTRAINT [PK_PRODUCTOS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[PRODUCTOS]  WITH CHECK ADD  CONSTRAINT [FK_PRODUCTOS_CATEGORIAS] FOREIGN KEY([IdCategoria])
REFERENCES [dbo].[CATEGORIAS] ([Id])
GO

ALTER TABLE [dbo].[PRODUCTOS] CHECK CONSTRAINT [FK_PRODUCTOS_CATEGORIAS]
GO

ALTER TABLE [dbo].[PRODUCTOS]  WITH CHECK ADD  CONSTRAINT [FK_PRODUCTOS_MARCAS] FOREIGN KEY([IdMarca])
REFERENCES [dbo].[MARCAS] ([Id])
GO

ALTER TABLE [dbo].[PRODUCTOS] CHECK CONSTRAINT [FK_PRODUCTOS_MARCAS]
GO

CREATE TABLE [dbo].[IMAGENES](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdProducto] [int] NOT NULL,
	[ImagenUrl] [varchar](1000) NOT NULL,
 CONSTRAINT [PK_IMAGENES] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[IMAGENES]  WITH CHECK ADD  CONSTRAINT [FK_IMAGENES_PRODUCTOS] FOREIGN KEY([IdProducto])
REFERENCES [dbo].[PRODUCTOS] ([Id])
GO

ALTER TABLE [dbo].[IMAGENES] CHECK CONSTRAINT [FK_IMAGENES_PRODUCTOS]
GO

CREATE TABLE [dbo].[Clientes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Documento] [varchar](50) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Apellido] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Direccion] [varchar](50) NOT NULL,
	[Ciudad] [varchar](50) NOT NULL,
	[CP] [int] NOT NULL,
 CONSTRAINT [PK_Clientes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


insert into MARCAS (Nombre) values ('Wilson'), ('Logitech'), ('Royal Kludge'), ('Lenovo'), ('Samsung'), ('Sony'), ('LG'), ('Dell'), ('Asus')
insert into CATEGORIAS (Nombre) values ('Mochilas'),('Periféricos'), ('Accesorios'), ('Televisores'), ('Notebooks')
insert into PRODUCTOS values
('M01', 'Mochila Porta Notebook', 'Esta mochila combina un diseño elegante y profesional con la robustez necesaria para enfrentar el ajetreo urbano y los viajes de negocios.', 1, 1, 'China', 49999, 0),
('P03', 'Mouse Gamer Hero G502', 'Sumérgete en el mundo de los videojuegos con el mouse gamer Logitech G Series Hero G502 en color negro', 2, 2, 'Corea', 64999, 0),
('P08', 'Teclado Mecánico 75% Rk M75', 'Este teclado cuenta con un diseño compacto con 81 teclas, por lo que es fácil de transportar y usar en cualquier lugar.', 2, 3, 'Japon', 185000, 0),
('65BRAVIA8II', 'Televisor 65" BRAVIA 8 OLED 4K', 'Televisor OLED 4K con tecnología QD-OLED', 6, 4, 'Japon', 6399000, 0),
('65X855', 'Televisor 65" Serie X855 4K', 'Este televisor cuenta con resolucion 4K y Triluminos Display', 6, 4, 'Japon', 2300000, 0)

insert into imagenes values
(1, 'https://http2.mlstatic.com/D_NQ_NP_703368-MLU76300898146_052024-O.webp'),
(1, 'https://http2.mlstatic.com/D_NQ_NP_842545-MLU76300482840_052024-O.webp'),
(1, 'https://http2.mlstatic.com/D_NQ_NP_747302-MLU76300769244_052024-O.webp'),
(2, 'https://http2.mlstatic.com/D_NQ_NP_701613-MLA45464844877_042021-O.webp'),
(2, 'https://http2.mlstatic.com/D_NQ_NP_987670-MLA45464844880_042021-O.webp'),
(2, 'https://http2.mlstatic.com/D_NQ_NP_793119-MLU72761228270_112023-O.webp'),
(3, 'https://http2.mlstatic.com/D_NQ_NP_767460-MLA74282172500_022024-O.webp'),
(3, 'https://http2.mlstatic.com/D_NQ_NP_848157-MLA74517144673_022024-O.webp'),
(3, 'https://http2.mlstatic.com/D_NQ_NP_616027-MLA74397845971_022024-O.webp'),
(4, 'https://sony.scene7.com/is/image/sonyglobalsolutions/TVFY24_UM_1_FrontWithStand_M?$productIntroPlatemobile$&fmt=png-alpha'),
(4, 'https://sony.scene7.com/is/image/sonyglobalsolutions/TVFY24_UM_2_CW_M?$productIntroPlatemobile$&fmt=png-alpha'),
(4, 'https://sony.scene7.com/is/image/sonyglobalsolutions/TVFY24_UM_3_CCW_M?$productIntroPlatemobile$&fmt=png-alpha'),
(4, 'https://sony.scene7.com/is/image/sonyglobalsolutions/TVFY24_UM_5_Bezel_M?$productIntroPlatemobile$&fmt=png-alpha'),
(4, 'https://sony.scene7.com/is/image/sonyglobalsolutions/TVFY24_UM_6_Stand_M?$productIntroPlatemobile$&fmt=png-alpha'),
(4, 'https://sony.scene7.com/is/image/sonyglobalsolutions/TVFY24_UM_0_insitu_M?$productIntroPlatemobile$&fmt=png-alpha')

insert into clientes values ('32333222', 'Doug', 'Narinas', 'doug@narinas.com','avenida 123', 'chuletas city', 1234)

--SELECT * FROM MARCAS
--SELECT * FROM CATEGORIAS
--SELECT * FROM PRODUCTOS