using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFMDIForm.JKHModel;

namespace WPFMDIForm
{
    /// <summary>
    /// Interaction logic for Page6.xaml
    /// </summary>
    public partial class PageReadings : UserControl
    {
        public const string ReportFolder = "Reports";
        public PageReadings()
        {
            DataContext = this;

            context = new JKHModelContainer();

            Calendars = new List<Календарь>();
            Calendars.Add(new Календарь());
            Calendars.AddRange(context.КалендарьSet);

            InitializeComponent();                        

            updateListData();
            dgvTable.IsReadOnly = true;
        }

        JKHModelContainer context;

        public List<Календарь> Calendars { get; set; }

        public Показания_квартир SelectedReading { get; set; }

        public Календарь _selectedCal;
        public Календарь SelectedCal
        {
            get { return _selectedCal; }
            set 
            { 
                _selectedCal = value;
                updateListData();
            }
        }

        private void updateListData()
        {
            if (SelectedCal ==null || string.IsNullOrEmpty(SelectedCal.Год) || string.IsNullOrEmpty(SelectedCal.Год))
            {
                dgvTable.ItemsSource = context.Показания_квартирSet.ToList();
            }
            else
            {
                dgvTable.ItemsSource = context.Показания_квартирSet.Where(s => s.Календарь.Id == SelectedCal.Id)
                    .ToList();
            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedReading != null)
            {
                context.Показания_квартирSet.Remove(SelectedReading);
                context.SaveChanges();
                updateListData();
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            if (context != null)
                context.Dispose();
        }

        private void btnUpd_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedReading == null)
                return;

            WindowAddReading window = new WindowAddReading(SelectedReading.Id);
            var result = window.ShowDialog();
            if (result ?? false)
            {
                context.Entry<Показания_квартир>(SelectedReading).Reload();
                updateListData();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            WindowAddReading window = new WindowAddReading();
            var result = window.ShowDialog();
            if (result ?? false)
            {
                updateListData();
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedCal == null)
                return;

            BackgroundWorker bw = new BackgroundWorker();

            progress.Visibility = System.Windows.Visibility.Visible;
            this.IsEnabled = false;
            bw.DoWork += new DoWorkEventHandler((s, ev) =>
            {
                generate(SelectedCal.Id);
            });
            
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler((s, ev)
                =>
                {
                    progress.Visibility = System.Windows.Visibility.Hidden;
                    this.IsEnabled = true;

                    if(ev.Error != null)
                    {
                        throw ev.Error;
                    }
                });
            bw.RunWorkerAsync();
        }


