USE [SGAPROD]
GO

SET ANSI_PADDING ON
GO

/****** Object:  Index [IDX_gnet_pedido_interfaz_facturacion]    Script Date: 5/01/2018 16:50:03 ******/
CREATE NONCLUSTERED INDEX [IDX_gnet_pedido_interfaz_facturacion] ON [dbo].[gnet_pedido]
(
	[nid_cliente_direccion] ASC,
	[fl_inactivo] ASC,
	[co_canal_venta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

