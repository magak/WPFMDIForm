#region MigraDoc - Creating Documents on the Fly
//
// Authors:
//   PDFsharp Team (mailto:PDFsharpSupport@pdfsharp.de)
//
// Copyright (c) 2001-2009 empira Software GmbH, Cologne (Germany)
//
// http://www.pdfsharp.com
// http://www.migradoc.com
// http://sourceforge.net/projects/pdfsharp
//
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included
// in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.
#endregion

using System;
using System.Diagnostics;
using System.Text;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using MigraDoc.DocumentObjectModel.Shapes;

namespace PDF
{
	class PDFRenderer
	{
		public class Service
		{
			public String vid;
			public float tarif;
			public String obem;
			public float nachisleno;
			public float lgoty;
			public float vsego;

			public Service(String vid, float tarif, String obem, float nachisleno, float lgoty, float vsego)
			{
				this.vid = vid;
				this.tarif = tarif;
				this.obem = obem;
				this.nachisleno = nachisleno;
				this.lgoty = lgoty;
				this.vsego = vsego;
			}
		}

		public class PageData
		{
			public String poluchatel;
			public String FIO;
			public String address;
			public String FLS;

			public int month;
			public int year;

			public float nachisleno;
			public float summaLgot;
			public float itogo;

			public String date;

			public Service[] services;
		}

		public static void GeneratePage(PageData data, string filePath)
		{
			DateTime now = DateTime.Now;
            string filename = filePath;
			//filename = Guid.NewGuid().ToString("D").ToUpper() + ".pdf";
			PdfDocument document = new PdfDocument();
			//document.Info.Title = "";
			//document.Info.Author = "";
			//document.Info.Subject = "";
			//document.Info.Keywords = "";

			PdfPage page = document.AddPage();
			XGraphics gfx = XGraphics.FromPdfPage(page);
			gfx.MUH = PdfFontEncoding.Unicode;

			// You always need a MigraDoc document for rendering.
			Document doc = new Document();
			MigraDoc.Rendering.DocumentRenderer docRenderer = new DocumentRenderer(doc);

			gfx.DrawImage(XImage.FromFile("form.png"),
				S(0.5f),
				S(0.4f),
				S(20.05f),
				S(14.2f));

			XPdfFontOptions fontOptions = new XPdfFontOptions(PdfFontEncoding.Unicode, PdfFontEmbedding.Always);
			XFont smallFont = new XFont("Times New Roman", 7, XFontStyle.Regular, fontOptions);
			XFont notSoSmallFont = new XFont("Times New Roman", 8, XFontStyle.Regular, fontOptions);
			XFont font = new XFont("Times New Roman", 9.3, XFontStyle.Regular, fontOptions);
			XFont bigFont = new XFont("Times New Roman", 10, XFontStyle.Bold, fontOptions);

			XBrush brush = XBrushes.Red;
			Color color = MigraDoc.DocumentObjectModel.Colors.Red;

			Action<String, double, double> DrawSmallText = (text, x, y) => gfx.DrawString(text, smallFont, brush, S(x), S(y), XStringFormats.TopLeft);
			Action<String, double, double> DrawNotSoSmallText = (text, x, y) => gfx.DrawString(text, notSoSmallFont, brush, S(x), S(y), XStringFormats.TopLeft);
			Action<String, double, double> DrawText = (text, x, y) => gfx.DrawString(text, font, brush, S(x), S(y), XStringFormats.TopLeft);
			Action<String, double, double> DrawBigText = (text, x, y) => gfx.DrawString(text, bigFont, brush, S(x), S(y), XStringFormats.TopLeft);

			Action<float, double, double> DRV = (value, x, y) => DrawRightValue(doc, docRenderer, gfx, color, "Times New Roman", 7.2, value, x, y);
			Action<String, double, double> DLTf8 = (text, x, y) => DrawLeftText(doc, docRenderer, gfx, color, "Times New Roman", 8.1, text, x, y);
			Action<float, double, double> DRVf8 = (value, x, y) => DrawRightValue(doc, docRenderer, gfx, color, "Times New Roman", 8.1, value, x, y);
			Action<String, double, double, double> DCTf8 = (text, x, y, width) => DrawCenterText(doc, docRenderer, gfx, color, "Times New Roman", 8.8, text, x, y, width);
			Action<String, double, double> DLTf6 = (text, x, y) => DrawLeftText(doc, docRenderer, gfx, color, "Times New Roman", 6.7, text, x, y);
			Action<float, double, double> DRVf6 = (value, x, y) => DrawRightValue(doc, docRenderer, gfx, color, "Times New Roman", 6.8, value, x, y);
			Action<String, double, double> DRTf6 = (text, x, y) => DrawRightText(doc, docRenderer, gfx, color, "Times New Roman", 6.8, text, x, y);

			DCTf8(data.poluchatel, 5.45, 0.9, 14.4);

			DrawText(data.FIO, 6.52, 1.98);
			DrawText(data.address, 6.53, 2.43);
			DrawText(data.FLS, 15.91, 2.65);

			DrawBigText(data.month.ToString(), 15.82, 2.1f);
			DrawBigText(data.year.ToString(), 18.37, 2.1f);

			DrawRightValue(doc, docRenderer, gfx, color, "Arial", 10, data.itogo, 8.63f, 3.82f);
			DrawRightValue(doc, docRenderer, gfx, color, "Times New Roman", 10, data.itogo, 4.64f, 5.51f);

			//--------------------------------------------
			DCTf8(data.poluchatel, 5.45, 7.23, 14.4);

			double sx0 = 5.44;
			double sy0 = 8.92;
			double systep = 0.295;
			int serviceIndex = 0;
			for (int i = 0; i < data.services.Length; i++)
			{
				Service service = data.services[i];
				if (service == null)
					continue; //no db response

				double y = sy0 + systep * serviceIndex;
				DLTf6(service.vid, sx0, y);
				DRVf6(service.tarif, sx0 - 1.56, y);
				DRTf6(service.obem, sx0 - 0.08, y);
				DRVf6(service.nachisleno, sx0 + 1.14, y);
				DRVf6(service.lgoty, sx0 + 2.24, y);
				DRVf6(service.vsego, sx0 + 3.44, y);

				serviceIndex++;
			}

			DrawSmallText(data.FIO, 14.97, 8.2);
			DrawSmallText(data.address, 14.97, 8.6);
			DrawSmallText(Months(data.month) + " " + data.year, 15.88, 9.01);
			DrawSmallText(data.FLS, 18.84, 9.01);

			{
				double x0 = 14.091;
				double y0 = 9.45;
				double xv = 15.28;
				DLTf8("ИТОГО К ОПЛАТЕ", x0, y0);

				DRVf8(data.itogo, xv, y0);
			}

			DRV(data.nachisleno, 6.55, 13.02);
			DRV(data.summaLgot, 7.65, 13.02);
			DRV(data.itogo, 8.85, 13.02);

			DrawNotSoSmallText(data.date, 3.58, 13.83);

			docRenderer.PrepareDocument();


			Debug.WriteLine("seconds=" + (DateTime.Now - now).TotalSeconds.ToString());

			// Save the document...
			document.Save(filename);
		}

