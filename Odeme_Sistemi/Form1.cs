using System.Drawing;
using System.Reflection.PortableExecutable;
using System.Runtime.ConstrainedExecution;
using System.Security.Policy;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
// V2.0: enum ve abstract class ile kod düzenlendi.

namespace Odeme_Sistemi
{
    // 1) Enum:
    // Artık magic numbers (0-1 veya metin) gibi anlamsız sayılar/metinler yerine anlamlı isimler var.
    // Kodun okunabilirliği ve bakımı artar. Yeni ödeme türü eklemek kolaylaşır.
    // enum, belirli bir grup sabit değeri temsil eder. Örneğin, ödeme türleri gibi.
    // bu değerler genellikle birbirleriyle ilişkili ve sınırlı sayıda olur. Enum kullanarak bu değerleri daha anlamlı hale getirebiliriz.
    // enum tip güvenliğini artırır. Yanlış değer atanmasını engeller. Örneğin, int kullanırsak 0,1,2 gibi herhangi bir sayı atanabilir. Enum ile sadece tanımlı değerler atanabilir.
    // Enum'lar genellikle switch-case yapılarıyla birlikte kullanılır. Bu da kodun daha düzenli ve okunabilir olmasını sağlar.
    // enum gövdesinde ilk değer varsayılan olarak 0'dır ve sonraki değerler sırasıyla artar. Ancak, istenirse her değere özel bir sayı atanabilir veya string karşılıkları da kullanılabilir.
    // Örneğin krediKartı = "Kredi Kartı" veya krediKartı = 1 gibi. Ancak, C#'ta enum'lar genellikle int türünde tanımlanır.
    // ileride seçenekler arasına yeni seçenekler eklenmek istenirse, seçeneklere özel değerler atamak daha güvenli olabilir.
    // Örneğin: KrediKarti = 1, Havale = 5, Nakit = 10 gibi. Böylece araya yeni seçenekler eklemek istediğimizde mevcut değerlerin sırasını bozmadan ekleme yapabiliriz.
    // Enum'lar genellikle uygulamanın farklı bölümlerinde aynı değerleri kullanmak istediğimizde faydalıdır.
    // Örneğin, ödeme türlerini hem kullanıcı arayüzünde hem de iş mantığında(ödeme yöntemleri kodlarında) kullanmak isteyebiliriz.
    // Enum sayesinde bu değerleri merkezi bir yerde tanımlayarak tutarlılığı sağlayabiliriz.
    public enum OdemeYontemi
    {
        KrediKarti,
        Havale
    }



    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


