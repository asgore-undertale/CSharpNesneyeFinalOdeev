# Kütüphane Yönetim Sistemi

Bu proje, temel kütüphane işlemlerini gerçekleştiren bir konsol uygulamasıdır. Nesne Yönelimli Programlama (OOP) prensiplerini ve Bağımlılığı Ters Çevirme İlkesi (DIP) prensibini uygulamaktadır.

## Özellikler

1.  **Eleman Ekleme (Add Item):**
    *   Kullanıcı, kitap (Book) veya DVD ekleyebilir.
    *   Kullanıcıdan başlık, yazar ve özel bilgileri (sayfa sayısı ya da süre) girerek kütüphaneye yeni bir öğe eklenir.

2.  **Öğeleri Listeleme (List Items):**
    *   Kütüphanede bulunan tüm öğeler, detaylı bilgilerle birlikte görüntülenir.

3.  **Öğe Düzenleme (Modify Item):**
    *   Mevcut bir öğenin bilgileri, öğe kimliği (ID) kullanılarak güncellenir.

4.  **Öğe Silme (Delete Item):**
    *   Bir öğe, kimlik numarasıyla (ID) kütüphaneden silinir.

5.  **Öğeyi Ödünç Alma (Check Out Item):**
    *   Kullanıcı bir öğeyi ödünç alabilir ve bu işlem öğenin kontrol durumu (Checked Out) olarak işaretlenir.

6.  **Öğeyi İade Etme (Return Item):**
    *   Ödünç alınmış bir öğe, kullanıcı tarafından geri iade edilebilir ve öğenin kontrol durumu güncellenir.

7.  **Çıkış (Exit):**
    *   Programdan çıkış yapılır.

## Nesne Yönelimli Programlama (OOP) İlkeleri

1.  **Kalıtım (Inheritance):**
    *   `LibraryItem` sınıfı, `Book` ve `DVD` gibi türetilmiş sınıflar için temel bir sınıf olarak kullanılmıştır.
    *   Ortak özellikler (`Title`, `Author`, `ItemType`, `IsCheckedOut`) ve davranışlar (`CheckOut`, `Return`, `ToCsv`) `LibraryItem` sınıfında tanımlanmıştır.
    *   Bu sayede kod tekrarından kaçınılmış ve öğe türlerine göre esneklik sağlanmıştır.

2.  **Kapsülleme (Encapsulation):**
    *   `Library` sınıfı, kütüphane öğelerinin eklenmesi, silinmesi ve yönetilmesi gibi işlemleri kapsar.
    *   Dosyadan veri yükleme ve kaydetme işlemleri, `LoadFromCsv` ve `SaveToCsv` yöntemleri aracılığıyla dışarıdan gizlenmiştir.
    *   Ayrıca, öğelerin kontrol durumu (`IsCheckedOut`) gibi durumlar doğrudan değil, sadece sınıfın metotları aracılığıyla değiştirilebilir.

3.  **Çok Biçimlilik (Polymorphism):**
    *   `DisplayDetails` ve `ToCsv` yöntemleri, her türetilmiş sınıf (`Book` ve `DVD`) için farklı biçimlerde uygulanmıştır.
    *   Program, türetilmiş sınıfları `LibraryItem` tipi üzerinden işleterek çok biçimliliği kullanır.
    *   Örneğin, `LibraryItem` türündeki bir öğenin `DisplayDetails` çağrısı, öğenin gerçek türüne göre farklı bir sonuç döndürür.

4.  **Soyutlama (Abstraction):**
    *   `LibraryItem` sınıfı soyut bir sınıf olarak tanımlanmıştır ve somut türetilmiş sınıflar (`Book`, `DVD`) bu soyut sınıfı genişletir.
    *   Bu, öğelerin türlerine bakılmaksızın tek bir arayüz (`LibraryItem`) üzerinden işlem yapılmasını sağlar.
    *   Kullanıcılar, bir öğenin kitap mı yoksa DVD mi olduğunu bilmeden, sadece `LibraryItem` arayüzü ile işlem yapabilir.

## Bağımlılığı Ters Çevirme İlkesi (Dependency Inversion Principle - DIP)

Bağımlılığı Ters Çevirme İlkesi, yüksek seviyeli modüllerin (örneğin, `Library` sınıfı) düşük seviyeli modüllere (örneğin, `Book`, `DVD`) doğrudan bağımlı olmamasını sağlar. Bunun yerine, her iki taraf da bir soyutlamaya bağlı olmalıdır.

**Uygulama:**

*   Mevcut kodda, `Library` sınıfı doğrudan `Book` ve `DVD` sınıflarına bağımlıdır.
*   Bu bağımlılık, `LoadFromCsv` yönteminde `Book` ve `DVD` türlerinin doğrudan oluşturulmasıyla görülür.
*   Bu durum, yeni bir tür (örneğin, `Magazine`) eklenmek istendiğinde `Library` sınıfının değiştirilmesi gerektiği anlamına gelir. Bu da Bağımlılığı Ters Çevirme İlkesi'ni ihlal eder.

**Çözüm:**

Kod, bağımlılığı ters çevirmek için bir fabrika deseni (Factory Pattern) kullanılarak yeniden yapılandırılmıştır.

*   `ILibraryItemFactory` adında bir arayüz tanımlanmıştır. Bu arayüz, öğe türüne göre (`Book`, `DVD`) doğru nesneyi oluşturan bir metot sağlar.
*   `Library` sınıfı artık `Book` ve `DVD` sınıflarına doğrudan bağımlı değildir. Bunun yerine, sadece `ILibraryItemFactory` arayüzüne bağımlıdır.
*   Yeni bir öğe türü eklenmek istenirse, yalnızca bu fabrikanın uygulanması değiştirilir; `Library` sınıfında herhangi bir değişiklik gerekmez.

**Sonuç:**

Yapılan değişikliklerle, `Library` sınıfı artık düşük seviyeli modüllere değil, bir soyutlamaya bağımlıdır. Bu sayede kod, DIP'ye uygun hale gelmiştir ve daha genişletilebilir bir yapıya sahiptir.