		static void DrawFormattedValue(Document doc, MigraDoc.Rendering.DocumentRenderer docRenderer, XGraphics gfx,
			Color color, String fontName, Unit fontSize, ParagraphAlignment alignment,
			float value,
			double x, double y, double width = 5)
		{
			DrawFormatted(doc, docRenderer, gfx,
			color, fontName, fontSize, alignment,
			value.ToString("F2"),
			x, y, width);
		}
		static void DrawRightValue(Document doc, MigraDoc.Rendering.DocumentRenderer docRenderer, XGraphics gfx,
			Color color, String fontName, Unit fontSize,
			float value,
			double x, double y, double width = 5)
		{
			DrawFormattedValue(doc, docRenderer, gfx,
			color, fontName, fontSize, ParagraphAlignment.Right,
			value,
			x, y, width);
		}
		static void DrawLeftText(Document doc, MigraDoc.Rendering.DocumentRenderer docRenderer, XGraphics gfx,
			Color color, String fontName, Unit fontSize,
			String text,
			double x, double y, double width = 5)
		{
			DrawFormatted(doc, docRenderer, gfx,
			color, fontName, fontSize, ParagraphAlignment.Left,
			text,
			x, y, width);
		}
		static void DrawRightText(Document doc, MigraDoc.Rendering.DocumentRenderer docRenderer, XGraphics gfx,
			Color color, String fontName, Unit fontSize,
			String text,
			double x, double y, double width = 5)
		{
			DrawFormatted(doc, docRenderer, gfx,
			color, fontName, fontSize, ParagraphAlignment.Right,
			text,
			x, y, width);
		}
		static void DrawCenterText(Document doc, MigraDoc.Rendering.DocumentRenderer docRenderer, XGraphics gfx,
			Color color, String fontName, Unit fontSize,
			String text,
			double x, double y, double width = 5)
		{
			DrawFormatted(doc, docRenderer, gfx,
			color, fontName, fontSize, ParagraphAlignment.Center,
			text,
			x, y, width);
		}

		static void DrawFormatted(Document doc, MigraDoc.Rendering.DocumentRenderer docRenderer, XGraphics gfx,
			Color color, String fontName, Unit fontSize, ParagraphAlignment alignment,
			String text,
			double x, double y, double width = 5)
		{
			Section sec = doc.AddSection();
			Paragraph para = sec.AddParagraph();
			para.Format.Alignment = alignment;
			para.Format.Font.Name = fontName;
			para.Format.Font.Size = fontSize;
			para.Format.Font.Bold = true;
			para.Format.Font.Color = color;
			//para.Format.Borders.Color = color;
			//para.Format.LineSpacing = 10;
			//para.Format.LineSpacingRule = LineSpacingRule.Exactly;
			para.AddText(text);
			docRenderer.RenderObject(gfx, XUnit.FromCentimeter(x), XUnit.FromCentimeter(y), XUnit.FromCentimeter(width), para);
		}

		static double S(double centimeters)
		{
			return XUnit.FromCentimeter(centimeters).Point;
		}


		static double A4Width = XUnit.FromCentimeter(21).Point;
		static double A4Height = XUnit.FromCentimeter(29.7).Point;

		static String[] months =
		{
			"легендарный нулевой месяц",
			"январь",
			"февраль",
			"март",
			"апрель",
			"май",
			"июнь",
			"июль",
			"август",
			"сентябрь",
			"октябрь",
			"ноябрь",
			"декабрь"
		};
		static String[] monthsIn =
		{
			"легендарном нулевом месяце",
			"январе",
			"феврале",
			"марте",
			"апреле",
			"мае",
			"июне",
			"июле",
			"августе",
			"сентябре",
			"октябре",
			"ноябре",
			"декабре"
		};
		static String Months(int month)
		{
			String text = months[month];
			return text.Substring(0, 1).ToString().ToUpper() + text.Substring(1);
		}
	}
}
