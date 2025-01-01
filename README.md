## C# Final Ödev Kütüphanesi - README.md

Bu README dosyası, C# Final Ödev Kütüphanesinin içeriğini ve kullanımını Türkçe olarak açıklamaktadır. CLI komutları İngilizce olarak korunmuştur.
Kütüphane Hakkında

### Kütüphane Hakkında

Bu kütüphane, kitaplık yönetimi uygulamaları için temel sınıflar ve işlevler sağlar. Kullanıcılar kitap, DVD ve diğer öğeleri ekleyebilir, silebilir, listeleyebilir, düzenleyebilir ve ödünç verebilir. Kütüphane ayrıca öğeleri bir CSV dosyasında saklayarak verilerin kalıcılığını sağlar.
OOP Prensipleri ve DIP Kullanımı

### OOP İlkeleri ve DIP Kullanımı

Kütüphane, Nesne Yönelimli Programlama (OOP) prensiplerini ve Dependency Inversion Principle (DIP) prensibini şu şekilde kullanır:

*   **Encapsulation (Kapasulaştırma):** `LibraryItem`, `Author`, `Book`, `DVD` gibi sınıflar, verilerini private alanlarda tutar ve bunlara public getter ve setter yöntemleriyle erişim sağlar. Bu, verilerin doğruluğunu ve tutarlılığını korur. Örneğin, Author sınıfında FirstName ve LastName özellikleri, regex ile kontrol edilerek sadece harf içermesi sağlanır.

*   **Inheritance (Kalıtım):** `LibraryItem` sınıfı, `Book` ve `DVD` sınıfları için ortak özellikleri tanımlar. Bu, kod tekrarını azaltır ve hiyerarşik ilişkileri yansıtır. `Book` ve `DVD`, LibraryItem'dan türer ve kendi özelleştirilmiş özelliklerine sahiptir.

*   **Polymorphism (Çok Biçimlilik):** `LibraryItem` sınıfının `DisplayDetails` ve `ToCsv` yöntemleri, türetilmiş sınıflar tarafından farklı şekilde gerçekleştirilir. Bu, her öğe türünün kendi detaylarını ve CSV formatını özelleştirmesini sağlar.

*   **Abstraction (Soyutlama):** `ILibraryItemFactory` arayüzü, öğe oluşturma mantığını somut `LibraryItemFactory` sınıfından ayırır. Bu, kodun daha esnek ve bakımı daha kolay olmasını sağlar. Farklı öğe türleri için fabrika sınıfı kullanılarak, yeni öğe türleri eklemek kolaylaşır.

*   **Dependency Inversion Principle (DIP):** `Library` sınıfı, `FileRepository` ve `ILibraryItemFactory` arayüzlerine bağımlıdır, somut sınıflara değil. Bu, `Library` sınıfının farklı CSV işleyicileri veya fabrika uygulamaları ile çalışabilmesini sağlar. Bu prensip, test edilebilirliği artırır ve bağımlılıkları azaltır.

### Komut Satırı Arayüzü (CLI) Kullanım Örnekleri

Kütüphane, komut satırı arayüzü (CLI) aracılığıyla kullanıcılarla etkileşim kurar. Aşağıda bazı kullanım örnekleri verilmiştir:

*   **Yeni kitap ekleme:**
```
> add
Enter title: Bana C# Öğret
Enter number of authors: 1
Enter author 1's first name: Ali
Enter author 1's last name: Yılmaz
Enter item type (Book/DVD): Book
Enter page count: 300
Book added successfully.
```

*   **Tüm öğeleri listeleme:**
```
> list
Library Items:

[Book] ID: e7e41e78-f3b9-4c69-a42d-ecfaa9ab2412
Title: Bana C# Öğret
Authors: Ali Yılmaz
Pages: 300
Checked Out: False
```

*   **DVD ekleme:**
```
> add
Enter title: Interstellar
Enter number of authors: 1
Enter author 1's first name: Christopher
Enter author 1's last name: Nolan
Enter item type (Book/DVD): DVD
Enter duration (in minutes): 169
DVD added successfully.
```

*   **Öğeyi düzenleme (modify):**
```
> modify
Enter the ID of the item to modify: e7e41e78-f3b9-4c69-a42d-ecfaa9ab2412
Enter new title: İleri Düzey C#
Enter number of authors: 2
Enter author 1's first name: Ayşe
Enter author 1's last name: Demir
Enter author 2's first name: Veli
Enter author 2's last name: Can
Enter item type (Book/DVD): Book
Enter new page count: 450
Book modified successfully.
```

*   **Öğeyi silme (delete):**
```
> delete
Enter the ID of the item to delete: e7e41e78-f3b9-4c69-a42d-ecfaa9ab2412
Item deleted successfully.
```

*   **Ödünç verme (checkout):**
```
> checkout
Enter the ID of the item to check out: e7e41e78-f3b9-4c69-a42d-ecfaa9ab2412
Item checked out successfully.
```

*   **İade etme (return):**
```
> return
Enter the ID of the item to return: e7e41e78-f3b9-4c69-a42d-ecfaa9ab2412
Item returned successfully.
```

Bu örnekler, kütüphanenin temel işlevlerini nasıl kullanabileceğinizi göstermektedir. Daha fazla bilgi için kütüphanenin kodunu inceleyebilir veya Program.cs dosyasındaki yorumlara bakabilirsiniz.
