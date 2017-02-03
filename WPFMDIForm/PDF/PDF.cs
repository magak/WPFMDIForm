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
		public class PageData
		{
			public String FIO;
			public String address;

			public int month;
			public int year;

			public float nachisleno;
			public float summaLgot;
			public float itogo;

			public String date;
		}

		public static void GeneratePage(PageData data)
		{
			DateTime now = DateTime.Now;
			string filename = "result.pdf";
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

			DrawText(data.FIO, 6.52f, 1.98f);
			DrawText(data.address, 6.53f, 2.43f);

			DrawBigText(data.month.ToString(), 15.82f, 2.1f);
			DrawBigText(data.year.ToString(), 18.37f, 2.1f);

			DrawRightValue(doc, docRenderer, gfx, color, "Arial", 10, data.itogo, 8.63f, 3.82f);
			DrawRightValue(doc, docRenderer, gfx, color, "Times New Roman", 10, data.itogo, 4.64f, 5.51f);

			//--------------------------------------------

			DrawSmallText(data.FIO, 14.97, 8.2);
			DrawSmallText(data.address, 14.97, 8.6);
			DrawSmallText(Months(data.month) + " " + data.year, 15.88, 9.01);

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
			// ...and start a viewer
			Process.Start(filename);
		}

		static void DrawFormattedValue(Document doc, MigraDoc.Rendering.DocumentRenderer docRenderer, XGraphics gfx,
			Color color, String fontName, Unit fontSize, ParagraphAlignment alignment,
			float value,
			double x, double y, float width = 5)
		{
			DrawFormatted(doc, docRenderer, gfx,
			color, fontName, fontSize, alignment,
			value.ToString("F2"),
			x, y, width);
		}
		static void DrawRightValue(Document doc, MigraDoc.Rendering.DocumentRenderer docRenderer, XGraphics gfx,
			Color color, String fontName, Unit fontSize,
			float value,
			double x, double y, float width = 5)
		{
			DrawFormattedValue(doc, docRenderer, gfx,
			color, fontName, fontSize, ParagraphAlignment.Right,
			value,
			x, y, width);
		}
		static void DrawLeftText(Document doc, MigraDoc.Rendering.DocumentRenderer docRenderer, XGraphics gfx,
			Color color, String fontName, Unit fontSize,
			String text,
			double x, double y, float width = 5)
		{
			DrawFormatted(doc, docRenderer, gfx,
			color, fontName, fontSize, ParagraphAlignment.Left,
			text,
			x, y, width);
		}

		static void DrawFormatted(Document doc, MigraDoc.Rendering.DocumentRenderer docRenderer, XGraphics gfx,
			Color color, String fontName, Unit fontSize, ParagraphAlignment alignment,
			String text,
			double x, double y, float width = 5)
		{
			Section sec = doc.AddSection();
			Paragraph para = sec.AddParagraph();
			para.Format.Alignment = alignment;
			para.Format.Font.Name = fontName;
			para.Format.Font.Size = fontSize;
			para.Format.Font.Bold = true;
			para.Format.Font.Color = color;
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
