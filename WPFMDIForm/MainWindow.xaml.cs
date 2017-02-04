using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;

namespace WPFMDIForm
{
   
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.DataContext = this;
            InitializeComponent();
        }
        
        private void menuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }        

		private void print_Click(object sender, RoutedEventArgs e)
		{
            //BackgroundWorker bw = new BackgroundWorker();

            //progress.Visibility = System.Windows.Visibility.Visible;
            //bw.DoWork += new DoWorkEventHandler((s, ev) => {
            //    //generate(14);
            //});
            //bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler((s, ev)
            //    => {
            //    progress.Visibility = System.Windows.Visibility.Hidden;
            //});
            //bw.RunWorkerAsync();
		}

        //private void generate(int period)
        //{
        //    Func<SqlDataReader, String, float> read = (reader, columnName) =>
        //    {
        //        int column = reader.GetOrdinal(columnName);
        //        if (reader.IsDBNull(column))
        //            return 0; //empty

        //        return Decimal.ToSingle(reader.GetDecimal(column));
        //    };

        //    // Посчитаем сначало все услуги для всех квартир
        //    Dictionary<int, PDF.PDFRenderer.Service> gvs = new Dictionary<int, PDF.PDFRenderer.Service>();

        //    DataOperations.ReadRows(CommandType.Text,
        //        string.Format("exec [dbo].[sp_CalcGVSForPeriod] @period = {0}", period),
        //        reader =>
        //        {
        //            gvs.Add(reader.GetInt32(reader.GetOrdinal("Id")), new PDF.PDFRenderer.Service("ГВС",
        //                tarif: read(reader, "Тариф"),
        //                obem: read(reader, "diff") + "м3",
        //                nachisleno: read(reader, "val"),
        //                lgoty: read(reader, "lgota"),
        //                vsego: read(reader, "total")
        //                )
        //            );
        //        });

        //    // Теперь прокаимся по всем квартирам выцепляя инфу про каждого
        //    // не забывая про вычисленные услуи
        //    DataOperations.ReadRows(CommandType.Text,
        //        string.Format("exec [dbo].[sp_FetchFlatsInfo] @period = {0}", period),
        //        reader =>
        //        {
        //            int flatId = reader.GetInt32(reader.GetOrdinal("Id"));

        //            List<PDF.PDFRenderer.Service> srvs = new List<PDF.PDFRenderer.Service>()
        //            {
        //                new PDF.PDFRenderer.Service("Сод.общ.имущ.дома", 23.6f, "37.20м2", 877.92f, 0, 877.92f),
        //                new PDF.PDFRenderer.Service("Отопление ДПУ", 2101.52f, "0.5952Гкал", 1250.83f, 0, 1250.83f),
        //                new PDF.PDFRenderer.Service("Антенна", 95f, "0.5аб-т", 47.50f, 0, 47.50f),
        //                new PDF.PDFRenderer.Service("Домофон", 39f, "1аб-т", 39f, 0, 39f),
        //                new PDF.PDFRenderer.Service("ХВС", 33.03f, "2м3", 66.06f, 0, 66.06f),
        //                new PDF.PDFRenderer.Service("Водоотв.", 23.43f, "2м3", 46.86f, 0, 46.86f),
        //            };

        //            if(gvs.ContainsKey(flatId))
        //            {
        //                srvs.Add(gvs[flatId]);
        //            }

        //            PDF.PDFRenderer.PageData pageData = new PDF.PDFRenderer.PageData()
        //            {
        //                poluchatel = "ЖСК \"Плановик-4\" ИНН 7708019820 АО Банк \"ТГБ\" р/с 40703810320000000006 к/с\n30101810345250000208 в ГУ  Банка России по ЦФО БИК 044525208",
        //                FIO = reader.GetString(reader.GetOrdinal("ФИО")),// "Конская Ольга Львовна",
        //                address = reader.GetString(reader.GetOrdinal("Адрес")),// "Б.Спасская д.10/1 кв.408",
        //                FLS = reader.GetString(reader.GetOrdinal("FLS")),

        //                month = reader.GetInt32(reader.GetOrdinal("Месяц")),//12,
        //                year = reader.GetInt32(reader.GetOrdinal("Год")),//2016,

        //                nachisleno = 2328.17f,
        //                summaLgot = 0,
        //                itogo = 2328.17f,

        //                date = "25.01.2017",

        //                services = srvs.ToArray()
        //            };

        //            if (!Directory.Exists(ReportFolder))
        //                Directory.CreateDirectory(ReportFolder);

        //            PDF.PDFRenderer.GeneratePage(pageData, Path.Combine(ReportFolder, string.Format("{0}.pdf", pageData.FIO)));
        //        });

        //    // откроем папку
        //    Process.Start(ReportFolder);            
        //}

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
            _openPage(new PageReadings());
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
