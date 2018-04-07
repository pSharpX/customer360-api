using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Gildemeister.Cliente360.Common
{
    public static class UtilExcel
    {
        public static ArchivoReporte ExportarArchivo(PlantillaExcel Plantilla)
        {
            var resultado = new ArchivoReporte();

            GenerarNombreArchivoYContentType(resultado, Plantilla);
            GeneraArchivoEnBytes(resultado, Plantilla);

            return resultado;

        }

        private static void GenerarNombreArchivoYContentType(ArchivoReporte resultado, PlantillaExcel Plantilla)
        {
            resultado.ContentType = "application/vnd.ms-excel";
            resultado.NombreArchivo = string.Format("{0}.xlsx", Plantilla.NombreArchivo ?? "ReportExcel");
        }

        private static void GeneraArchivoEnBytes(ArchivoReporte Archivo, PlantillaExcel Plantilla)
        {
            using (var ArchivoEnMemoria = new MemoryStream())
            {
                var ArchivoExcel = GenerarExcelNPOI(Plantilla);
                ArchivoExcel.Write(ArchivoEnMemoria);
                Archivo.ArchivoByte = ArchivoEnMemoria.ToArray();
            }
        }

        private static XSSFWorkbook GenerarExcelNPOI(PlantillaExcel Plantilla)
        {
            XSSFWorkbook workbook = new XSSFWorkbook();
            XSSFSheet sheet = (XSSFSheet)workbook.CreateSheet("Resultado");

            bool CrearCabeceras = true;
            Plantilla.FilasCreadasIndice = 1;
            var DiccionarioColumnas = (Dictionary<string, string>)(Plantilla.CabeceraColumna);

            if (Plantilla.Datos != null && Plantilla.Datos.GetType().IsGenericType)
            {
                foreach (var FilaDato in (IList)Plantilla.Datos)
                {
                    PropertyInfo[] ColumnasDatos = FilaDato.GetType().GetProperties();

                    if (CrearCabeceras)
                    {
                        CrearTituloExcel(workbook, sheet, ColumnasDatos.Length, Plantilla);
                        CrearSeccionFiltro(workbook, sheet, ColumnasDatos, Plantilla);
                        Plantilla.FilasCreadasIndice++;
                        CrearSeccionCabeceraDatos(workbook, sheet, Plantilla, ColumnasDatos);
                        CrearCabeceras = false;
                    }

                    InsertarFilaDeDatos(workbook, sheet, Plantilla, ColumnasDatos, FilaDato);

                }
            }

            return workbook;


        }

        private static void CrearSeccionCabeceraDatos(XSSFWorkbook workbook, XSSFSheet HojaActual, PlantillaExcel Plantilla, PropertyInfo[] ColumnasDatos)
        {
            XSSFRow rowHeaderGrid = (XSSFRow)HojaActual.CreateRow(Plantilla.FilasCreadasIndice);
            int ContadorColumna = 0;

            foreach (var itemCols in ColumnasDatos)
            {
                XSSFCell colGridExcel = (XSSFCell)rowHeaderGrid.CreateCell(ContadorColumna);
                if (Plantilla.EstiloCeldaCabecera != null)
                {
                    colGridExcel.CellStyle = GenerarCellStyleCeldaCabecera(workbook, Plantilla);
                }

                string colName = Plantilla.CabeceraColumna.Any(x => x.Key == itemCols.Name) ? Plantilla.CabeceraColumna.FirstOrDefault(x => x.Key == itemCols.Name).Value : itemCols.Name;
                int colWidth = Plantilla.TamanioColumna.Any(x => x.Key == itemCols.Name) ? Plantilla.TamanioColumna.FirstOrDefault(x => x.Key == itemCols.Name).Value : 80;

                colGridExcel.SetCellValue(new XSSFRichTextString(colName));
                colGridExcel.Sheet.DefaultColumnWidth = colWidth;
                ContadorColumna++;
            }

            Plantilla.FilasCreadasIndice++;
        }

        private static ICellStyle GenerarCellStyleCelda(XSSFWorkbook workbook, PlantillaExcel Plantilla)
        {
            var style1 = workbook.CreateCellStyle();
            var font1 = workbook.CreateFont();

            if (Plantilla.EstiloCelda.ColorLetra > 0)
            {
                font1.Color = Plantilla.EstiloCeldaCabecera.ColorLetra;

            }

            if (Plantilla.EstiloCelda.ColorFondo > 0)
            {
                style1.FillForegroundColor = Plantilla.EstiloCelda.ColorFondo;
                style1.FillPattern = FillPattern.SolidForeground;
            }

            if (Plantilla.EstiloCelda.Tamanio > 0)
            {
                font1.FontHeight = Plantilla.EstiloCelda.Tamanio;
            }

            if (Plantilla.EstiloCelda.EsNegrita)
            {
                font1.IsBold = Plantilla.EstiloCelda.EsNegrita;
            }


            if (Plantilla.EstiloCelda.AjustarCelda)
            {
                style1.ShrinkToFit = Plantilla.EstiloCelda.AjustarCelda;
            }

            style1.SetFont(font1);

            return style1;
        }
        private static ICellStyle GenerarCellStyleCeldaCabecera(XSSFWorkbook workbook, PlantillaExcel Plantilla)
        {
            var style1 = workbook.CreateCellStyle();
            var font1 = workbook.CreateFont();

            if (Plantilla.EstiloCeldaCabecera.ColorLetra > 0)
            {
                font1.Color = Plantilla.EstiloCeldaCabecera.ColorLetra;
            }

            if (Plantilla.EstiloCeldaCabecera.ColorFondo > 0)
            {
                style1.FillForegroundColor = Plantilla.EstiloCeldaCabecera.ColorFondo;
                style1.FillPattern = FillPattern.SolidForeground;
            }

            if (Plantilla.EstiloCeldaCabecera.Tamanio > 0)
            {
                font1.FontHeight = Plantilla.EstiloCeldaCabecera.Tamanio;
            }

            if (Plantilla.EstiloCeldaCabecera.EsNegrita)
            {
                font1.IsBold = Plantilla.EstiloCeldaCabecera.EsNegrita;
            }


            style1.SetFont(font1);

            return style1;
        }

        private static ICellStyle GenerarCellStyleFiltro(XSSFWorkbook workbook, PlantillaExcel Plantilla)
        {
            var style1 = workbook.CreateCellStyle();
            var font1 = workbook.CreateFont();

            if (Plantilla.EstiloFiltro.ColorLetra > 0)
            {
                font1.Color = Plantilla.EstiloFiltro.ColorLetra;
            }

            if (Plantilla.EstiloFiltro.ColorFondo > 0)
            {
                style1.FillForegroundColor = Plantilla.EstiloFiltro.ColorFondo;
                style1.FillPattern = FillPattern.SolidForeground;
            }


            if (Plantilla.EstiloFiltro.Tamanio > 0)
            {
                font1.FontHeight = Plantilla.EstiloFiltro.Tamanio;
            }

            if (Plantilla.EstiloFiltro.EsNegrita)
            {
                font1.IsBold = Plantilla.EstiloFiltro.EsNegrita;
            }


            style1.SetFont(font1);

            return style1;
        }

        private static ICellStyle GenerarCellStyleTitulo(XSSFWorkbook workbook, PlantillaExcel Plantilla)
        {
            var style1 = workbook.CreateCellStyle();
            var font1 = workbook.CreateFont();

            if (Plantilla.EstiloTitulo.ColorLetra > 0)
            {
                font1.Color = Plantilla.EstiloTitulo.ColorLetra;
            }

            if (Plantilla.EstiloTitulo.ColorFondo > 0)
            {
                style1.FillForegroundColor = Plantilla.EstiloTitulo.ColorFondo;
                style1.FillPattern = FillPattern.SolidForeground;
            }

            if (Plantilla.EstiloTitulo.Tamanio > 0)
            {
                font1.FontHeight = Plantilla.EstiloTitulo.Tamanio;
            }

            if (Plantilla.EstiloTitulo.EsNegrita)
            {
                font1.IsBold = Plantilla.EstiloTitulo.EsNegrita;
            }
            style1.SetFont(font1);

            return style1;
        }

        private static void InsertarFilaDeDatos(XSSFWorkbook workbook, XSSFSheet HojaActual, PlantillaExcel Plantilla, PropertyInfo[] ColumnasDatos, object FilaDato)
        {
            int ContadorColumna = 0;
            XSSFRow row = (XSSFRow)HojaActual.CreateRow(Plantilla.FilasCreadasIndice);

            foreach (var itemCol in ColumnasDatos)
            {
                var strValue = string.Empty;

                if (itemCol.GetValue(FilaDato) != null)
                {
                    if (itemCol.PropertyType == typeof(string) || itemCol.PropertyType == typeof(int))
                    {
                        strValue = itemCol.GetValue(FilaDato).ToString();
                    }
                    else if (itemCol.PropertyType == typeof(DateTime))
                    {
                        strValue = DateTime.Parse(itemCol.GetValue(FilaDato).ToString()).ToShortDateString();
                    }
                    else if (itemCol.PropertyType == typeof(bool))
                    {
                        strValue = itemCol.GetValue(FilaDato).ToString().ToUpper() == "TRUE" ? "SI" : "NO";
                    }
                    else
                    {
                        strValue = itemCol.GetValue(FilaDato).ToString();
                    }
                }

                var NuevaCelda = row.CreateCell(ContadorColumna);
                if (Plantilla.EstiloCelda != null)
                {
                    NuevaCelda.CellStyle = GenerarCellStyleCelda(workbook, Plantilla);
                }

                NuevaCelda.SetCellValue(strValue);
                ContadorColumna++;
            }
            Plantilla.FilasCreadasIndice++;
        }

        private static void CrearTituloExcel(XSSFWorkbook workbook, XSSFSheet HojaActual, int NumeroColumnas, PlantillaExcel Plantilla)
        {
            XSSFRow FilaTitulo = (XSSFRow)HojaActual.CreateRow(Plantilla.FilasCreadasIndice);

            int PosicionTitulo = 0;

            if (NumeroColumnas > 0)
            {
                PosicionTitulo = NumeroColumnas / 2;
            }
            var NuevaCelda = FilaTitulo.CreateCell(PosicionTitulo);
            if (Plantilla.EstiloTitulo != null)
            {
                NuevaCelda.CellStyle = GenerarCellStyleTitulo(workbook, Plantilla);
            }

            NuevaCelda.SetCellValue(Plantilla.TituloExcel);
            Plantilla.FilasCreadasIndice = 3;
        }

        private static void CrearSeccionFiltro(XSSFWorkbook workbook, XSSFSheet HojaActual, PropertyInfo[] ColumnasDatos, PlantillaExcel Plantilla)
        {

            XSSFRow FilaFiltros = null;

            if (Plantilla.Filtros != null)
            {
                foreach (var item in Plantilla.Filtros)
                {
                    FilaFiltros = (XSSFRow)HojaActual.CreateRow(Plantilla.FilasCreadasIndice);

                    var NuevaCelda = FilaFiltros.CreateCell(0);
                    if (Plantilla.EstiloFiltro != null)
                    {
                        NuevaCelda.CellStyle = GenerarCellStyleFiltro(workbook, Plantilla);
                    }

                    NuevaCelda.SetCellValue(item);
                    Plantilla.FilasCreadasIndice++;
                }

            }
        }
    }

    public class ArchivoReporte
    {
        public byte[] ArchivoByte { get; set; }
        public string ContentType { get; set; }
        public string NombreArchivo { get; set; }
    }

    public class EstiloCelda
    {
        public short ColorFondo { get; set; }
        public short ColorLetra { get; set; }
        public string Font { get; set; }
        public int Tamanio { get; set; }
        public bool EsNegrita { get; set; }
        public bool AjustarCelda { get; set; }
    }

    public class PlantillaExcel
    {
        public string TituloExcel { get; set; }
        public object Datos { get; set; }
        public List<string> Filtros { get; set; }
        public Dictionary<string, string> CabeceraColumna { get; set; }
        public Dictionary<string, int> TamanioColumna { get; set; }
        public string NombreArchivo { get; set; }
        public int FilasCreadasIndice { get; set; }

        public EstiloCelda EstiloTitulo { get; set; }
        public EstiloCelda EstiloFiltro { get; set; }
        public EstiloCelda EstiloCeldaCabecera { get; set; }
        public EstiloCelda EstiloCelda { get; set; }
    }
}
