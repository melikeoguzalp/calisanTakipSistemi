CREATE TABLE HareketsizlikAyarlari (
    KullaniciId INT PRIMARY KEY,
    HareketsizlikLimitiDakika INT NOT NULL DEFAULT 2,
    FOREIGN KEY (KullaniciId) REFERENCES Kullanicilar(Id)
);