        private void generate(int period)
        {
            Func<SqlDataReader, String, float> read = (reader, columnName) =>
            {
                int column = reader.GetOrdinal(columnName);
                if (reader.IsDBNull(column))
                    return 0; //empty

                return Decimal.ToSingle(reader.GetDecimal(column));
            };

            // Расчет ГВС для всех квартир
            Dictionary<int, PDF.PDFRenderer.Service> gvs = new Dictionary<int, PDF.PDFRenderer.Service>();
            DataOperations.ReadRows(CommandType.Text,
                string.Format("exec [dbo].[sp_CalcGVSForPeriod] @period = {0}", period),
                reader =>
                {
                    gvs.Add(reader.GetInt32(reader.GetOrdinal("Id")), new PDF.PDFRenderer.Service("ГВС",
                        tarif: read(reader, "Тариф"),
                        obem: read(reader, "diff") + "м3",
                        nachisleno: read(reader, "val"),
                        lgoty: read(reader, "lgota"),
                        vsego: read(reader, "total")
                        )
                    );
                });

            // Расчет ХВС для всех квартир
            Dictionary<int, PDF.PDFRenderer.Service> hvs = new Dictionary<int, PDF.PDFRenderer.Service>();
            DataOperations.ReadRows(CommandType.Text,
                string.Format("exec [dbo].[sp_CalcHVSForPeriod] @period = {0}", period),
                reader =>
                {
                    hvs.Add(reader.GetInt32(reader.GetOrdinal("Id")), new PDF.PDFRenderer.Service("ХВС",
                        tarif: read(reader, "Тариф"),
                        obem: read(reader, "diff") + "м3",
                        nachisleno: read(reader, "val"),
                        lgoty: read(reader, "lgota"),
                        vsego: read(reader, "total")
                        )
                    );
                });

            // Расчет водоотвода для всех квартир
            Dictionary<int, PDF.PDFRenderer.Service> vood = new Dictionary<int, PDF.PDFRenderer.Service>();
            DataOperations.ReadRows(CommandType.Text,
                string.Format("exec [dbo].[sp_CalcVodootvodForPeriod] @period = {0}", period),
                reader =>
                {
                    vood.Add(reader.GetInt32(reader.GetOrdinal("Id")), new PDF.PDFRenderer.Service("Водоотведение",
                        tarif: read(reader, "Тариф"),
                        obem: read(reader, "diff") + "м3",
                        nachisleno: read(reader, "val"),
                        lgoty: read(reader, "lgota"),
                        vsego: read(reader, "total")
                        )
                    );
                });

            // Расчет сожержания дома для всех квартир
            Dictionary<int, PDF.PDFRenderer.Service> soid = new Dictionary<int, PDF.PDFRenderer.Service>();
            DataOperations.ReadRows(CommandType.Text,
                string.Format("exec [dbo].[sp_CalcSOIDForPeriod] @period = {0}", period),
                reader =>
                {
                    soid.Add(reader.GetInt32(reader.GetOrdinal("Id")), new PDF.PDFRenderer.Service("Сод.общ.имущ.дома",
                        tarif: read(reader, "Тариф"),
                        obem: read(reader, "diff") + "м2",
                        nachisleno: read(reader, "val"),
                        lgoty: read(reader, "lgota"),
                        vsego: read(reader, "total")
                        )
                    );
                });

            // Расчет сожержания антены для всех квартир
            Dictionary<int, PDF.PDFRenderer.Service> antena = new Dictionary<int, PDF.PDFRenderer.Service>();
            DataOperations.ReadRows(CommandType.Text,
                string.Format("exec [dbo].[sp_CalcAntenaForPeriod] @period = {0}", period),
                reader =>
                {
                    antena.Add(reader.GetInt32(reader.GetOrdinal("Id")), new PDF.PDFRenderer.Service("Антенна",
                        tarif: read(reader, "Тариф"),
                        obem: read(reader, "diff") + "аб-т",
                        nachisleno: read(reader, "val"),
                        lgoty: read(reader, "lgota"),
                        vsego: read(reader, "total")
                        )
                    );
                });

            // Расчет сожержания домофона для всех квартир
            Dictionary<int, PDF.PDFRenderer.Service> domofon = new Dictionary<int, PDF.PDFRenderer.Service>();
            DataOperations.ReadRows(CommandType.Text,
                string.Format("exec [dbo].[sp_CalcDomofonForPeriod] @period = {0}", period),
                reader =>
                {
                    domofon.Add(reader.GetInt32(reader.GetOrdinal("Id")), new PDF.PDFRenderer.Service("Домофон",
                        tarif: read(reader, "Тариф"),
                        obem: read(reader, "diff") + "аб-т",
                        nachisleno: read(reader, "val"),
                        lgoty: read(reader, "lgota"),
                        vsego: read(reader, "total")
                        )
                    );
                });

            // Теперь прокаимся по всем квартирам выцепляя инфу про каждого
            // не забывая про вычисленные услуи
            DataOperations.ReadRows(CommandType.Text,
                string.Format("exec [dbo].[sp_FetchFlatsInfo] @period = {0}", period),
                reader =>
                {
                    int flatId = reader.GetInt32(reader.GetOrdinal("Id"));

                    List<PDF.PDFRenderer.Service> srvs = new List<PDF.PDFRenderer.Service>()
                    {
					    new PDF.PDFRenderer.Service("Отопление ДПУ", 2101.52f, "0.5952Гкал", 1250.83f, 0, 1250.83f),
                    };

                    float nachisleno = 0;
                    float summaLgot = 0;
                    float itogo = 0;                    

                    // добавим данные соид
                    if (soid.ContainsKey(flatId))
                    {
                        srvs.Add(soid[flatId]);
                        nachisleno += soid[flatId].nachisleno;
                        summaLgot += soid[flatId].lgoty;
                        itogo += soid[flatId].vsego;
                    }

                    // добавим данные ГВС
                    if (gvs.ContainsKey(flatId))
                    {
                        srvs.Add(gvs[flatId]);
                        nachisleno += gvs[flatId].nachisleno;
                        summaLgot += gvs[flatId].lgoty;
                        itogo += gvs[flatId].vsego;
                    }

                    // добавим данные ХВС
                    if (hvs.ContainsKey(flatId))
                    {
                        srvs.Add(hvs[flatId]);
                        nachisleno += hvs[flatId].nachisleno;
                        summaLgot += hvs[flatId].lgoty;
                        itogo += hvs[flatId].vsego;
                    }

                    // добавим данные водоотвод
                    if (vood.ContainsKey(flatId))
                    {
                        srvs.Add(vood[flatId]);
                        nachisleno += vood[flatId].nachisleno;
                        summaLgot += vood[flatId].lgoty;
                        itogo += vood[flatId].vsego;
                    }

                    // добавим данные антены
                    if (antena.ContainsKey(flatId))
                    {
                        srvs.Add(antena[flatId]);
                        nachisleno += antena[flatId].nachisleno;
                        summaLgot += antena[flatId].lgoty;
                        itogo += antena[flatId].vsego;
                    }
                    
                    // добавим данные домофона
                    if (domofon.ContainsKey(flatId))
                    {
                        srvs.Add(domofon[flatId]);
                        nachisleno += domofon[flatId].nachisleno;
                        summaLgot += domofon[flatId].lgoty;
                        itogo += domofon[flatId].vsego;
                    }

                    PDF.PDFRenderer.PageData pageData = new PDF.PDFRenderer.PageData()
                    {
                        poluchatel = "ЖСК \"Плановик-4\" ИНН 7708019820 АО Банк \"ТГБ\" р/с 40703810320000000006 к/с\n30101810345250000208 в ГУ  Банка России по ЦФО БИК 044525208",
                        FIO = reader.GetString(reader.GetOrdinal("ФИО")),// "Конская Ольга Львовна",
                        address = string.Format("{0} кв{1}", reader.GetString(reader.GetOrdinal("Адрес")), reader.GetString(reader.GetOrdinal("Номер_квартиры"))),// "Б.Спасская д.10/1 кв.408",
                        FLS = reader.GetString(reader.GetOrdinal("FLS")),

                        month = reader.GetInt32(reader.GetOrdinal("Месяц")),//12,
                        year = reader.GetInt32(reader.GetOrdinal("Год")),//2016,

                        nachisleno = nachisleno,
                        summaLgot = summaLgot,
                        itogo = itogo,

                        date = DateTime.Now.ToString("dd.MM.yyyy"),//сегодня,

                        services = srvs.ToArray()
                    };

                    if (!Directory.Exists(ReportFolder))
                        Directory.CreateDirectory(ReportFolder);

                    PDF.PDFRenderer.GeneratePage(pageData, System.IO.Path.Combine(ReportFolder, string.Format("{0}.pdf", pageData.FIO)));
                });

            // откроем папку
            Process.Start(ReportFolder);
        }
    }
}