            odemeSecenekleri();

        }


        private void odemeSecenekleri()
        {
            cmbOdemeTipi.DataSource = Enum.GetValues(typeof(OdemeYontemi)); //enum değerlerini(OdemeYontemi) combo box'a atar.
                                                                            //Böylece combo box'ta enum'daki seçenekler görünür ve seçilebilir olur.
                                                                            // Enum.GetValues(typeof(OdemeYontemi)) ifadesi, OdemeYontemi enum'ındaki tüm değerleri alır ve bunları bir dizi olarak döndürür.
                                                                            // Bu dizi, combo box'ın veri kaynağı olarak kullanılır.
                                                                            //önceden combo box'a elle "Kredi Kartı", "Havale" gibi seçenekler ekleniyordu. Şimdi ise enum'daki değerler otomatik olarak combo box'a ekleniyor.


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

            if (!decimal.TryParse(txtTutar.Text, out decimal girilenTutar) || girilenTutar <= 0)

            {
                MessageBox.Show("Geçerli bir tutar giriniz.");
                return;
            }

            OdemeYontemi secilenYontem = (OdemeYontemi)cmbOdemeTipi.SelectedItem!;
            // Buradaki ! operatörü “ben biliyorum, null olmayacak” demektir. Çünkü ComboBox’a enum değerlerini direkt ekledik.
            // cmbOdemeTipi.DataSource = Enum.GetValues(typeof(OdemeYontemi)); yazarak.

            // Combo box'ta seçilen ödeme yöntemini enum türüne dönüştürür. Böylece switch-case yapısında kullanabiliriz.

            // Abstract sınıftan referans tutulur.
            // Ama new ile abstract sınıf oluşturulmaz.
            Odeme? odemeIslemi = null;
            //Buradaki? işareti, “Bu değişken null olabilir, ben bunu bilerek yapıyorum” anlamına gelir. ? yazmaz isek yeşil çizgiyle uyarı verir. (hata değil sadece uyarı)
            //null! da yazılabilir !operatörü “null olsa bile sorun yok, ben sorumluluğu alıyorum” demektir. Ama bu yaklaşım risklidir.
            //Çünkü gerçekten null olursa ve null (yani içi boş, değeri henüz yok) ile işlem yapılmaya çalışılırsa program çökecek, hata verecektir.



            // Ödeme türüne göre hangi ödeme sınıfının kullanılacağını belirlemek için bir referans oluşturulur.
            // Ancak, bu referans başlangıçta null olarak atanır çünkü henüz hangi ödeme türünün seçileceği belli değildir.



            switch (secilenYontem)
            {
                case OdemeYontemi.KrediKarti: //enum çağrıldı
                    odemeIslemi = new KrediKartiOdeme();
                    // KrediKartiOdeme sınıfından bir nesne oluşturulur ve odemeIslemi referansına atanır.
                    // Böylece, odemeIslemi artık KrediKartiOdeme türünde bir nesneyi referans eder. 
                    // Bu nesne, KrediKartiOdeme sınıfının özelliklerini ve davranışlarını kullanarak ödeme işlemini gerçekleştirecektir.


                    break; //Switch bloğunu sonlandırır.

                case OdemeYontemi.Havale:
                    odemeIslemi = new HavaleOdeme();
                    break;
            }

            if (odemeIslemi != null)
            // odemeIslemi'nin null olup olmadığını kontrol eder. Eğer null değilse, yani geçerli bir ödeme türü seçilmişse, ödeme işlemi gerçekleştirilir.
            //if yerine odemeIslemi?.IslemiGerceklestir() gibi de yazabilirdik.
            //Ancak, bu durumda ödeme türü seçilmemişse hiçbir işlem yapılmaz ve lblSonuc güncellenmezdi.
            {
                odemeIslemi.Tutar = girilenTutar;
                // Abstract sınıfta tanımlanan ortak özellik olan Tutar'a, kullanıcı tarafından girilen tutar atanır. Böylece, ödeme işlemi gerçekleştirilirken bu tutar kullanılacaktır.
                lblSonuc.Text = odemeIslemi.IslemiGerceklestir();
                // Abstract sınıfta tanımlanan ortak davranış olan IslemiGerceklestir() metodu çağrılır.
                // Bu metodun hangi alt sınıf tarafından implement edildiği (uygulandığı), seçilen ödeme türüne bağlıdır.
            }


        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void cmbOdemeTuru_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}



// 2) Abstract class:
// Ortak yapı burada.
// Bu sınıftan new ile nesne üretilmez.
// Alt sınıflar bunu yazmak zorunda.
// Abstract sınıf, ortak özellikleri ve davranışları tanımlayan bir sınıftır. Ancak, bu sınıftan doğrudan nesne oluşturulamaz. Alt sınıflar bu sınıfı miras alarak kendi özel davranışlarını tanımlamak zorundadır.
// soyut sınıf denmesinin sebebi, bu sınıfın kendisi tek başına kullanılamaz, sadece miras verilerek kullanılabilir.
// Bu sayede ortak özellikler ve davranışlar tek bir yerde tanımlanır ve alt sınıflar bu yapıyı kullanarak kendi özel işlemlerini gerçekleştirebilirler.
// Bu da kodun daha düzenli, bakımı kolay ve genişletilebilir olmasını sağlar.
// interface yerine abstract class kullanmamızın sebebi, ortak özellikler ve davranışlar tanımlamak istememizdir.
// Interface sadece davranışları tanımlar, ancak ortak özellikler tanımlamak istediğimizde abstract class daha uygun olur.
// Ayrıca, interface'ler çoklu kalıtımı desteklerken, abstract class'lar tek kalıtımı destekler. Bu durumda, ödeme işlemleri için ortak bir yapı ve bazı ortak özellikler tanımlamak istediğimiz için abstract class kullanmak daha mantıklı olur.
// interface ancak davranışları tanımlarken, abstract class hem ortak özellikleri hem de ortak davranışları tanımlayabilir. Bu da ödeme işlemleri gibi belirli bir konsept için daha uygun bir yapı sağlar.
// kısaca hem ortak özellikler (Tutar gibi) hem de ortak davranışlar (IslemiGerceklestir gibi)  söz konusuyla abstract class mantıklı olur,
// fakat sadece davranış ve yetenekler tanımlanacaksa (örneğin sadece IslemiGerceklestir gibi) interface de kullanılabilir.
// Ancak bu örnekte ortak özellikler de var, bu yüzden abstract class daha uygun olur.
// abstract class = ortak özellik + davranış / yetenek fakat bir kez miras alınır.
// interface = sadece davranış / yetenek fakat birden fazla kez uygulanabilir. (Çoklu kalıtım destekler)
public abstract class Odeme
{
    public decimal Tutar { get; set; } // Ortak özellik. Tutar, tüm ödeme türleri için geçerlidir ve bu nedenle abstract sınıfta tanımlanır.
    // abstract olarak yazılmadığı için alt sınıflar bunu yazmak zorunda değildir, ancak ortak bir özellik olduğu için abstract sınıfta tanımlanır.

