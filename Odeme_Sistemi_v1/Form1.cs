using System.Reflection.PortableExecutable;
using System.Runtime.ConstrainedExecution;
using System.Security.Policy;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
//V1.ODEME SİSTEMİ: Form1 hem UI'yi yönetiyor, hem de ödeme mantığını içeriyor.
//Bu kötü bir tasarım. Çünkü tek sorumluluk prensibine aykırı.
namespace Odeme_Sistemi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


            odemeSecenekleri();

        }


        private void odemeSecenekleri()
        {
            cmbOdemeTipi.Items.Add("Kredi Kartı"); // comboBox'a seçenekleri kod ile ekledik. İsterseniz tasarım kısmında da ekleyebilirsiniz.
            cmbOdemeTipi.Items.Add("Havale/EFT");
            //Nakit ile ödemiyi buraya siz ekleyiniz.
            cmbOdemeTipi.SelectedIndex = 0; //form çalıştığında otomatik olarak 0. indeksteki bilgi (ilk sıradaki) gösterilsin.

            lblSonuc.Text = ""; //form çalıştığında işlem yapana kadar sonuc label'inde birşey yazmasın.
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }



        private void label1_Click(object sender, EventArgs e)
        {
        }



        private void btnode_Click(object sender, EventArgs e)
        {

            if (!decimal.TryParse(txtTutar.Text, out decimal tutar) || tutar <= 0)
            //TryParse: Güvenli dönüştürmek içindir. Hatalı çeviri olduğunda sonuc false olur, program hata vermez.
            //Convert kullansaydık, hata mesajı için try-catch yapısı gerekecekti.
            {
                MessageBox.Show("Geçerli bir tutar giriniz.");
                return; //Not: break, return, continue gibi yapılar kodun akışını kontrol etmek için kullanılır.
                        //break döngüyü kırar, continue döngünün o anki iterasyonunu atlar, return ise metodu sonlandırır ve çağıran yere geri döner.
            }

            string secim = cmbOdemeTipi.SelectedItem?.ToString()?? "";  //seçenekleri yazı ile veririz.
                                                                        //?. null ise devam etme, null döndür.” ?? → “Null ise şu değeri kullan.”
                                                                        // null = boş, yok, atanmadı. - null olan bir şeye .ToString(), .Length gibi işlemler yaparsan hata alırsın
                                                                        //?.ve ?? bu hataları engellemek için kullanılır




            //rakam ile: int odemeTipi = cmbOdemeTipi.SelectedIndex; // 0: Kredi Kartı, 1: Havale

            // KÖTÜ: Tüm iş tek yerde, if-else uzar gider
            if (secim == "Kredi Kartı") //rakam ile yapsaydık: if (odemeTipi == 0)
            {
                decimal sonTutar = tutar + (tutar * 0.05m); // %5 komisyon ekleyelim. m: decimal olduğunu belirtir. parasal işlemlerde decimal kullanmak hata riskini azaltır.
                lblSonuc.Text = $"Nakit ödeme alındı. Tutar: {sonTutar:0.00} TL"; //:0.00 → tutarı 2 ondalık basamakla gösterir.
            }
            else if (secim == "Havale/EFT")  //rakam ile yapsaydık: if (odemeTipi == 1)
            {
                decimal sonTutar = tutar - (tutar * 0.10m); // %10 indirim yapalım. Havale/EFT genellikle daha ucuz olur, bu yüzden indirim ekledik.
                lblSonuc.Text = $"Kredi Kartı ile ödeme alındı. Tutar: {sonTutar:0.00} TL";
            }
            //Nakit Ödeme yöntemini siz ekleyiniz.
            else
            {
                lblSonuc.Text = "Geçersiz ödeme tipi!";
            }







            //BU KOD YAPISI NEDEN KÖTÜ?
            //1. Magic string: "Kredi Kartı" gibi sabit string kullanmak kırılgan. Yazım değişirse kod bozulur.

            //2. Yeni ödeme türü eklersek btnOde_Click daha da uzar mı? Form1 içine sürekli else if eklemek gerekir, kod uzar, bakım zorlaşır.

            //3.Form neden hesaplama yapıyor? İş mantığı UI(User Interface) içinde → tek sorumluluk yok, test edilemez.

            //Yazılım geliştirmede Single Responsibility Principle(SRP) diye bir kural vardır:
            //“Her sınıfın tek bir sorumluluğu olmalı.”
            //Form1’in sorumluluğu aslında sadece UI’yi yönetmektir.
            //Ama bu örnekte Form1 aynı zamanda ödeme mantığını da içeriyor.
            //Bu yüzden tek sorumluluk kuralı bozuluyor.
            //Eğer ödeme mantığı ayrı bir sınıfta olsaydı(Odeme, KrediKartiOdeme, NakitOdeme gibi),
            //bu sınıfları bağımsız test etmek kolay olurdu.
            //İş mantığı(burada ödeme yöntemleri) Form1’in içinde olduğu için, bu mantığı ayrı birim testleriyle test etmek çok zor. 
            //Çünkü her seferinde tüm kodlar çalışıyor. Kodları(işleri) ayrı ayrı çalıştırıp denemek çok zor oluyor.








        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void cmbOdemeTuru_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
