
CREATE NONCLUSTERED INDEX [IDX_dw_unidades_facturadas_busquedaavanzada] ON [dbo].[tbl_dw_unidades_facturadas]
(
	[nu_vin] ASC,
	[no_asesor_comercial] ASC,
	[an_fabricacion] ASC,
	[an_modelo] ASC,
	[fe_entrega] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