    // Alt sınıflar bunu yazmak zorunda.
    public abstract string IslemiGerceklestir(); // Ortak davranış. Her ödeme türü için farklı bir işlem gerçekleştirme yöntemi olabilir, bu yüzden bu metot abstract olarak tanımlanır. Alt sınıflar bu metodu kendi ihtiyaçlarına göre implement ederler.
}


// 3) Sealed concrete classes:
// sealed sınıflar, başka sınıflar tarafından miras alınamaz. Bu, bu sınıfların davranışlarının değiştirilemeyeceği anlamına gelir.
// Bu sınıflar genellikle belirli bir işlevi yerine getirmek için tasarlanır ve bu işlevin değiştirilmemesi istenir.
// Örneğin, KrediKartiOdeme ve HavaleOdeme sınıfları, ödeme işlemlerini gerçekleştirmek için tasarlanmıştır ve bu işlemlerin belirli bir şekilde yapılması istenebilir.
// Bu nedenle, bu sınıflar sealed olarak tanımlanır, böylece başka sınıflar tarafından miras alınamaz ve davranışları korunur. 
//sealed olmasaydı , başka bir sınıf KrediKartiOdeme veya HavaleOdeme sınıflarını miras alarak bu sınıfların davranışlarını değiştirebilirdi.
//Bu da ödeme işlemlerinin beklenmedik şekilde çalışmasına neden olabilir. Sealed sınıflar, bu tür durumları önlemek için kullanılır.
//sealed sınıf miras vermez ama alabilir. Yani sealed sınıf başka bir sınıftan miras alabilir, ancak kendisi miras veremez.
//Bu, sealed sınıfların belirli bir işlevi yerine getirmek için tasarlandığı ve bu işlevin değiştirilmemesi gerektiği durumlarda kullanılır.
// sealed class, new ile nesne örneği alınabilir.
public sealed class KrediKartiOdeme : Odeme
{
    public override string IslemiGerceklestir()
    // override anahtar kelimesi, abstract sınıftaki soyut metodu alt sınıfta geçersiz kılarak kendi özel davranışını tanımlamak için kullanılır.
    // Bu, polymorphism (çok biçimlilik) sağlar, yani aynı temel sınıf türünde(ödeme) farklı alt sınıf nesneleri(kredi kartı, havale vb.) farklı davranışlar sergileyebilir.
    {
        decimal komisyonluTutar = Tutar + (Tutar * 0.05m);
        return "Kredi Kartı ile ödendi. Toplam: " + komisyonluTutar.ToString("0.00") + " TL";
    }
}
public sealed class HavaleOdeme : Odeme
{
    public override string IslemiGerceklestir()
    // bu kez override edilen metot, havale ödemesi için geçerli olan özel bir davranışı tanımlar. Bu durumda, havale ödemelerinde %10 indirim uygulanır.
    {
        decimal indirimliTutar = Tutar - (Tutar * 0.10m);
        return "Havale ile ödendi. Toplam: " + indirimliTutar.ToString("0.00") + " TL";
    }
}