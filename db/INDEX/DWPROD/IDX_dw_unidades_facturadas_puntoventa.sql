
CREATE NONCLUSTERED INDEX [IDX_dw_unidades_facturadas_puntoventa] ON [dbo].[tbl_dw_unidades_facturadas]
(
	[nid_punto_venta] ASC,
	[fl_inactivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

