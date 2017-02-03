using System;
using System.Windows;

namespace WPFMDIForm
{
   
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void menuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

		private void print_Click(object sender, RoutedEventArgs e)
		{
			PDF.PDFRenderer.PageData pageData = new PDF.PDFRenderer.PageData()
			{
				poluchatel = "ЖСК \"Плановик-4\" ИНН 7708019820 АО Банк \"ТГБ\" р/с 40703810320000000006 к/с\n30101810345250000208 в ГУ  Банка России по ЦФО БИК 044525208",
				FIO = "Конская Ольга Львовна",
				address = "Б.Спасская д.10/1 кв.408",
				FLS = "0000000408",

				month = 12,
				year = 2016,

				nachisleno = 2328.17f,
				summaLgot = 0,
				itogo = 2328.17f,

				date = "25.01.2017",

				services = new PDF.PDFRenderer.Service[]
				{
					new PDF.PDFRenderer.Service("Сод.общ.имущ.дома", 23.6f, "37.20м2", 877.92f, 0, 877.92f),
					new PDF.PDFRenderer.Service("Отопление ДПУ", 2101.52f, "0.5952Гкал", 1250.83f, 0, 1250.83f),
					new PDF.PDFRenderer.Service("Антенна", 95f, "0.5аб-т", 47.50f, 0, 47.50f),
					new PDF.PDFRenderer.Service("Домофон", 39f, "1аб-т", 39f, 0, 39f),
					new PDF.PDFRenderer.Service("ХВС", 33.03f, "2м3", 66.06f, 0, 66.06f),
					new PDF.PDFRenderer.Service("Водоотв.", 23.43f, "2м3", 46.86f, 0, 46.86f),
				}
			};

			PDF.PDFRenderer.GeneratePage(pageData);
		}
		private void changeUser_Click(object sender, RoutedEventArgs e)
		{
			_openPage(new PageLogin());
		}
		private void flats_Click(object sender, RoutedEventArgs e)
		{
			_openPage(new PageFlats());
		}
		private void tariffs_Click(object sender, RoutedEventArgs e)
		{
			//_openPage(new PageTariffs());
		}
		private void flatData_Click(object sender, RoutedEventArgs e)
		{
			//_openPage(new PageFlatData());
		}
		private void oduData_Click(object sender, RoutedEventArgs e)
		{
			_openPage(new PageODU());
		}

		private void _openPage(UIElement content)
		{
			MainContainer.Content = content;
		}
    }
}
