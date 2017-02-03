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

			public float dolg;
			public float itogo;
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

			gfx.DrawImage(XImage.FromFile("form.png"),
				S(0.5f),
				S(0.4f),
				S(20.05f),
				S(14.2f));

			XPdfFontOptions fontOptions = new XPdfFontOptions(PdfFontEncoding.Unicode, PdfFontEmbedding.Always);
			XFont font = new XFont("Times New Roman", 9.5, XFontStyle.Regular, fontOptions);
			XFont bigFont = new XFont("Times New Roman", 10, XFontStyle.Bold, fontOptions);

			XBrush brush = XBrushes.Red;
			Color color = MigraDoc.DocumentObjectModel.Colors.Red;

			Action<String, double, double> DrawText = (text, x, y) => gfx.DrawString(text, font, brush, x, y, XStringFormats.TopLeft);
			Action<String, double, double> DrawBigText = (text, x, y) => gfx.DrawString(text, bigFont, brush, x, y, XStringFormats.TopLeft);

			DrawText(data.FIO, S(6.5f), S(1.98f));
			DrawText(data.address, S(6.5f), S(2.43f));

			DrawBigText(data.month.ToString(), S(15.82f), S(2.1f));
			DrawBigText(data.year.ToString(), S(18.35f), S(2.1f));

			// You always need a MigraDoc document for rendering.
			Document doc = new Document();
			MigraDoc.Rendering.DocumentRenderer docRenderer = new DocumentRenderer(doc);

			DrawRightValue(doc, docRenderer, gfx, color, data.dolg, 8, 3.11f, 5.63f);
			DrawRightValue(doc, docRenderer, gfx, color, data.itogo, 8, 3.82f, 5.63f);

			docRenderer.PrepareDocument();

	
			Debug.WriteLine("seconds=" + (DateTime.Now - now).TotalSeconds.ToString());

			// Save the document...
			document.Save(filename);
			// ...and start a viewer
			Process.Start(filename);
		}

		static void DrawRightValue(Document doc, MigraDoc.Rendering.DocumentRenderer docRenderer, XGraphics gfx, Color color, float value,
			float x, float y, float width)
		{
			Section sec = doc.AddSection();
			Paragraph para = sec.AddParagraph();
			para.Format.Alignment = ParagraphAlignment.Right;
			para.Format.Font.Name = "Arial";
			para.Format.Font.Size = 10;
			para.Format.Font.Bold = true;
			para.Format.Font.Color = color;
			para.AddText(value.ToString("F2"));
			docRenderer.RenderObject(gfx, XUnit.FromCentimeter(x), XUnit.FromCentimeter(y), XUnit.FromCentimeter(width), para);
		}

		static double S(float centimeters)
		{
			return XUnit.FromCentimeter(centimeters).Point;
		}


		static double A4Width = XUnit.FromCentimeter(21).Point;
		static double A4Height = XUnit.FromCentimeter(29.7).Point;
	}
}
